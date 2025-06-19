using eBlogUI.Business.Interfaces;
using eBlogUI.Models.Dtos;
using eBlogUI.Models.Dtos.Category;
using Newtonsoft.Json;
using eBlog.Shared.Results;
using System.Text;

namespace eBlogUI.Business.Services
{
    public class CategoryApiManager : ICategoryApiService
    {
        private readonly HttpClient _httpClient;

        public CategoryApiManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IDataResult<List<CategoryListDto>>> GetListAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("categories");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<CategoryListDto>>>(content);
                    
                    if (apiResponse != null && apiResponse.Success && apiResponse.Data != null)
                    {
                        return new SuccessDataResult<List<CategoryListDto>>(apiResponse.Data);
                    }
                    
                    return new ErrorDataResult<List<CategoryListDto>>(apiResponse?.Message ?? "Kategoriler getirilemedi");
                }
                
                return new ErrorDataResult<List<CategoryListDto>>("API çağrısı başarısız oldu");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<CategoryListDto>>($"Hata: {ex.Message}");
            }
        }

        public async Task<IDataResult<CategoryListDto>> GetByIdAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"categories/{id}");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<CategoryListDto>>(content);
                    
                    if (apiResponse != null && apiResponse.Success && apiResponse.Data != null)
                    {
                        return new SuccessDataResult<CategoryListDto>(apiResponse.Data);
                    }
                    
                    return new ErrorDataResult<CategoryListDto>(apiResponse?.Message ?? "Kategori bulunamadı");
                }
                
                return new ErrorDataResult<CategoryListDto>("API çağrısı başarısız oldu");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<CategoryListDto>($"Hata: {ex.Message}");
            }
        }

        public async Task<IDataResult<CategoryListDto>> GetBySlugAsync(string slug)
        {
            try
            {
                var response = await _httpClient.GetAsync($"categories/slug/{slug}");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<CategoryListDto>>(content);
                    
                    if (apiResponse != null && apiResponse.Success && apiResponse.Data != null)
                    {
                        return new SuccessDataResult<CategoryListDto>(apiResponse.Data);
                    }
                    
                    return new ErrorDataResult<CategoryListDto>(apiResponse?.Message ?? "Kategori bulunamadı");
                }
                
                return new ErrorDataResult<CategoryListDto>("API çağrısı başarısız oldu");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<CategoryListDto>($"Hata: {ex.Message}");
            }
        }

        public async Task<IResult> CreateAsync(CategoryCreateDto dto)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(dto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync("categories", content);
                
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseContent);
                    
                    if (apiResponse != null && apiResponse.Success)
                    {
                        return new SuccessResult(apiResponse.Message ?? "Kategori başarıyla oluşturuldu");
                    }
                    
                    return new ErrorResult(apiResponse?.Message ?? "Kategori oluşturulamadı");
                }
                
                return new ErrorResult("API çağrısı başarısız oldu");
            }
            catch (Exception ex)
            {
                return new ErrorResult($"Hata: {ex.Message}");
            }
        }

        public async Task<IResult> UpdateAsync(CategoryUpdateDto dto)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(dto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PutAsync($"categories/{dto.Id}", content);
                
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseContent);
                    
                    if (apiResponse != null && apiResponse.Success)
                    {
                        return new SuccessResult(apiResponse.Message ?? "Kategori başarıyla güncellendi");
                    }
                    
                    return new ErrorResult(apiResponse?.Message ?? "Kategori güncellenemedi");
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
                var response = await _httpClient.DeleteAsync($"categories/{id}");
                
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseContent);
                    
                    if (apiResponse != null && apiResponse.Success)
                    {
                        return new SuccessResult(apiResponse.Message ?? "Kategori başarıyla silindi");
                    }
                    
                    return new ErrorResult(apiResponse?.Message ?? "Kategori silinemedi");
                }
                
                return new ErrorResult("API çağrısı başarısız oldu");
            }
            catch (Exception ex)
            {
                return new ErrorResult($"Hata: {ex.Message}");
            }
        }

        public async Task<IDataResult<int>> GetPostCountByCategoryAsync(Guid categoryId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"categories/{categoryId}/post-count");
                
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
    }
}
