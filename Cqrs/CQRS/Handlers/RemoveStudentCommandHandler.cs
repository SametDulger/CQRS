using Cqrs.CQRS.Commands;
using Cqrs.Data;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Cqrs.CQRS.Handlers
{
    public class RemoveStudentCommandHandler : IRequestHandler<RemoveStudentCommand>
    {
        private readonly StudentContext _Context;

        public RemoveStudentCommandHandler(StudentContext context)
        {
            _Context = context;
        }


        public async Task<Unit> Handle(RemoveStudentCommand request, CancellationToken cancellationToken)
        {
            var deletedEntity = _Context.Students.Find(request.Id);

            _Context.Students.Remove(deletedEntity);

            await _Context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
