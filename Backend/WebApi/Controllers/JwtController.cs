using System;
using Application.Features.Jwt;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtController : ControllerBase
    {
        private readonly IMediator _mediator;
        public JwtController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost("/token", Name = "Jwt Generator")]
        [ProducesResponseType(StatusCodes.Status200OK)]  
        public Object GetToken([FromBody]TweetSerializer tweets)
        {
            return _mediator.Send(new GetJwtQuery(tweets)).Result;
        }
    }
}