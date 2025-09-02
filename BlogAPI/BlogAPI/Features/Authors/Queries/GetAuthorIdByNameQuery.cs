using MediatR;

namespace BlogAPI.Features.Authors.Queries
{
    public class GetAuthorIdByNameQuery : IRequest<int?>
    {
        public string FirstName { get; }
        public string LastName { get; }

        public GetAuthorIdByNameQuery(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
