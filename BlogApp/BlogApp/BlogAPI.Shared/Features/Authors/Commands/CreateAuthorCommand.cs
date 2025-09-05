using BlogAPI.Shared.Features.Authors.DTOs;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Shared.Features.Authors.Commands
{
    public class CreateAuthorCommand : IRequest<AuthorDTO>
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(100, ErrorMessage = "First name cannot exceed 100 characters")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(100, ErrorMessage = "Last name cannot exceed 100 characters")]
        public string LastName { get; set; } = string.Empty;
    }
}
