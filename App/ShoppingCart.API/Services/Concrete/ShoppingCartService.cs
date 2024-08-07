using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ShoppingCart.API.Data;
using ShoppingCart.API.DTOs;
using ShoppingCart.API.Entities;
using ShoppingCart.API.Services.Abstractions;

namespace ShoppingCart.API.Services.Concrete
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ShoppingCartService> _logger;

        public ShoppingCartService(ApplicationDbContext context, IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<ShoppingCartService> logger)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<ShoppingCartDto> GetCartByUserAsync(string userId)
        {
            var cart = await _context.ShoppingCarts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);
            return cart != null ? new ShoppingCartDto
            {
                Id = cart.Id,
                CartItems = cart.CartItems.Select(ci => new CartItemDto
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    Price = ci.Price
                }).ToList()
            } : null;
        }

        public async Task<ShoppingCartDto> GetCartByIpAsync(string ip)
        {
            var cart = await _context.ShoppingCarts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserIp == ip);
            return cart != null ? new ShoppingCartDto
            {
                Id = cart.Id,
                CartItems = cart.CartItems.Select(ci => new CartItemDto
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    Price = ci.Price
                }).ToList()
            } : null;
        }

        public async Task AddToCartAsync(string userId, string ip, CartItemDto cartItemDto)
        {
            Entities.ShoppingCart cart;
            if (!string.IsNullOrEmpty(userId))
            {
                cart = await _context.ShoppingCarts
                    .FirstOrDefaultAsync(c => c.UserId == userId);
                if (cart == null)
                {
                    cart = new Entities.ShoppingCart { UserId = userId, CartItems = new List<CartItem>() };
                    _context.ShoppingCarts.Add(cart);
                }
            }
            else
            {
                cart = await _context.ShoppingCarts
                    .FirstOrDefaultAsync(c => c.UserIp == ip);
                if (cart == null)
                {
                    cart = new Entities.ShoppingCart { UserIp = ip, CartItems = new List<CartItem>() };
                    _context.ShoppingCarts.Add(cart);
                }
            }

            var product = await GetProductByIdAsync(cartItemDto.ProductId);

            if (product == null)
            {
                throw new Exception("Product not found");
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == cartItemDto.ProductId);
            if (cartItem != null)
            {
                cartItem.Quantity += cartItemDto.Quantity;
            }
            else
            {
                cart.CartItems.Add(new CartItem
                {
                    ProductId = cartItemDto.ProductId,
                    Quantity = cartItemDto.Quantity,
                    Price = product.Price
                });
            }

            await _context.SaveChangesAsync();
        }

        public async Task<decimal> GetCartTotalPriceAsync(string userId, string ip)
        {
            Entities.ShoppingCart cart;
            if (!string.IsNullOrEmpty(userId))
            {
                cart = await _context.ShoppingCarts
                    .Include(c => c.CartItems)
                    .FirstOrDefaultAsync(c => c.UserId == userId);
            }
            else
            {
                cart = await _context.ShoppingCarts
                    .Include(c => c.CartItems)
                    .FirstOrDefaultAsync(c => c.UserIp == ip);
            }

            if (cart == null)
                return 0;

            return cart.CartItems.Sum(ci => ci.Price * ci.Quantity);
        }

        public async Task<ProductDto> GetProductByIdAsync(Guid productId)
        {
            var client = _httpClientFactory.CreateClient("CatalogAPI");
            var response = await client.GetAsync($"/api/product/{productId}");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Failed to retrieve product with ID {productId}. Status Code: {response.StatusCode}");
                return null;
            }

            var product = await response.Content.ReadFromJsonAsync<ProductDto>();
            if (product == null)
            {
                _logger.LogError($"Product with ID {productId} not found.");
            }
            return product;
        }

        public async Task<UserDto> GetUserByIdAsync(Guid userId)
        {
            var client = _httpClientFactory.CreateClient("AuthenticationAPI");
            var response = await client.GetAsync($"/api/user/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Failed to retrieve user with ID {userId}. Status Code: {response.StatusCode}");
                return null;
            }

            var user = await response.Content.ReadFromJsonAsync<UserDto>();
            if (user == null)
            {
                _logger.LogError($"User with ID {userId} not found.");
            }
            return user;
        }
    }

}
