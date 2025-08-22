using BlogAPI.Features.Authors.DTOs;
using BlogAPI.Features.BlogPosts.DTOs;
using BlogAPI.Models;

namespace BlogAPI.Features.BlogPosts.Mapping
{
    public static class BlogPostExtensions
    {
        public static PostDTO ToDto(this BlogPost post) =>
            new PostDTO
            {
                Id = post.Id,
                Title = post.Title,
                ShortDescription = post.ShortDescription,
                Content = post.Content,
                Language = post.Language,
                DatePublished = post.DatePublished,
                Tags = post.Tags.Select(t => t.TagName).ToList(),
                AuthorFirstName = post.Author.FirstName,
                AuthorLastName = post.Author.LastName,
            };
    }
}
