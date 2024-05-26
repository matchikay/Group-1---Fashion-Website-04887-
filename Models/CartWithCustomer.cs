namespace LaFlor.Models
{
    public class CartWithCustomer
    {
        public Cart cart { get; set; }
        public Customer customer { get; set; }
        public Flowers flowers { get; set; }
        public Orders orders { get; set; }
    }
}
