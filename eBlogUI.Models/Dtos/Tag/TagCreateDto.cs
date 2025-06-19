using System.ComponentModel.DataAnnotations;

namespace eBlogUI.Models.Dtos.Tag
{
    public class TagCreateDto
    {
        [Required(ErrorMessage = "Etiket adı zorunludur")]
        [StringLength(50, ErrorMessage = "Etiket adı en fazla 50 karakter olabilir")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir")]
        public string Description { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "SEO URL en fazla 100 karakter olabilir")]
        public string Slug { get; set; } = string.Empty;
        
        public bool IsActive { get; set; } = true;
    }
}
