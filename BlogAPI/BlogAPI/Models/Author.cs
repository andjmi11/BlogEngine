using System.ComponentModel.DataAnnotations;
namespace BlogAPI.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Fist name is required.")]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(100)]
        public string LastName { get; set; }

    }
}
