using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IMediator mediator;

        public BaseController(IMediator mediator)
        {
            this.mediator = mediator;
        }
    }
}