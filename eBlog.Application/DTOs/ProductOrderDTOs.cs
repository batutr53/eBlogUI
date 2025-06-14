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
        public Guid BuyerId { get; set; }
        public string BuyerEmail { get; set; }
        public Guid ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime OrderDate { get; set; }
    }

    public class ProductOrderCreateDto
    {
        public Guid BuyerId { get; set; }   // Controller'da setleniyor
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public DateTime OrderedAt { get; set; }
    }

}


