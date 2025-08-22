using BlogAPI.Features.Authors.DTOs;
using MediatR;

namespace BlogAPI.Features.Authors.Queries
{
    public class GetAllAuthorsQuery : IRequest<IEnumerable<AuthorDTO>>
    {

    }
}
