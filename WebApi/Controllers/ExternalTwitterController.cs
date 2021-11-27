using System.Threading.Tasks;
using Application.Features.ExternalTwitterAPI.GetTweetFromURL;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tweet = Domain.Entities.Tweet;

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
        public async Task<ActionResult<Tweet>> GetExternalTweetById([FromRoute]string id)
        {
            var dtos = await _mediator.Send(new GetTweetFromURLQuery(id));

            return dtos;
        }
    }
}