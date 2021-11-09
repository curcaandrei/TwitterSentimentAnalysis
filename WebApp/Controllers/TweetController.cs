using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Features.Tweets.Queries;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Persistence.Repositories;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TweetController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TweetController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all", Name = "GetAllTweets")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<TweetListVm>>> GetAll()
        {
            var dtos = await _mediator.Send(new GetTweetListQuery());
            return Ok(dtos);
        }
    }
}