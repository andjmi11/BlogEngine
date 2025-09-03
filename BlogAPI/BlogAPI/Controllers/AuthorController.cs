using BlogAPI.Features.Authors.Commands;
using BlogAPI.Features.Authors.DTOs;
using BlogAPI.Features.Authors.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private ISender _sender;
        public AuthorController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateAuthor([FromBody] CreateAuthorCommand command)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var author = await _sender.Send(command);
            return CreatedAtAction(
                nameof(GetAuthorById),
                new { Id = author.Id },
                author);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAllAuthors()
        {
            var authors = await _sender.Send(new GetAllAuthorsQuery());
            return Ok(authors);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            await _sender.Send(new DeleteAuthorCommand(id));
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDTO>> GetAuthorById(int id)
        {
            var author = await _sender.Send(new GetAuthorByIdQuery(id));

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor([FromBody] UpdateAuthorCommand command)
        {
            var result = await _sender.Send(command);

            if (result == null)
                return NotFound("Author not found");


            return Ok($"Author {result.FirstName} {result.LastName} is updated successfully.");
        }

        [HttpGet("by-name")]
        public async Task<ActionResult<int>> GetAuthorIdByName([FromQuery] string firstName, [FromQuery] string lastName)
        {
            var authorId = await _sender.Send(new GetAuthorIdByNameQuery(firstName, lastName));

            if (authorId == null)
                return NotFound("Author not found");

            return Ok(authorId);
        }
    }
}
