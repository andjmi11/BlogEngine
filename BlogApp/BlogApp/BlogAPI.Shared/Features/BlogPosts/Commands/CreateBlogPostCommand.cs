using BlogAPI.Features.BlogPosts.DTOs;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Features.BlogPosts.Commands
{
    public class CreateBlogPostCommand : IRequest<PostDTO>
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(70, ErrorMessage = "Title cannot exceed 70 characters")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Short description is required")]
        [StringLength(200, ErrorMessage = "Short description cannot exceed 200 characters")]
        public string ShortDescription { get; set; } = string.Empty;

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; } = string.Empty;

        [Required(ErrorMessage = "Language is required")]
        public string Language { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date published is required")]
        public DateTime DatePublished { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "At least one tag is required")]
        public string Tags { get; set; } = string.Empty;

        [Required(ErrorMessage = "Author is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Author must be selected")]
        public int AuthorId { get; set; }
    }
}