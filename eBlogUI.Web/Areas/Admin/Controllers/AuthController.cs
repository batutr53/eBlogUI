using eBlog.Application.DTOs.Auth;
using eBlogUI.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eBlogUI.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        private readonly IAuthApiService _authApiService;

        public AuthController(IAuthApiService authApiService)
        {
            _authApiService = authApiService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _authApiService.LoginAsync(dto);
            if (!result.Success)
            {
                TempData["ErrorMessage"] = result.Message;
                return View(dto);
            }

            var token = result.Data;
            Response.Cookies.Append("AuthToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.Now.AddHours(1)
            });

            return RedirectToAction("Index", "Dashboard");
        }
    }
}
