using BlogAPI.Features.Authors.DTOs;
using MediatR;

namespace BlogAPI.Features.Authors.Commands
{
    public class UpdateAuthorCommand:IRequest<AuthorDTO>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
