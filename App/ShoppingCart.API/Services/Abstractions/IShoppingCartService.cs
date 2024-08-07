using ShoppingCart.API.DTOs;

namespace ShoppingCart.API.Services.Abstractions
{
    public interface IShoppingCartService
    {
        Task<ShoppingCartDto> GetCartByUserAsync(string userId);
        Task<ShoppingCartDto> GetCartByIpAsync(string ip);
        Task AddToCartAsync(string userId, string ip, CartItemDto cartItemDto);
        Task<decimal> GetCartTotalPriceAsync(string userId, string ip);
        Task<ProductDto> GetProductByIdAsync(Guid productId);
        Task<UserDto> GetUserByIdAsync(Guid userId); 
    }

}
