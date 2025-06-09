namespace eBlog.Shared.Results
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T? data, string? message = null) : base(data, true, message) { }
    }
}
