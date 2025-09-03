using MediatR;

namespace BlogAPI.Features.BlogPosts.Queries
{
    public class GetLanguagesQuery: IRequest<IEnumerable<string>>
    {
    }
}
