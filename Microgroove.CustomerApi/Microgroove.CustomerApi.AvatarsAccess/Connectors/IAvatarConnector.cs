namespace Microgroove.CustomerApi.AvatarsAccess.Connectors
{
    public interface IAvatarConnector
    {
        Task<string> GetProfileImageAsync(string name);
    }
}
