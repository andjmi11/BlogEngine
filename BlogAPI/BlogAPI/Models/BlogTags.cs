using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Models
{
    public class BlogTags
    {
        [Key]
        public int Id { get; set; }
        public int BlogPostId { get; set; }
        public BlogPost BlogPost { get; set; }
        public string TagName { get; set; }

    }
}
