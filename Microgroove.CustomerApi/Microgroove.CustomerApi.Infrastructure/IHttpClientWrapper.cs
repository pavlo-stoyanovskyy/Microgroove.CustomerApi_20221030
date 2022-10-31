namespace Microgroove.CustomerApi.Infrastructure
{
    public interface IHttpClientWrapper<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        Task<TResponse> GetAsync(string url);
    }
}
