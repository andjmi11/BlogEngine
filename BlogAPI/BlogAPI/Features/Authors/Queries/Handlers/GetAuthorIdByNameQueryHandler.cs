using BlogAPI.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Features.Authors.Queries
{
    public class GetAuthorIdByNameHandler : IRequestHandler<GetAuthorIdByNameQuery, int?>
    {
        private readonly BlogDbContext _context;

        public GetAuthorIdByNameHandler(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<int?> Handle(GetAuthorIdByNameQuery request, CancellationToken cancellationToken)
        {
            var author = await _context.Author
                .AsNoTracking()
                .FirstOrDefaultAsync(a =>
                    a.FirstName == request.FirstName &&
                    a.LastName == request.LastName,
                    cancellationToken);

            return author?.Id;
        }
    }
}
