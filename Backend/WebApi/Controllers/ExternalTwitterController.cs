using System.Threading.Tasks;
using Application.Features.ExternalTwitterAPI.GetTweetFromURL;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalTwitterController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ExternalTwitterController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("tweetById/{id}", Name = "Get-External-Tweet")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<Tweet> GetExternalTweetById([FromRoute]string id)
        {
            var dtos = await _mediator.Send(new GetTweetFromUrlQuery(id));

            return dtos;
        }
    }
}