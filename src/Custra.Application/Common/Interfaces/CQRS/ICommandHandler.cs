using MediatR;

namespace Custra.Application.Common.Interfaces.CQRS;

public interface ICommandHandler<TCommand, TResponse>
    : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
}