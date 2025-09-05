using BlogAPI.Shared.Features.BlogPosts.Commands;

namespace BlogAPI.Shared.Features.BlogPosts.Mapping
{
    public static class BlogPostExtensions
    {
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
