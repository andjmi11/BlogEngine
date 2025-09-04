using BlogAPI.Context;
using BlogAPI.Features.BlogPosts.DTOs;
using BlogAPI.Features.BlogPosts.Mapping;
using BlogAPI.Helpers;
using BlogAPI.Models;
using MediatR;

namespace BlogAPI.Features.BlogPosts.Commands.Handlers
{
    public class CreateBlogPostCommandHandler(BlogDbContext _context, Helper _helper) : IRequestHandler<CreateBlogPostCommand, PostDTO>
    {
        public async Task<PostDTO> Handle(CreateBlogPostCommand request, CancellationToken cancellationToken)
        {
            var author = await _context.Author.FindAsync(request.AuthorId);
            _ = author ?? throw new Exception("Author is null.");


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
            await _context.SaveChangesAsync(cancellationToken);

            return blogPost.ToDto();

        }

    }
}
