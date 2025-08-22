using MediatR;

namespace BlogAPI.Features.Authors.Commands
{
    public class DeleteAuthorCommand : IRequest
    {
        public DeleteAuthorCommand(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
