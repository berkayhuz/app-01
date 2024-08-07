namespace ShoppingCart.API.DTOs
{
    public class ShoppingCartDto
    {
        public Guid Id { get; set; }
        public List<CartItemDto> CartItems { get; set; }
    }

}
