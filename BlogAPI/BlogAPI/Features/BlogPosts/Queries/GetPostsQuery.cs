using BlogAPI.Features.BlogPosts.DTOs;
using BlogAPI.Models;
using MediatR;

namespace BlogAPI.Features.BlogPosts.Queries
{
    public class GetPostsQuery : IRequest<List<PostDTO>>
    {
        public string Language { get; set; }
        public string Tags { get; set; }
        public int? AuthorId { get; set; }        
        public DateTime? DateFrom { get; set; }  
        public DateTime? DateTo { get; set; }
    }
}
