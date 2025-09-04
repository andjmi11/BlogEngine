using MediatR;

namespace BlogAPI.Shared.Features.Authors.Commands
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
