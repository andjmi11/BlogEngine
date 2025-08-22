using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlogAPI.Models
{
    public class BlogTags
    {
        [Key]
        public int Id { get; set; }
        public int BlogPostId { get; set; }
        [JsonIgnore]
        public BlogPost BlogPost { get; set; }
        public string TagName { get; set; }

    }
}
