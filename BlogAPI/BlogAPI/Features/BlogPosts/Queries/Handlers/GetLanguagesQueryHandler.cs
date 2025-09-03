using BlogAPI.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Features.BlogPosts.Queries.Handlers
{
    public class GetLanguagesQueryHandler : IRequestHandler<GetLanguagesQuery, IEnumerable<string>>
    {
        private readonly BlogDbContext _context;

        public GetLanguagesQueryHandler(BlogDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<string>> Handle(GetLanguagesQuery request, CancellationToken cancellationToken)
        {
            var languages = await _context.BlogPost
               .Select(p => p.Language)
               .Where(l => l != null)
               .Distinct()
               .ToListAsync();

            return languages;
        }
    }
}
