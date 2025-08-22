using BlogAPI.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Features.Authors.Commands.Handlers
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand>
    {
        private readonly BlogDbContext _context;
        public DeleteAuthorCommandHandler(BlogDbContext context)
        {
            _context = context;
        }
        public async Task Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _context.Author.FirstOrDefaultAsync(a=> a.Id == request.Id);


            if (author != null)
            {
                _context.Author.Remove(author);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
