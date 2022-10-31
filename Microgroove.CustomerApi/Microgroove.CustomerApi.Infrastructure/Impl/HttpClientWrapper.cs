using Newtonsoft.Json;

namespace Microgroove.CustomerApi.Infrastructure.Impl
{
    /// <summary>
    /// A generic wrapper class to REST API calls.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class HttpClientWrapper<TRequest, TResponse> : IHttpClientWrapper<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        private readonly HttpClient _httpClient;

        public HttpClientWrapper(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        /// <summary>
        /// For getting the resources from a web api.
        /// </summary>
        /// <param name="url">API Url</param>
        /// <returns>A Task with result object of type TResponse.</returns>
        public async Task<TResponse> GetAsync(string url)
        {
            var result = default(TResponse);

            var response = await _httpClient.GetAsync(new Uri(url));

            response.EnsureSuccessStatusCode();

            await response.Content.ReadAsStringAsync()
                .ContinueWith((x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception ?? new Exception("HttpClientWrapper uknown exception.");

                    if (typeof(TResponse) == typeof(string))
                    {
                        result = x.Result as TResponse;
                    }
                    else 
                    {
                        result = JsonConvert.DeserializeObject<TResponse>(x.Result);
                    }
                });

            return result;
        }
    }
}
