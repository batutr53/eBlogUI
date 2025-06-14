using eBlog.Application.DTOs.Auth;
using eBlog.Shared.Results;
using eBlogUI.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace eBlogUI.Business.Services
{
    public class AuthApiManager : IAuthApiService
    {
        private readonly HttpClient _httpClient;

        public AuthApiManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DataResult<string>> LoginAsync(LoginDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/login", dto);
            var json = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<DataResult<string>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
                return result!;
            }

            return new ErrorDataResult<string>("Kullanıcı adı veya şifre hatalı.");
        }

    }
}
