using BlogAPI.Context;
using MediatR;

namespace BlogAPI.Features.BlogPosts.Commands.Handlers
{
    public class DeleteBlogPostCommandHandler(BlogDbContext _context) : IRequestHandler<DeleteBlogPostCommand, int>
    {
        public async Task<int> Handle(DeleteBlogPostCommand request, CancellationToken cancellationToken)
        {
            var post = await _context.BlogPost.FindAsync(request.Id);

            if (post == null)
                return 0;

            _context.BlogPost.Remove(post);
            await _context.SaveChangesAsync(cancellationToken);

            return request.Id;
        }
    }
}
