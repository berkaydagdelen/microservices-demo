namespace OrderService.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }  // Hangi kullanýcýya ait
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime OrderDate { get; set; }
    }
}

