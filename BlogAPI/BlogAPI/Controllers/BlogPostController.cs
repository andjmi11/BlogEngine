using BlogAPI.Features.BlogPosts;
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
            var postBlogForDelete = await _sender.Send(new DeleteBlogPostQuery());
            return Ok(postBlogForDelete);
        }
    }
}
