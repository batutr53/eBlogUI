using eBlogUI.Business.Interfaces;
using eBlogUI.Models.Dtos.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eBlogUI.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Editor")]
    public class CategoryController : Controller
    {
        private readonly ICategoryApiService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(
            ICategoryApiService categoryService,
            ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var result = await _categoryService.GetListAsync();
                if (result.Success)
                {
                    return View(result.Data);
                }

                TempData["ErrorMessage"] = result.Message ?? "Kategoriler yüklenirken bir hata oluştu.";
                return View(new List<CategoryListDto>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kategoriler yüklenirken bir hata oluştu");
                TempData["ErrorMessage"] = "Kategoriler yüklenirken bir hata oluştu.";
                return View(new List<CategoryListDto>());
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            try
            {
                var result = await _categoryService.CreateAsync(dto);
                if (result.Success)
                {
                    TempData["SuccessMessage"] = result.Message ?? "Kategori başarıyla oluşturuldu.";
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", result.Message ?? "Kategori oluşturulurken bir hata oluştu.");
                return View(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kategori oluşturulurken bir hata oluştu");
                ModelState.AddModelError("", "Kategori oluşturulurken bir hata oluştu.");
                return View(dto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var result = await _categoryService.GetByIdAsync(id);
                if (!result.Success || result.Data == null)
                {
                    TempData["ErrorMessage"] = result.Message ?? "Kategori bulunamadı.";
                    return RedirectToAction(nameof(Index));
                }

                var updateDto = new CategoryUpdateDto
                {
                    Id = result.Data.Id,
                    Name = result.Data.Name,
                    Description = result.Data.Description,
                    Slug = result.Data.Slug,
                    IsActive = result.Data.IsActive
                };

                return View(updateDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kategori düzenleme sayfası yüklenirken bir hata oluştu");
                TempData["ErrorMessage"] = "Kategori düzenleme sayfası yüklenirken bir hata oluştu.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            try
            {
                var result = await _categoryService.UpdateAsync(dto);
                if (result.Success)
                {
                    TempData["SuccessMessage"] = result.Message ?? "Kategori başarıyla güncellendi.";
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", result.Message ?? "Kategori güncellenirken bir hata oluştu.");
                return View(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kategori güncellenirken bir hata oluştu");
                ModelState.AddModelError("", "Kategori güncellenirken bir hata oluştu.");
                return View(dto);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _categoryService.DeleteAsync(id);
                if (result.Success)
                {
                    TempData["SuccessMessage"] = result.Message ?? "Kategori başarıyla silindi.";
                }
                else
                {
                    TempData["ErrorMessage"] = result.Message ?? "Kategori silinirken bir hata oluştu.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kategori silinirken bir hata oluştu");
                TempData["ErrorMessage"] = "Kategori silinirken bir hata oluştu.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
