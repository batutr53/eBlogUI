namespace eBlog.Shared.Results
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T? data, string? message = null) : base(data, false, message) { }
        public ErrorDataResult(string? message = null) : base(default, false, message) { }
    }
}
