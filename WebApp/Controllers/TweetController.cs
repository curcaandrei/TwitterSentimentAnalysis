using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TweetController : ControllerBase
    {
        private readonly IRepository<Tweet> _repository;

        public TweetController(IRepository<Tweet> repository)
        {
            _repository = repository;
        }

        [HttpGet("all", Name = "GetAllTweets")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Tweet>>> GetAll()
        {
            var dtos = _repository.SelectAll();
            return Ok(dtos);
        }
    }
}