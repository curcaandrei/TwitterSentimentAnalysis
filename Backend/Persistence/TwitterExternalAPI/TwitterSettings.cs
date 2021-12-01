namespace Persistence.TwitterExternalAPI
{
    public class TwitterSettings
    {
        public string apiKey { get; set; }
        public string apiSecret { get; set; }
        public string accessToken { get; set; }
        public string accessSecret { get; set; }

        public TwitterSettings(string apiKey, string apiSecret, string accessToken, string accessSecret)
        {
            this.apiKey = apiKey;
            this.apiSecret = apiSecret;
            this.accessToken = accessToken;
            this.accessSecret = accessSecret;
        }
    }
}