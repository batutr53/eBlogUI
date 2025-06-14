using eBlog.Application.DTOs;
using eBlog.Shared.Results;
using eBlogUI.Business.Interfaces;
using System.Text;
using System.Text.Json;

namespace eBlogUI.Business.Services
{
    public class PostApiManager : IPostApiService
    {
        private readonly HttpClient _httpClient;

        public PostApiManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DataResult<List<PostListDto>>> GetListAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("post");
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<DataResult<List<PostListDto>>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<PostListDto>>($"Post listesi alınamadı: {ex.Message}");
            }
        }

        public async Task<DataResult<PostDetailDto>> GetDetailAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"post/{id}");
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<DataResult<PostDetailDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<PostDetailDto>($"Post detayı alınamadı: {ex.Message}");
            }
        }

        public async Task<Result> CreateAsync(PostCreateDto dto)
        {
            try
            {
                var json = JsonSerializer.Serialize(dto);
                var response = await _httpClient.PostAsync("post", new StringContent(json, Encoding.UTF8, "application/json"));
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Result>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
            }
            catch (Exception ex)
            {
                return new ErrorResult($"Post oluşturulurken hata oluştu: {ex.Message}");
            }
        }

        public async Task<Result> UpdateAsync(Guid id, PostUpdateDto dto)
        {
            try
            {
                var json = JsonSerializer.Serialize(dto);
                var response = await _httpClient.PutAsync($"post/{id}", new StringContent(json, Encoding.UTF8, "application/json"));
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Result>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
            }
            catch (Exception ex)
            {
                return new ErrorResult($"Post güncellenemedi: {ex.Message}");
            }
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"post/{id}");
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Result>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
            }
            catch (Exception ex)
            {
                return new ErrorResult($"Post silinemedi: {ex.Message}");
            }
        }
    }
}
