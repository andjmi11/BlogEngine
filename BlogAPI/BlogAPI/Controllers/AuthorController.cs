using BlogAPI.Features.Authors;
using BlogAPI.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : Controller
    {
        private ISender _sender;
        public AuthorController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateAuthor(CreateAuthorCommand command)
        {
            var authorId = await _sender.Send(command);
            return Ok(authorId);
        }

        [HttpGet]
        public async Task<ActionResult<List<Author>>> GetAllAuthors()
        {
            var authors = await _sender.Send(new GetAllAuthorsQuery());
            return Ok(authors);
        }
    }
}
