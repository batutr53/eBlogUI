namespace eBlog.Shared.Results
{
    public interface IDataResult<T> : IResult
    {
        T? Data { get; }
    }
}
