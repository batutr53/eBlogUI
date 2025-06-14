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

        public PostController(
            IPostApiService postApiService,
            ICategoryApiService categoryApiService,
            ITagApiService tagApiService)
        {
            _postApiService = postApiService;
            _categoryApiService = categoryApiService;
            _tagApiService = tagApiService;
        }

        // Listeleme
        public async Task<IActionResult> Index()
        {
            try
            {
                var result = await _postApiService.GetListAsync();
                return View(result.Data ?? new List<PostListDto>());
            }
            catch
            {
                TempData["ErrorMessage"] = "Post listesi alınırken bir hata oluştu.";
                return View(new List<PostListDto>());
            }
        }

        // Dropdownlar için ortak method
        private async Task PopulateDropdownsAsync()
        {
            var categories = await _categoryApiService.GetListAsync();
            var tags = await _tagApiService.GetListAsync();

            ViewBag.Categories = categories.Data ?? new List<CategoryListDto>();
            ViewBag.Tags = tags.Data ?? new List<TagListDto>();

        }

        // Post Ekle (GET)
        public async Task<IActionResult> Create()
        {
            await PopulateDropdownsAsync();
            return View(new PostCreateDto());
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(PostCreateDto dto)
        {
            await PopulateDropdownsAsync();

            if (!ModelState.IsValid)
                return View(dto);

            var result = await _postApiService.CreateAsync(dto);

            if (result.Success)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = result.Message;
            return View(dto);
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var postResult = await _postApiService.GetDetailAsync(id);
            if (!postResult.Success || postResult.Data == null)
            {
                TempData["ErrorMessage"] = postResult.Message;
                return RedirectToAction("Index");
            }

            var categoryResult = await _categoryApiService.GetListAsync();
            var tagResult = await _tagApiService.GetListAsync();

            ViewBag.Categories = categoryResult.Data ?? new();
            ViewBag.Tags = tagResult.Data ?? new();
            ViewBag.Seo = postResult.Data.SeoMetadata;
            ViewBag.PostId = postResult.Data.Id;

            var updateDto = new PostUpdateDto
            {
                Title = postResult.Data.Title,
                Content = postResult.Data.Content,
                CategoryId = categoryResult.Data?.FirstOrDefault(c => c.Name == postResult.Data.CategoryName)?.Id ?? Guid.Empty,
                TagIds = postResult.Data.TagIds ?? new List<Guid>()
            };

            return View(updateDto);
        }

        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id, PostUpdateDto dto)
        {
            var categoryResult = await _categoryApiService.GetListAsync();
            var tagResult = await _tagApiService.GetListAsync();

            ViewBag.Categories = categoryResult.Data ?? new();
            ViewBag.Tags = tagResult.Data ?? new();
            ViewBag.PostId = id;

            if (!ModelState.IsValid)
                return View(dto);

            var result = await _postApiService.UpdateAsync(id, dto);

            if (result.Success)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = result.Message;
            return View(dto);
        }


        // Post Sil (AJAX)
        [HttpPost]
        public async Task<JsonResult> Delete(Guid id)
        {
            try
            {
                var result = await _postApiService.DeleteAsync(id);
                if (result.Success)
                    return Json(new { success = true, message = result.Message });

                return Json(new { success = false, message = result.Message ?? "Silme işlemi başarısız." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Sunucu hatası: " + ex.Message });
            }
        }
    }
}
