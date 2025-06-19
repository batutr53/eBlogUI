using eBlogUI.Models.Dtos;
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
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await _authApiService.LoginAsync(loginDto);
            if (!result.Success || result.Data == null)
            {
                TempData["ErrorMessage"] = result.Message;
                return View(loginDto);
            }

            var authUser = result.Data;
            Response.Cookies.Append("AuthToken", authUser.Token, new CookieOptions
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
