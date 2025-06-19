namespace eBlogUI.Models.Dtos
{
    public class ApiResponse<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; }
        public List<string>? Errors { get; set; }
    }

    public class ApiResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; }
        public List<string>? Errors { get; set; }
    }
}
