using System.Threading.Tasks;
using Application.Features.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.v1
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
        [Route("/getOne")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await mediator.Send(new GetAllTweetsQuery()));
        }

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
        [Route("/getAll")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await mediator.Send(new GetTweetByIdQuery()));
        }

    }
}