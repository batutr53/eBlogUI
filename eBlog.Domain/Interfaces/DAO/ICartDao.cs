namespace eBlog.Domain.Interfaces.DAO
{
    public interface ICartDao
    {
        Task<decimal> GetTotalCartPriceAsync(Guid cartId);
        // Sepete özel dapper metotlar
    }
}
