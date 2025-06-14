using eBlog.Application.DTOs;
using eBlog.Shared.Results;
using eBlogUI.Business.Interfaces;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text;
using System.Text.Json;

namespace eBlogUI.Business.Services
{
    public class PostApiManager : IPostApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PostApiManager> _logger;
        private readonly JsonSerializerOptions _jsonOptions;

        public PostApiManager(HttpClient httpClient, ILogger<PostApiManager> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        // ✅ Liste metodları
        public async Task<DataResult<List<PostListDto>>> GetListAsync()
        {
            try
            {
                _logger.LogInformation("Post listesi API'den alınıyor...");

                var response = await _httpClient.GetAsync("post");

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"API isteği başarısız: {response.StatusCode} - {response.ReasonPhrase}");
                    return new ErrorDataResult<List<PostListDto>>($"Post listesi alınamadı: {response.ReasonPhrase}");
                }

                var content = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(content))
                {
                    _logger.LogWarning("API'den boş response alındı");
                    return new SuccessDataResult<List<PostListDto>>(new List<PostListDto>(), "Post listesi boş.");
                }

                var result = JsonSerializer.Deserialize<DataResult<List<PostListDto>>>(content, _jsonOptions);

                if (result == null)
                {
                    _logger.LogError("JSON deserialize edilemedi");
                    return new ErrorDataResult<List<PostListDto>>("Veri çözümlenemedi.");
                }

