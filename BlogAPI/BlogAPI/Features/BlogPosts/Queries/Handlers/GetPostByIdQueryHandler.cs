using BlogAPI.Context;
using BlogAPI.Features.BlogPosts.DTOs;
using BlogAPI.Features.BlogPosts.Mapping;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Features.BlogPosts.Queries.Handlers
{
    public class GetPostByIdQueryHandler(BlogDbContext _context) : IRequestHandler<GetPostByIdQuery, PostDTO>
    {
        public async Task<PostDTO> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var post = await _context.BlogPost
                .AsNoTracking()
                .Include(p => p.Author)
                .Include(p => p.Tags)
                .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);


            if (post == null)
            {
                return null;
            }

            return post.ToDto();
        }
    }
}
