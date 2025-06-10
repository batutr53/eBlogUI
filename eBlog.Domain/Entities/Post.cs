using eBlog.Domain.Common;
using eBlog.Domain.Interfaces;

namespace eBlog.Domain.Entities
{
    public class Post : BaseEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public Guid AuthorId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid? SeoMetadataId { get; set; }

        public string LanguageCode { get; set; } = "tr";
        public DateTime PublishedAt { get; set; }
        public bool IsPublished { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
        public User Author { get; set; } = null!;
        public Category Category { get; set; } = null!;
        public SeoMetadata? SeoMetadata { get; set; }

        public ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
        public ICollection<PostModule> PostModules { get; set; } = new List<PostModule>();
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}
