using BlogAPI.Features.BlogPosts.Commands;
using BlogAPI.Features.BlogPosts.DTOs;
using BlogAPI.Features.BlogPosts.Queries;
using BlogAPI.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : Controller
    {
        private ISender _sender;
        public BlogPostController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateBlogPost(CreateBlogPostCommand command)
        {
            var blogPostId = await _sender.Send(command);
            return Ok(blogPostId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogPost(int id)
        {
            var postBlogForDelete = await _sender.Send(new DeleteBlogPostCommand(id));
            return Ok(postBlogForDelete);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlogPost(int id, [FromBody] UpdateBlogPostCommand query)
        {
            query.Id = id;
            var result = await _sender.Send(query);

            if (result == null)
                return NotFound("Blog post not found");

            return Ok($"Blog post {result.Title} is updated successfully.");
        }

        [HttpGet("get-all")]
        public async Task<IEnumerable<PostDTO>> GetPosts([FromQuery] List<string>? tags, [FromQuery] string? language)
        {
            var posts = await _sender.Send(new GetPostsQuery
            {
                Tags = tags,
                Language = language
            });

            return posts;
        }
    }
}
