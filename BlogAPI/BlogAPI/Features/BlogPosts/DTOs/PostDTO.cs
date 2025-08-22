namespace BlogAPI.Features.BlogPosts.DTOs
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public string Language { get; set; }
        public DateTime DatePublished { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
    }
}
