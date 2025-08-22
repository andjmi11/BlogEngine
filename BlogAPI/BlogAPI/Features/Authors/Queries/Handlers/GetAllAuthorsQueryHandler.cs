using BlogAPI.Context;
using BlogAPI.Models;
using BlogAPI.Features.Authors.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using BlogAPI.Features.Authors.Mapping;

namespace BlogAPI.Features.Authors.Queries.Handlers
{
    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, IEnumerable<AuthorDTO>>
    {
        private readonly BlogDbContext _context;

        public GetAllAuthorsQueryHandler(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AuthorDTO>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            var authors = await _context.Author
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return authors.Select(a => a.ToDto()).ToList();
        }
    }
}
