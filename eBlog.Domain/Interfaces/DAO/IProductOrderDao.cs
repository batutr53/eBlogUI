namespace eBlog.Domain.Interfaces.DAO
{
    public interface IProductOrderDao
    {
        Task<decimal> GetTotalSalesForProductAsync(Guid productId);
        // Siparişlere özel dapper metotlar
    }
}
