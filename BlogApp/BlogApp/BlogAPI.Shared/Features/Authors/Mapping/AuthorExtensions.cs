using BlogAPI.Features.Authors.Commands;
using BlogAPI.Features.Authors.DTOs;

namespace BlogAPI.Features.Authors.Mapping
{
    public static class AuthorExtensions
    {
        public static CreateAuthorCommand ToCreateCommand(this AuthorDTO dto) =>
            new CreateAuthorCommand
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };
    }
}
