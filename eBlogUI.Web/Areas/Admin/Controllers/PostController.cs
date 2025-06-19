using eBlogUI.Models.Dtos.Category;
using eBlogUI.Models.Dtos.Tag;
using eBlogUI.Models.Dtos.Post;
using eBlogUI.Models.Dtos;
using eBlogUI.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eBlogUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly IPostApiService _postApiService;
        private readonly ICategoryApiService _categoryApiService;
        private readonly ITagApiService _tagApiService;
        private readonly ILogger<PostController> _logger;

        public PostController(
            IPostApiService postApiService,
            ICategoryApiService categoryApiService,
            ITagApiService tagApiService,
            ILogger<PostController> logger)
        {
            _postApiService = postApiService;
            _categoryApiService = categoryApiService;
            _tagApiService = tagApiService;
            _logger = logger;
        }

        // ✅ Listeleme
        public async Task<IActionResult> Index()
        {
            try
            {
                var result = await _postApiService.GetListAsync();

                if (!result.Success)
                {
                    TempData["ErrorMessage"] = result.Message;
                    _logger.LogWarning("Post listesi alınamadı: {Message}", result.Message);
                    return View(new List<PostListDto>());
                }

                return View(result.Data ?? new List<PostListDto>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Post listesi alınırken beklenmeyen hata");
                TempData["ErrorMessage"] = "Post listesi alınırken bir hata oluştu.";
                return View(new List<PostListDto>());
            }
        }

        // ✅ Detay Görüntüleme
        public async Task<IActionResult> Detail(Guid id)
        {
            try
            {
                var result = await _postApiService.GetDetailAsync(id);

                if (!result.Success || result.Data == null)
                {
                    TempData["ErrorMessage"] = result.Message ?? "Post bulunamadı.";
                    _logger.LogWarning("Post detayı bulunamadı: {PostId}", id);
                    return RedirectToAction("Index");
                }

                // SEO metadata kontrolü
                if (result.Data.SeoMetadata == null)
                {
                    _logger.LogWarning("Post SEO metadata eksik: {PostId}", id);
                    TempData["WarningMessage"] = "Bu post için SEO bilgileri eksik.";
                }

                return View(result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Post detayı alınırken beklenmeyen hata: {PostId}", id);
                TempData["ErrorMessage"] = "Post detayı alınırken bir hata oluştu.";
                return RedirectToAction("Index");
            }
        }

        // ✅ Dropdownları doldur
        private async Task PopulateDropdownsAsync()
        {
            try
            {
                var categoriesTask = _categoryApiService.GetListAsync();
                var tagsTask = _tagApiService.GetListAsync();

                await Task.WhenAll(categoriesTask, tagsTask);

                var categories = await categoriesTask;
                var tags = await tagsTask;

                ViewBag.Categories = categories.Data ?? new List<CategoryListDto>();
                ViewBag.Tags = tags.Data ?? new List<TagListDto>();

                if (!categories.Success)
                {
                    _logger.LogWarning("Kategoriler alınamadı: {Message}", categories.Message);
                    TempData["WarningMessage"] = "Kategoriler yüklenemedi.";
                }

                if (!tags.Success)
                {
                    _logger.LogWarning("Etiketler alınamadı: {Message}", tags.Message);
                    TempData["WarningMessage"] = "Etiketler yüklenemedi.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Dropdown verileri yüklenirken hata");
                ViewBag.Categories = new List<CategoryListDto>();
                ViewBag.Tags = new List<TagListDto>();
                TempData["WarningMessage"] = "Bazı veriler yüklenemedi.";
            }
        }

        // ✅ Post Ekle (GET)
        public async Task<IActionResult> Create()
        {
            await PopulateDropdownsAsync();
            return View(new PostCreateDto());
        }

        // ✅ Post Ekle (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostCreateDto dto)
        {
            try
            {
                await PopulateDropdownsAsync();

                if (!ModelState.IsValid)
                {
                    return View(dto);
                }

                var result = await _postApiService.CreateAsync(dto);

                if (result.Success)
                {
                    TempData["SuccessMessage"] = result.Message ?? "Post başarıyla oluşturuldu.";
                    _logger.LogInformation("Post başarıyla oluşturuldu: {Title}", dto.Title);
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", result.Message ?? "Post oluşturulamadı.");
                _logger.LogWarning("Post oluşturulamadı: {Message}", result.Message);
                return View(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Post oluşturulurken beklenmeyen hata: {Title}", dto.Title);
                await PopulateDropdownsAsync();
                ModelState.AddModelError("", "Post oluşturulurken beklenmeyen bir hata oluştu.");
                return View(dto);
            }
        }

        // ✅ Post Düzenle (GET)
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var postResult = await _postApiService.GetDetailAsync(id);

                if (!postResult.Success || postResult.Data == null)
                {
                    TempData["ErrorMessage"] = "Post bulunamadı.";
                    return RedirectToAction("Index");
                }

                var categoriesResult = await _categoryApiService.GetListAsync();
                var tagsResult = await _tagApiService.GetListAsync();

                ViewBag.Categories = categoriesResult.Success ? categoriesResult.Data : new List<CategoryListDto>();
                ViewBag.Tags = tagsResult.Success ? tagsResult.Data : new List<TagListDto>();

                var updateDto = new PostUpdateDto
                {
                    Id = postResult.Data.Id,
                    Title = postResult.Data.Title,
                    Content = postResult.Data.Content,
                    CategoryId = postResult.Data.CategoryId,
                    Tags = postResult.Data.Tags ?? new List<string>()
                };

                ViewBag.PostId = id;
                ViewBag.Seo = postResult.Data.SeoMetadata;

                return View(updateDto);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Post yüklenirken hata oluştu: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        // ✅ Post Düzenle (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PostUpdateDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var categoriesResult = await _categoryApiService.GetListAsync();
                    var tagsResult = await _tagApiService.GetListAsync();
                    ViewBag.Categories = categoriesResult.Success ? categoriesResult.Data : new List<CategoryListDto>();
                    ViewBag.Tags = tagsResult.Success ? tagsResult.Data : new List<TagListDto>();
                    return View(dto);
                }

                var result = await _postApiService.UpdateAsync(id, dto);
                if (result.Success)
                {
                    TempData["SuccessMessage"] = "Post başarıyla güncellendi.";
                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = result.Message;
                return View(dto);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Post güncellenirken hata oluştu: " + ex.Message;
                return View(dto);
            }
        }

        // ✅ Post Sil
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _postApiService.DeleteAsync(id);
                if (result.Success)
                {
                    TempData["SuccessMessage"] = "Post başarıyla silindi.";
                }
                else
                {
                    TempData["ErrorMessage"] = result.Message;
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Post silinirken hata oluştu: " + ex.Message;
            }

            return RedirectToAction("Index");
        }

        // ✅ Yardımcı metodlar
        private Guid GetCategoryIdByName(string categoryName)
        {
            if (ViewBag.Categories is List<CategoryListDto> categories)
            {
                return categories.FirstOrDefault(c => c.Name == categoryName)?.Id ?? Guid.Empty; // Default category ID
            }
            return Guid.Empty; // Default category ID
        }

        private int? GetUserId()
        {
            var userIdClaim = HttpContext.User.FindFirst("UserId")?.Value;
            return int.TryParse(userIdClaim, out var userId) ? userId : null;
        }

        // ✅ SEO Preview
        [HttpGet]
        public async Task<IActionResult> SeoPreview(string slug)
        {
            try
            {
                var result = await _postApiService.GetPostForSeoAsync(slug);

                if (!result.Success)
                    return NotFound();

                return PartialView("_SeoPreview", result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SEO preview hatası: {Slug}", slug);
                return StatusCode(500);
            }
        }
    }
}