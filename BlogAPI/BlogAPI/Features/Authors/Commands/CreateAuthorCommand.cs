using BlogAPI.Features.Authors.DTOs;
using MediatR;

namespace BlogAPI.Features.Authors.Commands
{
    public class CreateAuthorCommand : IRequest<AuthorDTO>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
