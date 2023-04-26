using Cqrs.CQRS.Queries;
using Cqrs.CQRS.Results;
using Cqrs.Data;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Cqrs.CQRS.Handlers
{
    public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, GetStudentByIdQueryResult>
    {
        private readonly StudentContext _context;

        public GetStudentByIdQueryHandler(StudentContext studentContext)
        {
            _context = studentContext;
        }



        public async Task<GetStudentByIdQueryResult> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _context.Set<Student>().FindAsync(request.Id);

            return new GetStudentByIdQueryResult
            {
                Name = student.Name,
                Surname = student.Surname,
                Age = student.Age
            };
        }
    }
}
