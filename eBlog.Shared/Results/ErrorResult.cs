namespace eBlog.Shared.Results
{
    public class ErrorResult : Result
    {
        public ErrorResult(string? message = null) : base(false, message) { }
    }
}
