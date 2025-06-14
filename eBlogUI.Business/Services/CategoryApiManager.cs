using eBlog.Application.DTOs;
using eBlog.Shared.Results;
using eBlogUI.Business.Services.Interfaces;
using System.Text.Json;

namespace eBlogUI.Business.Services
{
    public class CategoryApiManager : ICategoryApiService
    {
        private readonly HttpClient _httpClient;

        public CategoryApiManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DataResult<List<CategoryListDto>>> GetListAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("category");
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<DataResult<List<CategoryListDto>>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<CategoryListDto>>("Kategori listesi alınamadı: " + ex.Message);
            }
        }
    }
}
