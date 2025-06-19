using eBlogUI.Business.Interfaces;
using eBlogUI.Models.Dtos;
using Newtonsoft.Json;
using eBlog.Shared.Results;
using System.Text;

namespace eBlogUI.Business.Services
{
    public class AuthApiManager : IAuthApiService
    {
        private readonly HttpClient _httpClient;

        public AuthApiManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IDataResult<AuthUserDto>> LoginAsync(LoginDto dto)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(dto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync("auth/login", content);
                
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var authUser = JsonConvert.DeserializeObject<AuthUserDto>(responseContent);
                    
                    if (authUser != null)
                        return new SuccessDataResult<AuthUserDto>(authUser, "Giriş başarılı");
                }
                
                return new ErrorDataResult<AuthUserDto>("Giriş bilgileri hatalı");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<AuthUserDto>($"Hata: {ex.Message}");
            }
        }

        public async Task<IResult> RegisterAsync(RegisterDto dto)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(dto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync("auth/register", content);
                
                if (response.IsSuccessStatusCode)
                {
                    return new SuccessResult("Kayıt başarılı");
                }
                
                return new ErrorResult("Kayıt işlemi başarısız");
            }
            catch (Exception ex)
            {
                return new ErrorResult($"Hata: {ex.Message}");
            }
        }

        public async Task<IResult> LogoutAsync()
        {
            try
            {
                var response = await _httpClient.PostAsync("auth/logout", null);
                
                if (response.IsSuccessStatusCode)
                {
                    return new SuccessResult("Çıkış başarılı");
                }
                
                return new ErrorResult("Çıkış işlemi başarısız");
            }
            catch (Exception ex)
            {
                return new ErrorResult($"Hata: {ex.Message}");
            }
        }

        public async Task<IDataResult<AuthUserDto>> GetCurrentUserAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("auth/current-user");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<AuthUserDto>(content);
                    
                    if (user != null)
                        return new SuccessDataResult<AuthUserDto>(user);
                }
                
                return new ErrorDataResult<AuthUserDto>("Kullanıcı bilgileri getirilemedi");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<AuthUserDto>($"Hata: {ex.Message}");
            }
        }
    }
}
