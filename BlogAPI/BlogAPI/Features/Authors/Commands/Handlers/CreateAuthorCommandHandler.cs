using BlogAPI.Context;
using BlogAPI.Features.Authors.DTOs;
using BlogAPI.Features.Authors.Mapping;
using BlogAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace BlogAPI.Features.Authors.Commands.Handlers
{
    public class CreateAuthorCommandHandler(BlogDbContext _context) : IRequestHandler<CreateAuthorCommand, AuthorDTO>
    {

        public async Task<AuthorDTO> Handle(CreateAuthorCommand request, CancellationToken cancellationToken) 
        {
            var existingAuthor = await _context.Author
                .FirstOrDefaultAsync(
                    a => a.FirstName == request.FirstName && a.LastName == request.LastName,
                    cancellationToken);

            if (existingAuthor != null)
            {
                return null;
            }

            var author = new Author 
            { 
                FirstName = request.FirstName, 
                LastName = request.LastName 
            };

            _context.Author.Add(author);
            await _context.SaveChangesAsync(cancellationToken);

            return author.ToDto();
        }
    }
}
