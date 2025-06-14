using eBlog.Domain.Entities;
using eBlog.Domain.Interfaces;
using eBlog.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace eBlog.Persistence.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(AppDbContext context) : base(context) { }

        public async Task<List<Post>> GetPostsByAuthorAsync(Guid authorId)
           => await _dbSet.Where(x => x.AuthorId == authorId).ToListAsync();

        public async Task<Post?> GetPostWithDetailsAsync(Guid id)
        {
            return await _dbSet
                .Include(p => p.Author)           // Post yazarı
                .Include(p => p.Category)         // Kategori
                .Include(p => p.SeoMetadata)      // 🎯 SEO metadata (KRITIK!)
                .Include(p => p.PostTags)         // Post etiketleri
                    .ThenInclude(pt => pt.Tag)    // Etiket detayları
                .Include(p => p.PostModules)      // Post modülleri
                .Include(p => p.Language)         // Dil bilgisi
                .Where(p => p.DeletedAt == null)  // Silinmemiş
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Post>> GetPostsWithBasicDetailsAsync()
        {
            return await _dbSet
                .Include(p => p.Author)
                .Include(p => p.Category)
                .Where(p => p.DeletedAt == null && p.IsPublished)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<Post?> GetPostBySlugWithDetailsAsync(string slug)
        {
            return await _dbSet
                .Include(p => p.Author)
                .Include(p => p.Category)
                .Include(p => p.SeoMetadata)
                .Include(p => p.PostTags)
                    .ThenInclude(pt => pt.Tag)
                .Include(p => p.PostModules)
                .Where(p => p.DeletedAt == null)
                .FirstOrDefaultAsync(p => p.Slug == slug);
        }
    }
}
