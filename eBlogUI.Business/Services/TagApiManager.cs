using eBlogUI.Business.Interfaces;
using eBlogUI.Models.Dtos;
using eBlogUI.Models.Dtos.Tag;
using Newtonsoft.Json;
using eBlog.Shared.Results;
using System.Text;

namespace eBlogUI.Business.Services
{
    public class TagApiManager : ITagApiService
    {
        private readonly HttpClient _httpClient;

        public TagApiManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IDataResult<List<TagListDto>>> GetListAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("tags");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<TagListDto>>>(content);
                    
                    if (apiResponse != null && apiResponse.Success && apiResponse.Data != null)
                    {
                        return new SuccessDataResult<List<TagListDto>>(apiResponse.Data);
                    }
                    
                    return new ErrorDataResult<List<TagListDto>>(apiResponse?.Message ?? "Etiketler getirilemedi");
                }
                
                return new ErrorDataResult<List<TagListDto>>("API çağrısı başarısız oldu");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<TagListDto>>($"Hata: {ex.Message}");
            }
        }

        public async Task<IDataResult<TagListDto>> GetByIdAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"tags/{id}");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<TagListDto>>(content);
                    
                    if (apiResponse != null && apiResponse.Success && apiResponse.Data != null)
                    {
                        return new SuccessDataResult<TagListDto>(apiResponse.Data);
                    }
                    
                    return new ErrorDataResult<TagListDto>(apiResponse?.Message ?? "Etiket bulunamadı");
                }
                
                return new ErrorDataResult<TagListDto>("API çağrısı başarısız oldu");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<TagListDto>($"Hata: {ex.Message}");
            }
        }

        public async Task<IDataResult<TagListDto>> GetBySlugAsync(string slug)
        {
            try
            {
                var response = await _httpClient.GetAsync($"tags/slug/{slug}");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<TagListDto>>(content);
                    
                    if (apiResponse != null && apiResponse.Success && apiResponse.Data != null)
                    {
                        return new SuccessDataResult<TagListDto>(apiResponse.Data);
                    }
                    
                    return new ErrorDataResult<TagListDto>(apiResponse?.Message ?? "Etiket bulunamadı");
                }
                
                return new ErrorDataResult<TagListDto>("API çağrısı başarısız oldu");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<TagListDto>($"Hata: {ex.Message}");
            }
        }

        public async Task<IResult> CreateAsync(TagCreateDto dto)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(dto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync("tags", content);
                
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseContent);
                    
                    if (apiResponse != null && apiResponse.Success)
                    {
                        return new SuccessResult(apiResponse.Message ?? "Etiket başarıyla oluşturuldu");
                    }
                    
                    return new ErrorResult(apiResponse?.Message ?? "Etiket oluşturulamadı");
                }
                
                return new ErrorResult("API çağrısı başarısız oldu");
            }
            catch (Exception ex)
            {
                return new ErrorResult($"Hata: {ex.Message}");
            }
        }

        public async Task<IResult> UpdateAsync(TagUpdateDto dto)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(dto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PutAsync($"tags/{dto.Id}", content);
                
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseContent);
                    
                    if (apiResponse != null && apiResponse.Success)
                    {
                        return new SuccessResult(apiResponse.Message ?? "Etiket başarıyla güncellendi");
                    }
                    
                    return new ErrorResult(apiResponse?.Message ?? "Etiket güncellenemedi");
                }
                
                return new ErrorResult("API çağrısı başarısız oldu");
            }
            catch (Exception ex)
            {
                return new ErrorResult($"Hata: {ex.Message}");
            }
        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"tags/{id}");
                
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseContent);
                    
                    if (apiResponse != null && apiResponse.Success)
                    {
                        return new SuccessResult(apiResponse.Message ?? "Etiket başarıyla silindi");
                    }
                    
                    return new ErrorResult(apiResponse?.Message ?? "Etiket silinemedi");
                }
                
                return new ErrorResult("API çağrısı başarısız oldu");
            }
            catch (Exception ex)
            {
                return new ErrorResult($"Hata: {ex.Message}");
            }
        }

        public async Task<IDataResult<int>> GetPostCountByTagAsync(Guid tagId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"tags/{tagId}/post-count");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<int>>(content);
                    
                    if (apiResponse != null && apiResponse.Success)
                    {
                        return new SuccessDataResult<int>(apiResponse.Data);
                    }
                    
                    return new ErrorDataResult<int>(apiResponse?.Message ?? "Post sayısı alınamadı");
                }
                
                return new ErrorDataResult<int>("API çağrısı başarısız oldu");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<int>($"Hata: {ex.Message}");
            }
        }

        public async Task<IDataResult<List<TagListDto>>> GetPopularTagsAsync(int count = 10)
        {
            try
            {
                var response = await _httpClient.GetAsync($"tags/popular?count={count}");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<TagListDto>>>(content);
                    
                    if (apiResponse != null && apiResponse.Success && apiResponse.Data != null)
                    {
                        return new SuccessDataResult<List<TagListDto>>(apiResponse.Data);
                    }
                    
                    return new ErrorDataResult<List<TagListDto>>(apiResponse?.Message ?? "Popüler etiketler getirilemedi");
                }
                
                return new ErrorDataResult<List<TagListDto>>("API çağrısı başarısız oldu");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<TagListDto>>($"Hata: {ex.Message}");
            }
        }
    }
}
