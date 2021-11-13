using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Commands.CreateTweet;
using Application.Features.Tweets;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TweetsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TweetsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all", Name = "AllTweets")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Tweet>>> GetAll()
        {
            var dtos = await _mediator.Send(new GetTweetsQuery());
            return Ok(dtos);
        }

        [HttpPost("Create", Name = "CreateTweet")]
        public async Task<ActionResult<Tweet>> Create([FromBody] CreateTweetCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}