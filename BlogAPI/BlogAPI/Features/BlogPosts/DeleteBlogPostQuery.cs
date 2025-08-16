using MediatR;

namespace BlogAPI.Features.BlogPosts
{
    public class DeleteBlogPostQuery : IRequest<int>
    {
        public int Id { get; set; }
    }
}
