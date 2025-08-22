using BlogAPI.Features.Authors.DTOs;
using MediatR;

namespace BlogAPI.Features.Authors.Queries
{
    public class GetAuthorByIdQuery: IRequest<AuthorDTO>
    {
        public GetAuthorByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
