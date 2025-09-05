using BlogAPI.Context;
using BlogAPI.Features.BlogPosts.DTOs;
using BlogAPI.Helpers;
using BlogAPI.Models;
using MediatR;
using BlogAPI.Features.BlogPosts.Commands;
using BlogAPI.Features.BlogPosts.Mapping;

namespace BlogAPI.Features.BlogPosts.Commands.Handlers
{
    public class CreateBlogPostCommandHandler : IRequestHandler<CreateBlogPostCommand, PostDTO>
    {
        private readonly BlogDbContext _context;
        private readonly Helper _helper;
        public CreateBlogPostCommandHandler(BlogDbContext context, Helper helper)
        {
            _context = context;
            _helper = helper;
        }
        public async Task<PostDTO> Handle(CreateBlogPostCommand request, CancellationToken cancellationToken)
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
                Tags = _helper.ToNormalizedBlogTags(request.Tags),
                Author = author,
            };

            _context.BlogPost.Add(blogPost);
            await _context.SaveChangesAsync();

            return blogPost.ToDto();

        }

    }
}
