using BlogAPI.Features.BlogPosts.DTOs;
using MediatR;

namespace BlogAPI.Features.BlogPosts.Commands
{
    public class CreateBlogPostCommand :IRequest<PostDTO>
    {
        public string Title  { get ; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public string Language { get; set; }
        public DateTime DatePublished { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public int AuthorId { get; set; }

    }
}