                _logger.LogInformation($"{result.Data?.Count ?? 0} post başarıyla alındı");
                return result;
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "HTTP isteği sırasında hata oluştu");
                return new ErrorDataResult<List<PostListDto>>("Bağlantı hatası. Lütfen daha sonra tekrar deneyin.");
            }
            catch (TaskCanceledException tcEx)
            {
                _logger.LogError(tcEx, "İstek zaman aşımına uğradı");
                return new ErrorDataResult<List<PostListDto>>("İstek zaman aşımına uğradı.");
            }
            catch (JsonException jsonEx)
            {
                _logger.LogError(jsonEx, "JSON çözümleme hatası");
                return new ErrorDataResult<List<PostListDto>>("Veri formatı hatalı.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Beklenmeyen hata oluştu");
                return new ErrorDataResult<List<PostListDto>>("Beklenmeyen bir hata oluştu.");
            }
        }

        public async Task<DataResult<List<PostListDto>>> GetPublishedPostsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("post?status=published");
                return await ProcessListResponseAsync(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Yayınlanmış postlar alınırken hata");
                return new ErrorDataResult<List<PostListDto>>($"Yayınlanmış postlar alınamadı: {ex.Message}");
            }
        }

        public async Task<DataResult<List<PostListDto>>> GetPostsByAuthorAsync(Guid authorId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"post/author/{authorId}");
                return await ProcessListResponseAsync(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Yazar postları alınırken hata: {AuthorId}", authorId);
                return new ErrorDataResult<List<PostListDto>>($"Yazar postları alınamadı: {ex.Message}");
            }
        }

        public async Task<DataResult<List<PostListDto>>> GetPostsByCategoryAsync(Guid categoryId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"post/category/{categoryId}");
                return await ProcessListResponseAsync(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kategori postları alınırken hata: {CategoryId}", categoryId);
                return new ErrorDataResult<List<PostListDto>>($"Kategori postları alınamadı: {ex.Message}");
            }
        }

        // ✅ Detay metodları
        public async Task<DataResult<PostDetailDto>> GetDetailAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("Post detayı alınıyor: {PostId}", id);

                var response = await _httpClient.GetAsync($"post/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        _logger.LogWarning("Post bulunamadı: {PostId}", id);
                        return new ErrorDataResult<PostDetailDto>("Post bulunamadı.");
                    }

                    _logger.LogError("API isteği başarısız: {StatusCode} - {ReasonPhrase}",
                                   response.StatusCode, response.ReasonPhrase);
                    return new ErrorDataResult<PostDetailDto>($"Post detayı alınamadı: {response.ReasonPhrase}");
                }

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<DataResult<PostDetailDto>>(content, _jsonOptions);

                if (result?.Data?.SeoMetadata != null)
                {
                    _logger.LogInformation("Post detayı SEO metadata ile birlikte alındı: {PostId}", id);
                }
                else
                {
                    _logger.LogWarning("Post detayında SEO metadata bulunamadı: {PostId}", id);
                }

                return result ?? new ErrorDataResult<PostDetailDto>("Veri çözümlenemedi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Post detayı alınırken hata: {PostId}", id);
                return new ErrorDataResult<PostDetailDto>($"Post detayı alınamadı: {ex.Message}");
            }
        }

        public async Task<DataResult<PostDetailDto>> GetDetailBySlugAsync(string slug)
        {
            try
            {
                _logger.LogInformation("Post slug ile alınıyor: {Slug}", slug);

                var response = await _httpClient.GetAsync($"post/slug/{slug}");

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                        return new ErrorDataResult<PostDetailDto>("Post bulunamadı.");

                    return new ErrorDataResult<PostDetailDto>($"Post alınamadı: {response.ReasonPhrase}");
                }

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<DataResult<PostDetailDto>>(content, _jsonOptions);

                return result ?? new ErrorDataResult<PostDetailDto>("Veri çözümlenemedi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Post slug ile alınırken hata: {Slug}", slug);
                return new ErrorDataResult<PostDetailDto>($"Post alınamadı: {ex.Message}");
            }
        }

        // ✅ CRUD metodları
        public async Task<Result> CreateAsync(PostCreateDto dto)
        {
            try
            {
                _logger.LogInformation("Yeni post oluşturuluyor: {Title}", dto.Title);

                var json = JsonSerializer.Serialize(dto, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("post", content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Post oluşturma başarısız: {StatusCode} - {Content}",
                                   response.StatusCode, errorContent);
                    return new ErrorResult($"Post oluşturulamadı: {response.ReasonPhrase}");
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<Result>(responseContent, _jsonOptions);

                _logger.LogInformation("Post başarıyla oluşturuldu: {Title}", dto.Title);
                return result ?? new SuccessResult("Post oluşturuldu.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Post oluşturulurken hata: {Title}", dto.Title);
                return new ErrorResult($"Post oluşturulurken hata oluştu: {ex.Message}");
            }
        }

        public async Task<Result> UpdateAsync(Guid id, PostUpdateDto dto)
        {
            try
            {
                _logger.LogInformation("Post güncelleniyor: {PostId}", id);

                var json = JsonSerializer.Serialize(dto, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"post/{id}", content);

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                        return new ErrorResult("Güncellenecek post bulunamadı.");

                    return new ErrorResult($"Post güncellenemedi: {response.ReasonPhrase}");
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<Result>(responseContent, _jsonOptions);

                _logger.LogInformation("Post başarıyla güncellendi: {PostId}", id);
                return result ?? new SuccessResult("Post güncellendi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Post güncellenirken hata: {PostId}", id);
                return new ErrorResult($"Post güncellenirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("Post siliniyor: {PostId}", id);

                var response = await _httpClient.DeleteAsync($"post/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                        return new ErrorResult("Silinecek post bulunamadı.");

                    return new ErrorResult($"Post silinemedi: {response.ReasonPhrase}");
                }

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<Result>(content, _jsonOptions);

                _logger.LogInformation("Post başarıyla silindi: {PostId}", id);
                return result ?? new SuccessResult("Post silindi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Post silinirken hata: {PostId}", id);
                return new ErrorResult($"Post silinirken hata oluştu: {ex.Message}");
            }
        }

        // ✅ Özel metodlar
        public async Task<DataResult<List<PostListDto>>> SearchPostsAsync(string searchTerm)
        {
            try
            {
                var encodedSearchTerm = Uri.EscapeDataString(searchTerm);
                var response = await _httpClient.GetAsync($"post/search?q={encodedSearchTerm}");
                return await ProcessListResponseAsync(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Post arama hatası: {SearchTerm}", searchTerm);
                return new ErrorDataResult<List<PostListDto>>($"Arama yapılırken hata oluştu: {ex.Message}");
            }
        }

        public async Task<DataResult<List<PostListDto>>> GetRecentPostsAsync(int count = 10)
        {
            try
            {
                var response = await _httpClient.GetAsync($"post/recent?count={count}");
                return await ProcessListResponseAsync(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Son postlar alınırken hata");
                return new ErrorDataResult<List<PostListDto>>($"Son postlar alınamadı: {ex.Message}");
            }
        }

        public async Task<DataResult<List<PostListDto>>> GetPopularPostsAsync(int count = 10)
        {
            try
            {
                var response = await _httpClient.GetAsync($"post/popular?count={count}");
                return await ProcessListResponseAsync(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Popüler postlar alınırken hata");
                return new ErrorDataResult<List<PostListDto>>($"Popüler postlar alınamadı: {ex.Message}");
            }
        }

        // ✅ SEO metodları
        public async Task<DataResult<PostDetailDto>> GetPostForSeoAsync(string slug)
        {
            try
            {
                // SEO için özel endpoint (cache'lenmiş vs.)
                var response = await _httpClient.GetAsync($"post/seo/{slug}");

                if (!response.IsSuccessStatusCode)
                {
                    // Fallback: Normal slug metodunu kullan
                    return await GetDetailBySlugAsync(slug);
                }

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<DataResult<PostDetailDto>>(content, _jsonOptions);

                return result ?? new ErrorDataResult<PostDetailDto>("SEO verisi çözümlenemedi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SEO post verisi alınırken hata: {Slug}", slug);
                // Fallback: Normal metodu dene
                return await GetDetailBySlugAsync(slug);
            }
        }

        public async Task<Result> UpdatePostSeoAsync(Guid id, SeoMetadataDto seoDto)
        {
            try
            {
                var json = JsonSerializer.Serialize(seoDto, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"post/{id}/seo", content);

                if (!response.IsSuccessStatusCode)
                    return new ErrorResult($"SEO güncellenemedi: {response.ReasonPhrase}");

                return new SuccessResult("SEO bilgileri güncellendi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SEO güncellenirken hata: {PostId}", id);
                return new ErrorResult($"SEO güncellenirken hata oluştu: {ex.Message}");
            }
        }

        // ✅ Yardımcı metodlar
        private async Task<DataResult<List<PostListDto>>> ProcessListResponseAsync(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                return new ErrorDataResult<List<PostListDto>>($"API hatası: {response.ReasonPhrase}");
            }

            var content = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(content))
            {
                return new SuccessDataResult<List<PostListDto>>(new List<PostListDto>(), "Liste boş.");
            }

            var result = JsonSerializer.Deserialize<DataResult<List<PostListDto>>>(content, _jsonOptions);
            return result ?? new ErrorDataResult<List<PostListDto>>("Veri çözümlenemedi.");
        }

        private static string GetFriendlyErrorMessage(HttpStatusCode statusCode)
        {
            return statusCode switch
            {
                HttpStatusCode.NotFound => "İstenen kayıt bulunamadı.",
                HttpStatusCode.Unauthorized => "Bu işlem için yetkiniz bulunmuyor.",
                HttpStatusCode.Forbidden => "Bu işlemi gerçekleştirme izniniz yok.",
                HttpStatusCode.BadRequest => "Gönderilen veri hatalı.",
                HttpStatusCode.InternalServerError => "Sunucu hatası oluştu.",
                HttpStatusCode.ServiceUnavailable => "Servis şu anda kullanılamıyor.",
                HttpStatusCode.RequestTimeout => "İstek zaman aşımına uğradı.",
                _ => "Bilinmeyen bir hata oluştu."
            };
        }
    }
}