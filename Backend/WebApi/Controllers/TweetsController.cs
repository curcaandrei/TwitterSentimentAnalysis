using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Commands.CreateTweet;
using Application.Commands.DeleteTweet;
using Application.Commands.UpdateTweet;
using Application.Features.Tweets.GetAllTweets;
using Application.Features.Tweets.GetOneTweet;
using Domain.Dtos;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

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

        [HttpGet("all/{pageNr}", Name = "AllTweets")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Tweet>>> GetAll(int pageNr)
        {
            var dtos = await _mediator.Send(new GetTweetsQuery(pageNr));
            return Ok(dtos);
        }
        
        [HttpPost("Create", Name = "CreateTweet")]
        public async Task<ActionResult<Tweet>> Create([FromBody] CreateTweetCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response.ToJson());
        }

        [HttpGet("one/{id}", Name = "GetOne")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<TweetDTO> GetOne([FromRoute]string id)
        {
            var res = _mediator.Send(new GetOneTweetQuery(id));
            TweetDTO dto = new TweetDTO();
            dto.Id = res.Result.Id.ToString();
            dto.feels = res.Result.feels;
            dto.Text = res.Result.Text;
            dto.Username = res.Result.Username;
            dto.Date = res.Result.Date;
            dto.User = res.Result.User;
            return Task.FromResult(dto);
        }

        [HttpDelete("delete/{id}", Name = "DeleteOne")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public DeleteResult DeleteOne([FromRoute] string id)
        {
            var res = _mediator.Send(new DeleteTweetCommand(id));
            return res.Result;
        }

        [HttpPut("update/{id}", Name = "UpdateOne")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public UpdateResult UpdateOne([FromRoute] string id, [FromBody]Dictionary<string, float> feels)
        {
            var res = _mediator.Send(new UpdateTweetCommand(id, feels));
            return res.Result;
        }
    }
}