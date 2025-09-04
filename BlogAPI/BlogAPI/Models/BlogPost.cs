using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace BlogAPI.Models
{
    public class BlogPost
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Short description is required.")]
        [StringLength(250, ErrorMessage = "Short description cannot exceed 250 characters.")]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Language is required.")]
        [StringLength(2, ErrorMessage = "Language code must be 2 characters.")]
        public string Language { get; set; }

        [DataType(DataType.Date)]
        public DateTime DatePublished { get; set; }

        [Required(ErrorMessage = "Author is required.")]
        public Author Author { get; set; }
        public ICollection<BlogTags> Tags { get; set; } = new List<BlogTags>();
    }
}