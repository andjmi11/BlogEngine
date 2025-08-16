using BlogAPI.Models;
using MediatR;

namespace BlogAPI.Features.Authors
{
    public class GetAllAuthorsQuery : IRequest<List<Author>>
    {

    }
}
