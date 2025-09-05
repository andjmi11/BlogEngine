using BlogAPI.Shared.Features.Authors.Commands;
using BlogAPI.Shared.Features.Authors.DTOs;

namespace BlogAPI.Shared.Features.Authors.Mapping
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
