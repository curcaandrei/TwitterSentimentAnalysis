
using System.Threading.Tasks;
using Application.Features.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Web.Http;
using Raven.Client.Document;
using Raven.Database.FileSystem.Extensions;
using WebApplication.Controllers;

namespace WebApplication.v1
{
    public class TweetController : BaseController
    {
        public TweetController(IMediator mediator) : base(mediator)
        {
        }

        // [HttpPost]
        // public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        // {
        //     return Ok(await mediator.Send(command));
        // }

        // [HttpGet]
        // public async Task<IActionResult> Get()
        // {
        //     return Ok(await mediator.Send(new GetProductsQuery()));
        // }

        // [HttpPut]
        // public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductCommand command)
        // {
        //     if (id != command.Id)
        //     {
        //         return BadRequest();
        //     }
        //
        //     return Ok(await mediator.Send(command));
        // }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await mediator.Send(new GetTweetByIdQuery()));
        }
    }
}