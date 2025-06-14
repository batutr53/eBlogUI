using eBlog.Application.DTOs;
using eBlog.Shared.Results;
using eBlogUI.Business.Services.Interfaces;
using System.Text.Json;

namespace eBlogUI.Business.Services
{
    public class TagApiManager : ITagApiService
    {
        private readonly HttpClient _httpClient;

        public TagApiManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DataResult<List<TagListDto>>> GetListAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("tag");
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<DataResult<List<TagListDto>>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<TagListDto>>("Etiketler alınamadı: " + ex.Message);
            }
        }
    }
}
