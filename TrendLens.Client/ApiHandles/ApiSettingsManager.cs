namespace TrendLens.Client.ApiHandles
{
    public class ApiSettingsManager
    {
        private readonly Dictionary<string, ApiServiceSettings> _settings;

        public ApiSettingsManager()
        {
            _settings = new Dictionary<string, ApiServiceSettings>
        {
            { "Twitter", new ApiServiceSettings { BaseUrl = "https://api.twitter.com/2/", AuthKey = "twitter-auth-key" } },
            { "Instagram", new ApiServiceSettings { BaseUrl = "https://graph.instagram.com/", AuthKey = "instagram-auth-key" } }
            // Add more services as needed
        };
        }

        public ApiServiceSettings GetSettings(string serviceName)
        {
            return _settings.TryGetValue(serviceName, out var settings) ? settings : null;
        }
    }

}
