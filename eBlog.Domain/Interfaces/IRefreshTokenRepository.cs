using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;

namespace eBlog.Application.Interfaces.Repositories
{
    public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
    {
        Task<RefreshToken?> GetByTokenAsync(string token);
    }
}
