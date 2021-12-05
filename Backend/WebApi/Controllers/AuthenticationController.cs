using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tweetinvi;
using Tweetinvi.Auth;
using Tweetinvi.Parameters;

namespace WebApi.Controllers
{
    // TODO MAKE IT WORK 
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private static readonly IAuthenticationRequestStore _myAuthRequestStore = new LocalAuthenticationRequestStore();
        
        [HttpPost("/signin", Name = "Twitter Authentication")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> TwitterAuth()
        {
            var appClient = new TwitterClient("yhJAct7wkrUjZqH29G2JnaNXp", "VIGJH866VEcowNcK1VxzsdJXJ01JJFLohGrNXS8mSHwNvLR2g7");
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
            return new RedirectResult(authenticationRequestToken.AuthorizationURL);
        }

        [HttpGet("/signin", Name = "Twitter Authentication Validator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> ValidateTwitterAuth()
        {
            Console.WriteLine("PRINT!");
            var appClient = new TwitterClient("yhJAct7wkrUjZqH29G2JnaNXp", "VIGJH866VEcowNcK1VxzsdJXJ01JJFLohGrNXS8mSHwNvLR2g7");
    
            // Extract the information from the redirection url
            var requestParameters = await RequestCredentialsParameters.FromCallbackUrlAsync(Request.QueryString.Value, _myAuthRequestStore);
            
            // Request Twitter to generate the credentials.
            var userCreds = await appClient.Auth.RequestCredentialsAsync(requestParameters);
            
            // Congratulations the user is now authenticated!
            var userClient = new TwitterClient(userCreds.ConsumerKey,userCreds.ConsumerSecret,userCreds.AccessToken,userCreds.AccessTokenSecret);
            var user = await userClient.Users.GetAuthenticatedUserAsync();
            
            ViewBag.User = user;
            
            return View();
        }
    }
}