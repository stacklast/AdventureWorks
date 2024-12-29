using AdventureWorks.Shared;
using MediatR;

namespace AdventureWorks.Application.Abstractions.Messaging;
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
