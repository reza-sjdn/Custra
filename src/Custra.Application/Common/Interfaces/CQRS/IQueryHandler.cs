using MediatR;

namespace Custra.Application.Common.Interfaces.CQRS;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
}