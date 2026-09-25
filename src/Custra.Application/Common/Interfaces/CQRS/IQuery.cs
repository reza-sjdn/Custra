using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Common.Interfaces.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
