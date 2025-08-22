using BlogAPI.Context;
using BlogAPI.Features.BlogPosts.DTOs;
using BlogAPI.Features.BlogPosts.Mapping;
using BlogAPI.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Features.BlogPosts.Commands.Handlers
{
    public class UpdateBlogPostCommandHandler : IRequestHandler<UpdateBlogPostCommand, PostDTO>
    {
        private readonly BlogDbContext _context;
        private readonly Helper _helper;
        public UpdateBlogPostCommandHandler(BlogDbContext context, Helper helper)
        {
            _context = context;
            _helper = helper;
        }

        public async Task<PostDTO> Handle(UpdateBlogPostCommand request, CancellationToken cancellationToken)
        {
            var blogPost = await _context.BlogPost
                .Include(t => t.Tags)
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

            if (blogPost == null) throw new Exception("Blog post not found.");

            blogPost.Title = request.Title;
            blogPost.ShortDescription = request.ShortDescription;
            blogPost.Content = request.Content;
            blogPost.Language = request.Language;
            blogPost.DatePublished = request.DatePublished;

            if(blogPost.Author.Id != request.AuthorId)
            {
                var author = await _context.Author.FindAsync(request.AuthorId);
                if (author == null) throw new Exception("Author not found");

                blogPost.Author = author;
            }

            var newTags = _helper.ToNormalizedBlogTags(request.Tags);

            var tagsToRemove = blogPost.Tags.Where(t => !newTags.Any(nt => nt.TagName == t.TagName)).ToList();
            foreach (var tag in tagsToRemove) 
            {
                blogPost.Tags.Remove(tag); 
            }

            var tagsToAdd = newTags.Where(nt => !blogPost.Tags.Any(t => t.TagName == nt.TagName)).ToList();
            foreach (var tag in tagsToAdd)
            {
                blogPost.Tags.Add(tag);
            }


            _context.BlogPost.Update(blogPost);
            await _context.SaveChangesAsync(cancellationToken);

            return blogPost.ToDto();
        }
    }
}
