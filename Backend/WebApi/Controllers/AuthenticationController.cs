using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tweetinvi;
using Tweetinvi.Auth;
using Tweetinvi.Parameters;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private static readonly IAuthenticationRequestStore _myAuthRequestStore = new LocalAuthenticationRequestStore();

        [HttpPost("/signin", Name = "Twitter Authentication")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<string> TwitterAuth()
        {
            var appClient = new TwitterClient("q7mkGsaBL9YggmwtpgRPnoqIo", "xwozMoQJAEUnpCSGJlv7y3cDILCPYyhZgSUzzWMODrBxENGXEW");
            var authenticationRequestId = Guid.NewGuid().ToString();
            var redirectPath = Request.Scheme + "://" + Request.Host.Value + "/signin";
            Console.WriteLine(redirectPath);
            // Add the user identifier as a query parameters that will be received by `ValidateTwitterAuth`
            var redirectURL = _myAuthRequestStore.AppendAuthenticationRequestIdToCallbackUrl(redirectPath, authenticationRequestId);
            // Initialize the authentication process
            var authenticationRequestToken = await appClient.Auth.RequestAuthenticationUrlAsync(redirectURL);
            // Store the token information in the store
            await _myAuthRequestStore.AddAuthenticationTokenAsync(authenticationRequestId, authenticationRequestToken);
            Console.WriteLine(authenticationRequestToken);
            Console.WriteLine(authenticationRequestId);
            // Link to redirect the user to Twitter
            // return authenticationRequestToken.AuthorizationURL;
            return authenticationRequestToken.AuthorizationURL;
        }

        [HttpGet("/signin", Name = "Twitter Authentication Validator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<long> ValidateTwitterAuth()
        {
            var appClient = new TwitterClient("q7mkGsaBL9YggmwtpgRPnoqIo", "xwozMoQJAEUnpCSGJlv7y3cDILCPYyhZgSUzzWMODrBxENGXEW");
    
            // Extract the information from the redirection url
            var requestParameters = await RequestCredentialsParameters.FromCallbackUrlAsync(Request.QueryString.Value, _myAuthRequestStore);
            
            // Request Twitter to generate the credentials.
            var userCreds = await appClient.Auth.RequestCredentialsAsync(requestParameters);
            // Congratulations the user is now authenticated!
            var userClient = new TwitterClient(userCreds.ConsumerKey,userCreds.ConsumerSecret,userCreds.AccessToken,userCreds.AccessTokenSecret);
            var user = await userClient.Users.GetAuthenticatedUserAsync();
            
            // string url = "https://api.twitter.com/2/users/" + user.Id + "/tweets";
            // var request = WebRequest.Create(url);
            // request.Method = "GET";
            //
            // using var webResponse = request.GetResponse();
            // using var webStream = webResponse.GetResponseStream();
            //
            // using var reader = new StreamReader(webStream);
            // var data = reader.ReadToEnd();

            return user.Id;
        }
    }
}