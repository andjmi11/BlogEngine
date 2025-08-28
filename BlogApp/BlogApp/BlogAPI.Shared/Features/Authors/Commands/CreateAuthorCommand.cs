using BlogAPI.Features.Authors.DTOs;
using MediatR;

namespace BlogAPI.Features.Authors.Commands
{
    public class CreateAuthorCommand : IRequest<AuthorDTO>
    {
        public string FirstName { get; set; } =  string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
