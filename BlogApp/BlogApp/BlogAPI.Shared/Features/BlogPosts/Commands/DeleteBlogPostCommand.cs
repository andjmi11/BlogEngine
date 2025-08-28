using MediatR;

namespace BlogAPI.Features.BlogPosts.Commands
{
    public class DeleteBlogPostCommand : IRequest<int>
    {
        public DeleteBlogPostCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
