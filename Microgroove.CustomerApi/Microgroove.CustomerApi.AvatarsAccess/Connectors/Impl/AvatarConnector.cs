using Microgroove.CustomerApi.AvatarsAccess.Settings;
using Microgroove.CustomerApi.Infrastructure;
using Microsoft.Extensions.Options;
using System.Web;

namespace Microgroove.CustomerApi.AvatarsAccess.Connectors.Impl
{
    public class AvatarConnector : IAvatarConnector
    {
        private readonly IHttpClientWrapper<string, string> _httpClientWrapper;
        private readonly AvatarApiSettings _avatarApiSettings;

        public AvatarConnector(IHttpClientWrapper<string, string> httpClientWrapper, IOptions<AvatarApiSettings> avatarApiOptions)
        {
            _httpClientWrapper = httpClientWrapper ?? throw new ArgumentNullException(nameof(httpClientWrapper));
            _avatarApiSettings = avatarApiOptions?.Value ?? throw new ArgumentNullException(nameof(avatarApiOptions));
        }

        public Task<string> GetProfileImageAsync(string name)
        {
            return _httpClientWrapper.GetAsync($"{_avatarApiSettings.Url}/?name={name}&format=svg");
        }
    }
}
