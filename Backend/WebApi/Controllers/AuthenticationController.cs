using System;
using System.Threading.Tasks;
using Application.Features.ExternalTwitterAPI.LogInUser.GetTwitterAuth;
using Application.Features.ExternalTwitterAPI.LogInUser.ValidateAuth;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IMediator _mediator;
        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("/signin", Name = "Twitter Authentication")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<string> TwitterAuth()
        {
            var response = await _mediator.Send(new GetTwitterAuthQuery());
            return response;
        }

        [HttpGet("/signin", Name = "Twitter Authentication Validator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<string> ValidateTwitterAuth(string tweetinvi_auth_request_id, string oauth_token, string oauth_verifier)
        {
            string response = "";
            var req = "?tweetinvi_auth_request_id=" + tweetinvi_auth_request_id + 
                      "&oauth_token=" + oauth_token + 
                      "&oauth_verifier=" + oauth_verifier;
            response = await _mediator.Send(new ValidateAuthQuery(req));
            Console.WriteLine(response);
            return response;
        }
    }
}