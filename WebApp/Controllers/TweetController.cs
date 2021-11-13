using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Features.Tweets.Commands;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace WebApp.Controllers
{
    public class TweetController : BaseApiController
    {

        [HttpPost]
        public async Task<IActionResult> Create(AddTweetCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}