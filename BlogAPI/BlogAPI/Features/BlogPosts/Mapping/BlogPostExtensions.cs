using BlogAPI.Features.Authors.DTOs;
using BlogAPI.Features.BlogPosts.Commands;
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
                Tags = post.Tags != null
                    ? string.Join(", ", post.Tags.Select(t => t.TagName))
                    : string.Empty,
                AuthorFirstName = post.Author?.FirstName ?? string.Empty,
                AuthorLastName = post.Author?.LastName ?? string.Empty,
            };
        public static UpdateBlogPostCommand ToUpdateCommand(this CreateBlogPostCommand create) =>
            new UpdateBlogPostCommand
            {
                Title = create.Title,
                ShortDescription = create.ShortDescription,
                Content = create.Content,
                Language = create.Language,
                Tags = create.Tags,
                DatePublished = create.DatePublished,
                AuthorId = create.AuthorId
            };
        }
    }
