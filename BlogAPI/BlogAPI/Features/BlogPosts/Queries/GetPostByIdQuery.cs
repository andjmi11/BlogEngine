using BlogAPI.Features.BlogPosts.DTOs;
using MediatR;

namespace BlogAPI.Features.BlogPosts.Queries
{
    public class GetPostByIdQuery: IRequest<PostDTO>
    {
        public GetPostByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
