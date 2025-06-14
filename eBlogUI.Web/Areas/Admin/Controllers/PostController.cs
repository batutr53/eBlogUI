using eBlog.Application.DTOs;
using eBlogUI.Business.Interfaces;
using eBlogUI.Business.Services.Interfaces;
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
                _logger.LogError(ex, "Post detayı alınırken hata: {PostId}", id);
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
                    TempData["ErrorMessage"] = postResult.Message ?? "Post bulunamadı.";
                    return RedirectToAction("Index");
                }

                await PopulateDropdownsAsync();

                var updateDto = new PostUpdateDto
                {
                    Title = postResult.Data.Title,
                    Content = postResult.Data.Content,
                    CategoryId = GetCategoryIdByName(postResult.Data.CategoryName),
                    TagIds = postResult.Data.TagIds ?? new List<Guid>()
                };

                ViewBag.PostId = id;
                ViewBag.Seo = postResult.Data.SeoMetadata;

                return View(updateDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Post düzenleme sayfası yüklenirken hata: {PostId}", id);
                TempData["ErrorMessage"] = "Post bilgileri alınırken hata oluştu.";
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
                await PopulateDropdownsAsync();
                ViewBag.PostId = id;

                if (!ModelState.IsValid)
                {
                    return View(dto);
                }

                var result = await _postApiService.UpdateAsync(id, dto);

                if (result.Success)
                {
                    TempData["SuccessMessage"] = result.Message ?? "Post başarıyla güncellendi.";
                    _logger.LogInformation("Post başarıyla güncellendi: {PostId}", id);
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", result.Message ?? "Post güncellenemedi.");
                return View(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Post güncellenirken hata: {PostId}", id);
                await PopulateDropdownsAsync();
                ViewBag.PostId = id;
                ModelState.AddModelError("", "Post güncellenirken beklenmeyen bir hata oluştu.");
                return View(dto);
            }
        }

        // ✅ Post Sil (AJAX)
        [HttpPost]
        public async Task<JsonResult> Delete(Guid id)
        {
            try
            {
                var result = await _postApiService.DeleteAsync(id);

                if (result.Success)
                {
                    _logger.LogInformation("Post başarıyla silindi: {PostId}", id);
                    return Json(new { success = true, message = result.Message ?? "Post başarıyla silindi." });
                }

                _logger.LogWarning("Post silinemedi: {PostId} - {Message}", id, result.Message);
                return Json(new { success = false, message = result.Message ?? "Post silinemedi." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Post silinirken beklenmeyen hata: {PostId}", id);
                return Json(new { success = false, message = "Sunucu hatası oluştu." });
            }
        }

        // ✅ Yardımcı metodlar
        private Guid GetCategoryIdByName(string categoryName)
        {
            if (ViewBag.Categories is List<CategoryListDto> categories)
            {
                return categories.FirstOrDefault(c => c.Name == categoryName)?.Id ?? Guid.Empty;
            }
            return Guid.Empty;
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