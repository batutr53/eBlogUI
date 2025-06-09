namespace eBlog.Shared.Results
{
    public class SuccessResult : Result
    {
        public SuccessResult(string? message = null) : base(true, message) { }
    }
}
