using BlogAPI.Context;
using MediatR;

namespace BlogAPI.Features.BlogPosts
{
    public class DeleteBlogPostQueryHandler : IRequestHandler<DeleteBlogPostQuery, int>
    {
        private readonly BlogDbContext _context;

        public DeleteBlogPostQueryHandler(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(DeleteBlogPostQuery request, CancellationToken cancellationToken)
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
