using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Features.Tweets.PredictTweetSentiment;
using Domain;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Domain.TweetML;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictMlController : Controller
    {
        
        private readonly IMediator _mediator;
        public PredictMlController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost("/predictText")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<Dictionary<string, float>> Analysis(string text)
        {
            
            var response = await _mediator.Send(new PredictTweetSentimentQuery(text));
            return response;

        }
    }
}
