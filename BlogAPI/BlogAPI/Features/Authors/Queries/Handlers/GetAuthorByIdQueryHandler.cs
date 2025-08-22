using BlogAPI.Context;
using BlogAPI.Features.Authors.DTOs;
using BlogAPI.Features.Authors.Mapping;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Features.Authors.Queries.Handlers
{
    public class GetAuthorByIdQueryHandler: IRequestHandler<GetAuthorByIdQuery, AuthorDTO>
    {
        private readonly BlogDbContext _context;

        public GetAuthorByIdQueryHandler(BlogDbContext context)
        {
            _context = context;
        }

       public async Task<AuthorDTO> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var author =await  _context.Author
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == request.Id);

            if (author == null)
            {
                return null;
            }

            return author.ToDto();
        }
    }
}
