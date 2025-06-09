namespace eBlog.Application.DTOs
{
    public class ProductOrderListDto
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = null!;
        public string BuyerUserName { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = null!;
    }

    public class ProductOrderCreateDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }

}
