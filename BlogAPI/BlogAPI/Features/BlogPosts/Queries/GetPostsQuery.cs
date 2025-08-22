using BlogAPI.Features.BlogPosts.DTOs;
using BlogAPI.Models;
using MediatR;

namespace BlogAPI.Features.BlogPosts.Queries
{
    public class GetPostsQuery : IRequest<List<PostDTO>>
    {
        public string Language { get; set; }
        public List<string> Tags { get; set; }
    }
}
