using BlogAPI.Context;
using BlogAPI.Models;
using MediatR;

namespace BlogAPI.Features.BlogPosts
{
    public class CreateBlogPostCommandHandler : IRequestHandler<CreateBlogPostCommand, int>
    {
        private readonly BlogDbContext _context;

        public CreateBlogPostCommandHandler(BlogDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreateBlogPostCommand request, CancellationToken cancellationToken)
        {
            var author = await _context.Author.FindAsync(request.AuthorId);
            if (author == null) throw new Exception("Author is null.");

            var blogPost = new BlogPost
            {
                Title = request.Title,
                ShortDescription = request.ShortDescription,
                Content = request.Content,
                Language = request.Language,
                DatePublished = request.DatePublished,
                Tags = ToNormalizedBlogTags(request.Tags),
                Author = author,
            };

            _context.BlogPost.Add(blogPost);
            await _context.SaveChangesAsync();

            return blogPost.Id;

        }

        private ICollection<BlogTags> ToNormalizedBlogTags(List<string> tags)
        {
            return tags.Where(t => !string.IsNullOrWhiteSpace(t))
                .Select(t => t.Trim().ToLower())
                .Distinct()
                .Select(tag => new BlogTags { TagName = tag })
                .ToList() ?? new List<BlogTags>();
        }
    }
}
