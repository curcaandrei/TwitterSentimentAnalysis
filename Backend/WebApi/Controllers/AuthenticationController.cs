using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using RestSharp.Authenticators;
using Tweetinvi;
using Tweetinvi.Auth;
using Tweetinvi.Parameters;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private static readonly IAuthenticationRequestStore MyAuthRequestStore = new LocalAuthenticationRequestStore();

        [HttpPost("/signin", Name = "Twitter Authentication")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<string> TwitterAuth()
        {
            var appClient = new TwitterClient("q7mkGsaBL9YggmwtpgRPnoqIo", "xwozMoQJAEUnpCSGJlv7y3cDILCPYyhZgSUzzWMODrBxENGXEW");
            var authenticationRequestId = Guid.NewGuid().ToString();
            var redirectPath = Request.Scheme + "://" + Request.Host.Value + "/signin";
            Console.WriteLine(redirectPath);
            
            var redirectURL = MyAuthRequestStore.AppendAuthenticationRequestIdToCallbackUrl(redirectPath, authenticationRequestId);
   
            var authenticationRequestToken = await appClient.Auth.RequestAuthenticationUrlAsync(redirectURL);

            await MyAuthRequestStore.AddAuthenticationTokenAsync(authenticationRequestId, authenticationRequestToken);
            Console.WriteLine(authenticationRequestToken);
            Console.WriteLine(authenticationRequestId);

            return authenticationRequestToken.AuthorizationURL;
        }

        [HttpGet("/signin", Name = "Twitter Authentication Validator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<string> ValidateTwitterAuth()
        {
            var appClient = new TwitterClient("q7mkGsaBL9YggmwtpgRPnoqIo", "xwozMoQJAEUnpCSGJlv7y3cDILCPYyhZgSUzzWMODrBxENGXEW");
            
            var requestParameters = await RequestCredentialsParameters.FromCallbackUrlAsync(Request.QueryString.Value, MyAuthRequestStore);
            
            var userCreds = await appClient.Auth.RequestCredentialsAsync(requestParameters);

            var userClient = new TwitterClient(userCreds.ConsumerKey,userCreds.ConsumerSecret,userCreds.AccessToken,userCreds.AccessTokenSecret);
            var user = await userClient.Users.GetAuthenticatedUserAsync();
            var client = new RestClient("https://api.twitter.com/2/users/"+user.Id +"/tweets");
            client.Authenticator = OAuth1Authenticator.ForAccessToken(userCreds.ConsumerKey, userCreds.ConsumerSecret,
                userCreds.AccessToken, userCreds.AccessTokenSecret);
            var request = new RestRequest("", DataFormat.Json);
            request.AddHeader("content-type", "application/json");
            return client.Execute(request).Content;
        }
    }
}