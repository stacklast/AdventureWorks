using AdventureWorks.Shared;
using MediatR;

namespace AdventureWorks.Application.Abstractions.Messaging;
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{

}
