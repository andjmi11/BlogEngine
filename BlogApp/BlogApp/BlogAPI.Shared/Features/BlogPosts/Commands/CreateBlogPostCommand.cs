using BlogAPI.Shared.Features.BlogPosts.DTOs;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Shared.Features.BlogPosts.Commands
{
    public class CreateBlogPostCommand : IRequest<PostDTO>
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(70, ErrorMessage = "Title cannot exceed 70 characters")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Short description is required")]
        [StringLength(200, ErrorMessage = "Short description cannot exceed 200 characters")]
        public string ShortDescription { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        [Required(ErrorMessage = "Language is required")]
        [RegularExpression(@"^[a-zA-Z]{2}$", ErrorMessage = "Language must be exactly two letters.")]
        public string Language { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date published is required")]
        public DateTime DatePublished { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "At least one tag is required")]
        [RegularExpression(@"^([a-zA-Z0-9]+(-[a-zA-Z0-9]+)*)(,\s*([a-zA-Z0-9]+(-[a-zA-Z0-9]+)*))*$",
                ErrorMessage = "Tags can only contain letters, numbers, optional hyphens, and must be separated by commas.")]
        public string Tags { get; set; } = string.Empty;

        [Required(ErrorMessage = "Author is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Author must be selected")]
        public int AuthorId { get; set; }
    }
}