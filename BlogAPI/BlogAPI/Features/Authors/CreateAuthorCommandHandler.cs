using BlogAPI.Context;
using BlogAPI.Models;
using MediatR;
namespace BlogAPI.Features.Authors
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, int>
    {
        private readonly BlogDbContext _context;

        public CreateAuthorCommandHandler(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateAuthorCommand request, CancellationToken cancellationToken) 
        {
            var author = new Author { FirstName = request.FirstName, LastName = request.LastName };
            _context.Author.Add(author);
            await _context.SaveChangesAsync();

            return author.Id;
        }
    }
}
