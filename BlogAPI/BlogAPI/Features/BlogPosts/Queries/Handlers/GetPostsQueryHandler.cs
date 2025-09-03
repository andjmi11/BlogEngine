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

            if (!string.IsNullOrWhiteSpace(request.Tags))
            {
                var tags = request.Tags
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(t => t.Trim().ToLower())
                    .ToList();

                foreach (var tag in tags)
                {
                    posts = posts.Where(p => p.Tags.Any(t => t.TagName == tag));
                }
            }
            if (request.AuthorId.HasValue)
                posts = posts.Where(p => p.Author.Id == request.AuthorId.Value);

            if (request.DateFrom.HasValue)
                posts = posts.Where(p => p.DatePublished >= request.DateFrom.Value);

            if (request.DateTo.HasValue)
                posts = posts.Where(p => p.DatePublished <= request.DateTo.Value);


            var result = await posts
                .Select(p => p.ToDto())   
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}
