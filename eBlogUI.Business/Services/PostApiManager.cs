using eBlogUI.Business.Interfaces;
using eBlogUI.Models.Dtos;
using eBlogUI.Models.Dtos.Post;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using eBlog.Shared.Results;
using System.Net;
using System.Text;

namespace eBlogUI.Business.Services
{
    public class PostApiManager : IPostApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PostApiManager> _logger;

        public PostApiManager(HttpClient httpClient, ILogger<PostApiManager> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IDataResult<List<PostListDto>>> GetListAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("post");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<PostListDto>>>(content);
                    
                    if (apiResponse != null && apiResponse.Success && apiResponse.Data != null)
                    {
                        return new SuccessDataResult<List<PostListDto>>(apiResponse.Data);
                    }
                    
                    return new ErrorDataResult<List<PostListDto>>(apiResponse?.Message ?? "Postlar getirilemedi");
                }
                
                return new ErrorDataResult<List<PostListDto>>("API çağrısı başarısız oldu");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Postlar getirilirken hata oluştu");
                return new ErrorDataResult<List<PostListDto>>($"Hata: {ex.Message}");
            }
        }

        public async Task<IDataResult<List<PostListDto>>> GetPublishedPostsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("post/published");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<PostListDto>>>(content);
                    
                    if (apiResponse != null && apiResponse.Success && apiResponse.Data != null)
                    {
                        return new SuccessDataResult<List<PostListDto>>(apiResponse.Data);
                    }
                    
                    return new ErrorDataResult<List<PostListDto>>(apiResponse?.Message ?? "Yayınlanan postlar getirilemedi");
                }
                
                return new ErrorDataResult<List<PostListDto>>("API çağrısı başarısız oldu");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Yayınlanan postlar getirilirken hata oluştu");
                return new ErrorDataResult<List<PostListDto>>($"Hata: {ex.Message}");
            }
        }

        public async Task<IDataResult<List<PostListDto>>> GetPostsByAuthorAsync(Guid authorId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"post/author/{authorId}");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<PostListDto>>>(content);
                    
                    if (apiResponse != null && apiResponse.Success && apiResponse.Data != null)
                    {
                        return new SuccessDataResult<List<PostListDto>>(apiResponse.Data);
                    }
                    
                    return new ErrorDataResult<List<PostListDto>>(apiResponse?.Message ?? "Yazar postları getirilemedi");
                }
                
                return new ErrorDataResult<List<PostListDto>>("API çağrısı başarısız oldu");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Yazar postları getirilirken hata oluştu");
                return new ErrorDataResult<List<PostListDto>>($"Hata: {ex.Message}");
            }
        }

        public async Task<IDataResult<List<PostListDto>>> GetPostsByCategoryAsync(Guid categoryId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"post/category/{categoryId}");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<PostListDto>>>(content);
                    
                    if (apiResponse != null && apiResponse.Success && apiResponse.Data != null)
                    {
                        return new SuccessDataResult<List<PostListDto>>(apiResponse.Data);
                    }
                    
                    return new ErrorDataResult<List<PostListDto>>(apiResponse?.Message ?? "Kategori postları getirilemedi");
                }
                
                return new ErrorDataResult<List<PostListDto>>("API çağrısı başarısız oldu");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kategori postları getirilirken hata oluştu");
                return new ErrorDataResult<List<PostListDto>>($"Hata: {ex.Message}");
            }
        }

        public async Task<IDataResult<PostDetailDto>> GetDetailAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"post/{id}");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<PostDetailDto>>(content);
                    
                    if (apiResponse != null && apiResponse.Success && apiResponse.Data != null)
                    {
                        return new SuccessDataResult<PostDetailDto>(apiResponse.Data);
                    }
                    
                    return new ErrorDataResult<PostDetailDto>(apiResponse?.Message ?? "Post bulunamadı");
                }
                
                return new ErrorDataResult<PostDetailDto>("API çağrısı başarısız oldu");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Post detayı getirilirken hata oluştu");
                return new ErrorDataResult<PostDetailDto>($"Hata: {ex.Message}");
            }
        }

        public async Task<IDataResult<PostDetailDto>> GetDetailBySlugAsync(string slug)
        {
            try
            {
                var response = await _httpClient.GetAsync($"post/slug/{slug}");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<PostDetailDto>>(content);
                    
                    if (apiResponse != null && apiResponse.Success && apiResponse.Data != null)
                    {
                        return new SuccessDataResult<PostDetailDto>(apiResponse.Data);
                    }
                    
                    return new ErrorDataResult<PostDetailDto>(apiResponse?.Message ?? "Post bulunamadı");
                }
                
                return new ErrorDataResult<PostDetailDto>("API çağrısı başarısız oldu");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Post detayı getirilirken hata oluştu");
                return new ErrorDataResult<PostDetailDto>($"Hata: {ex.Message}");
            }
        }

        public async Task<IResult> CreateAsync(PostCreateDto dto)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(dto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync("post", content);
                
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseContent);
                    
                    if (apiResponse != null && apiResponse.Success)
                    {
                        return new SuccessResult(apiResponse.Message ?? "Post başarıyla oluşturuldu");
                    }
                    
                    return new ErrorResult(apiResponse?.Message ?? "Post oluşturulamadı");
                }
                
                return new ErrorResult("API çağrısı başarısız oldu");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Post oluşturulurken hata oluştu");
                return new ErrorResult($"Hata: {ex.Message}");
            }
        }

        public async Task<IResult> UpdateAsync(Guid id, PostUpdateDto dto)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(dto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PutAsync($"post/{id}", content);
                
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseContent);
                    
                    if (apiResponse != null && apiResponse.Success)
                    {
                        return new SuccessResult(apiResponse.Message ?? "Post başarıyla güncellendi");
                    }
                    
                    return new ErrorResult(apiResponse?.Message ?? "Post güncellenemedi");
                }
                
                return new ErrorResult("API çağrısı başarısız oldu");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Post güncellenirken hata oluştu");
                return new ErrorResult($"Hata: {ex.Message}");
            }
        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"post/{id}");
                
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseContent);
                    
                    if (apiResponse != null && apiResponse.Success)
                    {
                        return new SuccessResult(apiResponse.Message ?? "Post başarıyla silindi");
                    }
                    
                    return new ErrorResult(apiResponse?.Message ?? "Post silinemedi");
                }
                
                return new ErrorResult("API çağrısı başarısız oldu");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Post silinirken hata oluştu");
                return new ErrorResult($"Hata: {ex.Message}");
            }
        }

        public async Task<IDataResult<List<PostListDto>>> SearchPostsAsync(string searchTerm)
        {
            try
            {
                var response = await _httpClient.GetAsync($"post/search?q={searchTerm}");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<PostListDto>>>(content);
                    
                    if (apiResponse != null && apiResponse.Success && apiResponse.Data != null)
                    {
                        return new SuccessDataResult<List<PostListDto>>(apiResponse.Data);
                    }
                    
                    return new ErrorDataResult<List<PostListDto>>(apiResponse?.Message ?? "Arama sonucu bulunamadı");
                }
                
                return new ErrorDataResult<List<PostListDto>>("API çağrısı başarısız oldu");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Post arama işlemi sırasında hata oluştu");
                return new ErrorDataResult<List<PostListDto>>($"Hata: {ex.Message}");
            }
        }

        public async Task<IDataResult<List<PostListDto>>> GetRecentPostsAsync(int count = 10)
        {
            try
            {
                var response = await _httpClient.GetAsync($"post/recent?count={count}");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<PostListDto>>>(content);
                    
                    if (apiResponse != null && apiResponse.Success && apiResponse.Data != null)
                    {
                        return new SuccessDataResult<List<PostListDto>>(apiResponse.Data);
                    }
                    
                    return new ErrorDataResult<List<PostListDto>>(apiResponse?.Message ?? "Son postlar getirilemedi");
                }
                
                return new ErrorDataResult<List<PostListDto>>("API çağrısı başarısız oldu");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Son postlar getirilirken hata oluştu");
                return new ErrorDataResult<List<PostListDto>>($"Hata: {ex.Message}");
            }
        }

        public async Task<IDataResult<List<PostListDto>>> GetPopularPostsAsync(int count = 10)
        {
            try
            {
                var response = await _httpClient.GetAsync($"post/popular?count={count}");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<PostListDto>>>(content);
                    
                    if (apiResponse != null && apiResponse.Success && apiResponse.Data != null)
                    {
                        return new SuccessDataResult<List<PostListDto>>(apiResponse.Data);
                    }
                    
                    return new ErrorDataResult<List<PostListDto>>(apiResponse?.Message ?? "Popüler postlar getirilemedi");
                }
                
                return new ErrorDataResult<List<PostListDto>>("API çağrısı başarısız oldu");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Popüler postlar getirilirken hata oluştu");
                return new ErrorDataResult<List<PostListDto>>($"Hata: {ex.Message}");
            }
        }

        public async Task<IDataResult<PostDetailDto>> GetPostForSeoAsync(string slug)
        {
            try
            {
                var response = await _httpClient.GetAsync($"post/seo/{slug}");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<PostDetailDto>>(content);
                    
                    if (apiResponse != null && apiResponse.Success && apiResponse.Data != null)
                    {
                        return new SuccessDataResult<PostDetailDto>(apiResponse.Data);
                    }
                    
                    return new ErrorDataResult<PostDetailDto>(apiResponse?.Message ?? "Post bulunamadı");
                }
                
                return new ErrorDataResult<PostDetailDto>("API çağrısı başarısız oldu");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SEO için post getirilirken hata oluştu");
                return new ErrorDataResult<PostDetailDto>($"Hata: {ex.Message}");
            }
        }

        public async Task<IResult> UpdatePostSeoAsync(Guid id, SeoMetadataDto seoDto)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(seoDto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PutAsync($"post/{id}/seo", content);
                
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseContent);
                    
                    if (apiResponse != null && apiResponse.Success)
                    {
                        return new SuccessResult(apiResponse.Message ?? "SEO bilgileri başarıyla güncellendi");
                    }
                    
                    return new ErrorResult(apiResponse?.Message ?? "SEO bilgileri güncellenemedi");
                }
                
                return new ErrorResult("API çağrısı başarısız oldu");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SEO bilgileri güncellenirken hata oluştu");
                return new ErrorResult($"Hata: {ex.Message}");
            }
        }
    }
}
