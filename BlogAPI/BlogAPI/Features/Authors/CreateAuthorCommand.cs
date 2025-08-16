using MediatR;

namespace BlogAPI.Features.Authors
{
    public class CreateAuthorCommand :IRequest<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
