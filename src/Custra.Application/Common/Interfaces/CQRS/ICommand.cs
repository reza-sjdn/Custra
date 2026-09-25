using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Common.Interfaces.CQRS;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
