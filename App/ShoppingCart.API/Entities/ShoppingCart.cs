namespace ShoppingCart.API.Entities
{
    public class ShoppingCart
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string UserIp { get; set; } 
        public ICollection<CartItem> CartItems { get; set; }
    }

}
