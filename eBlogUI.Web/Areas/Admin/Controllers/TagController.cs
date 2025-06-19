using eBlogUI.Business.Interfaces;
using eBlogUI.Models.Dtos.Tag;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eBlogUI.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Editor")]
    public class TagController : Controller
    {
        private readonly ITagApiService _tagService;
        private readonly ILogger<TagController> _logger;

        public TagController(
            ITagApiService tagService,
            ILogger<TagController> logger)
        {
            _tagService = tagService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var result = await _tagService.GetListAsync();
                if (result.Success)
                {
                    return View(result.Data);
                }

                TempData["ErrorMessage"] = result.Message ?? "Etiketler yüklenirken bir hata oluştu.";
                return View(new List<TagListDto>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Etiketler yüklenirken bir hata oluştu");
                TempData["ErrorMessage"] = "Etiketler yüklenirken bir hata oluştu.";
                return View(new List<TagListDto>());
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TagCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            try
            {
                var result = await _tagService.CreateAsync(dto);
                if (result.Success)
                {
                    TempData["SuccessMessage"] = result.Message ?? "Etiket başarıyla oluşturuldu.";
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", result.Message ?? "Etiket oluşturulurken bir hata oluştu.");
                return View(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Etiket oluşturulurken bir hata oluştu");
                ModelState.AddModelError("", "Etiket oluşturulurken bir hata oluştu.");
                return View(dto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var result = await _tagService.GetByIdAsync(id);
                if (!result.Success || result.Data == null)
                {
                    TempData["ErrorMessage"] = result.Message ?? "Etiket bulunamadı.";
                    return RedirectToAction(nameof(Index));
                }

                var updateDto = new TagUpdateDto
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
                _logger.LogError(ex, "Etiket düzenleme sayfası yüklenirken bir hata oluştu");
                TempData["ErrorMessage"] = "Etiket düzenleme sayfası yüklenirken bir hata oluştu.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TagUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            try
            {
                var result = await _tagService.UpdateAsync(dto);
                if (result.Success)
                {
                    TempData["SuccessMessage"] = result.Message ?? "Etiket başarıyla güncellendi.";
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", result.Message ?? "Etiket güncellenirken bir hata oluştu.");
                return View(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Etiket güncellenirken bir hata oluştu");
                ModelState.AddModelError("", "Etiket güncellenirken bir hata oluştu.");
                return View(dto);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _tagService.DeleteAsync(id);
                if (result.Success)
                {
                    TempData["SuccessMessage"] = result.Message ?? "Etiket başarıyla silindi.";
                }
                else
                {
                    TempData["ErrorMessage"] = result.Message ?? "Etiket silinirken bir hata oluştu.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Etiket silinirken bir hata oluştu");
                TempData["ErrorMessage"] = "Etiket silinirken bir hata oluştu.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
