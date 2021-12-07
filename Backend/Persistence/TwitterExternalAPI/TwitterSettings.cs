namespace Persistence.TwitterExternalAPI
{
    public class TwitterSettings
    {
        public string ApiKey { get; set; } = "No key";
        public string ApiSecret { get; set; } = "No Secret";
        public string AccessToken { get; set; } = "No Access Token";
        public string AccessSecret { get; set; } = "No Access Secret";

        public TwitterSettings(string apiKey, string apiSecret, string accessToken, string accessSecret)
        {
            this.ApiKey = apiKey;
            this.ApiSecret = apiSecret;
            this.AccessToken = accessToken;
            this.AccessSecret = accessSecret;
        }

        public TwitterSettings()
        {
            
        }
    }
}