using BlogAPI.Context;
using BlogAPI.Features.Authors.DTOs;
using BlogAPI.Features.Authors.Mapping;
using BlogAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Features.Authors.Commands.Handlers
{
    public class UpdateAuthorCommandHandler(BlogDbContext _context) : IRequestHandler<UpdateAuthorCommand, AuthorDTO>
    {
        public async Task<AuthorDTO> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _context.Author.FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

            if (author == null)
            {
                return null;
            }
            author.FirstName = request.FirstName;
            author.LastName = request.LastName;

            _context.Author.Update(author);
            await _context.SaveChangesAsync(cancellationToken);

            return author.ToDto();
        }
    }
}
