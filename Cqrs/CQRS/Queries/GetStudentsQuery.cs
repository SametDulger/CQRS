using Cqrs.CQRS.Results;
using MediatR;
using System.Collections.Generic;

namespace Cqrs.CQRS.Queries
{
    public class GetStudentsQuery : IRequest<IEnumerable<GetStudentsQueryResult>>
    {
    }
}
