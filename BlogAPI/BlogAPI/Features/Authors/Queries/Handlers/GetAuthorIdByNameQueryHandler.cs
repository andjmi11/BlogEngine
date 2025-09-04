using BlogAPI.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Features.Authors.Queries.Handlers;

public class GetAuthorIdByNameHandler(BlogDbContext _context) : IRequestHandler<GetAuthorIdByNameQuery, int?>
{
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
