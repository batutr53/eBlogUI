using System.ComponentModel.DataAnnotations;

namespace eBlogUI.Models.Dtos.Category
{
    public class CategoryCreateDto
    {
        [Required(ErrorMessage = "Kategori adı zorunludur")]
        [StringLength(100, ErrorMessage = "Kategori adı en fazla 100 karakter olabilir")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir")]
        public string Description { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "SEO URL en fazla 100 karakter olabilir")]
        public string Slug { get; set; } = string.Empty;
        
        public bool IsActive { get; set; } = true;
    }
}
