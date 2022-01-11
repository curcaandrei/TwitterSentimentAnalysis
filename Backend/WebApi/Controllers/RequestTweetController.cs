using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Commands.AcceptRequest;
using Application.Commands.RequestTweet;
using Application.Features.RequestTweet;
using Application.Features.Tweets.GetAllTweets;
using Domain;
using Domain.Dtos;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestTweetController : Controller
    {
        private readonly IMediator _mediator;
        public RequestTweetController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<TweetDto>> GetAll()
        {
            var list = await _mediator.Send(new GetAllRequestsQuery());
            return list;
        }
        
        [HttpPost("/request-to-add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task AddToRequests([FromBody] Tweet tweet)
        {
            
            await _mediator.Send(new RequestToAddTweetQuery(tweet));
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("delete/{id}", Name = "DeleteRequest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public DeleteResult DeleteRequest(string id)
        {
            var res = _mediator.Send(new DeleteTweetRequestCommand(id));
            return res.Result;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("accept-request/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public void AcceptRequest(string id)
        {

            _mediator.Send(new AcceptRequestCommand(id));
        }
    }
}