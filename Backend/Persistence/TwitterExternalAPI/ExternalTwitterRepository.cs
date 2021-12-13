using System;
using System.Threading.Tasks;
using Application.Persistence;
using Persistence.Repositories;
using RestSharp;
using RestSharp.Authenticators;
using Tweetinvi;
using Tweetinvi.Auth;
using Tweetinvi.Parameters;
using Tweet = Domain.Entities.Tweet;

namespace Persistence.TwitterExternalAPI
{
    public class ExternalTwitterRepository : MlRepository, IExternalTweetRepository
    {
        private readonly ITwitterHelper _twitterHelper;
        private static IAuthenticationRequestStore MyAuthRequestStore = MyAuthRequestStore = new LocalAuthenticationRequestStore();

        public ExternalTwitterRepository(ITwitterHelper twitterHelper)
        {
            _twitterHelper = twitterHelper;
        }
        
        public async Task<Tweet> GetTweetById(string id, bool unitTest = false)
        {
            var tweetResponse = await _twitterHelper._twitterClient.TweetsV2.GetTweetAsync(id);
            var tweetV2 = tweetResponse.Tweet;
            Tweet tweet = new Tweet();
            tweet.Text = tweetV2.Text;
            tweet.Date = tweetV2.CreatedAt.ToString();

            var userResponse = await _twitterHelper._twitterClient.UsersV2.GetUserByIdAsync(tweetResponse.Tweet.AuthorId);
            var user = userResponse.User;
            tweet.User = user.Name;
            tweet.Username = user.Username;
            if (!unitTest)
            {
                tweet.feels = PredictSentiment(tweet.Text).Result;

            }
            return tweet;
        }

        public async Task<string> PostToGetAuth()
        {
            var appClient = _twitterHelper._twitterClient;
            var authenticationRequestId = Guid.NewGuid().ToString();
            
            #pragma warning disable S1075 // URIs should not be hardcoded   
            const string redirectPath = "https://localhost:7225/signin";

            var redirectUrl = MyAuthRequestStore.AppendAuthenticationRequestIdToCallbackUrl(redirectPath, authenticationRequestId);
   
            var authenticationRequestToken = await appClient.Auth.RequestAuthenticationUrlAsync(redirectUrl);

            await MyAuthRequestStore.AddAuthenticationTokenAsync(authenticationRequestId, authenticationRequestToken);
            
            return authenticationRequestToken.AuthorizationURL;
        }

        public async Task<string> ValidateAuth(string queryValue)
        {
            var appClient = _twitterHelper._twitterClient;
            
            var requestParameters = await RequestCredentialsParameters.FromCallbackUrlAsync(queryValue, MyAuthRequestStore);
            
            var userCreds = await appClient.Auth.RequestCredentialsAsync(requestParameters);

            var userClient = new TwitterClient(userCreds.ConsumerKey,
                                               userCreds.ConsumerSecret,
                                               userCreds.AccessToken,
                                      userCreds.AccessTokenSecret);
            
            var user = await userClient.Users.GetAuthenticatedUserAsync();
            
            #pragma warning disable S1075 // URIs should not be hardcoded   
            var client = new RestClient("https://api.twitter.com/2/users/" + user.Id + "/tweets")
            {
                Authenticator = OAuth1Authenticator.ForAccessToken(
                    userCreds.ConsumerKey, 
                    userCreds.ConsumerSecret,
                    userCreds.AccessToken, 
                    userCreds.AccessTokenSecret)
            };

            var request = new RestRequest("", DataFormat.Json);
            request.AddHeader("content-type", "application/json");
            
            return (await client.ExecuteAsync(request)).Content;
        }
        
    }
}