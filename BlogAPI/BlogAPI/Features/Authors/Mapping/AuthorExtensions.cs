using BlogAPI.Features.Authors.Commands;
using BlogAPI.Features.Authors.DTOs;
using BlogAPI.Models;

namespace BlogAPI.Features.Authors.Mapping
{
    public static class AuthorExtensions
    {
        public static AuthorDTO ToDto(this Author author) =>
            new()
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
            };

        public static CreateAuthorCommand ToCreateCommand(this AuthorDTO dto) =>
            new()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };
    }
}
