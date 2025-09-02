using BlogAPI.Features.BlogPosts.DTOs;
using MediatR;

namespace BlogAPI.Features.BlogPosts.Queries
{
    public class GetTagByPostIdQuery : IRequest<List<TagDTO>>
    {
        public int PostId { get; set; }
        public GetTagByPostIdQuery(int postId)
        {
            PostId = postId;
        }
    }
}
