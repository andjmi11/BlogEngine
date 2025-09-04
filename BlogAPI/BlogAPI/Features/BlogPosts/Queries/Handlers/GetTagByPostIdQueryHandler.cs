using BlogAPI.Context;
using BlogAPI.Features.BlogPosts.DTOs;
using BlogAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Features.BlogPosts.Queries.Handlers
{
    public class GetTagByPostIdQueryHandler(BlogDbContext _context) : IRequestHandler<GetTagByPostIdQuery, List<TagDTO>>
    {
        public async Task<List<TagDTO>> Handle(GetTagByPostIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Set<BlogTags>()
                .Where(bt => bt.BlogPostId == request.PostId)
                .Select(bt => new TagDTO
                {
                    Name = bt.TagName   
                })
                .ToListAsync(cancellationToken);
        }
    }
}
