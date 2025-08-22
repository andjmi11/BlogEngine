using BlogAPI.Context;
using BlogAPI.Features.BlogPosts.DTOs;
using BlogAPI.Features.BlogPosts.Mapping;
using BlogAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Features.BlogPosts.Queries.Handlers
{
    public class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, List<PostDTO>>
    {
        private readonly BlogDbContext _context;

        public GetPostsQueryHandler(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<List<PostDTO>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
        {
            var posts = _context.BlogPost
                .Include(p => p.Author)
                .Include(p => p.Tags)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Language))
            {
                posts = posts.Where(p => p.Language == request.Language);
            }

            if (request.Tags != null && request.Tags.Any())
            {
                foreach (var tag in request.Tags)
                {
                    posts = posts.Where(p => p.Tags.Any(t => t.TagName == tag));
                }
            }
            var result = await posts
                .Select(p => p.ToDto())   
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}
