using BlogAPI.Context;
using BlogAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Features.Authors
{
    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, List<Author>>
    {
        private readonly BlogDbContext _context;

        public GetAllAuthorsQueryHandler(BlogDbContext context)
        {
            _context = context;
        }
        public async Task<List<Author>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Author.ToListAsync(cancellationToken);
        }
    }
}
