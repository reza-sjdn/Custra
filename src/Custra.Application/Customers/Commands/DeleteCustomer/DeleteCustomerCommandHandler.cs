using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Customers.Commands.DeleteCustomer;

public sealed class DeleteCustomerCommandHandler(
    IApplicationDbContext dbContext,
    ICustomerCommands customerCommands)
    : ICommandHandler<DeleteCustomerCommand, Unit>
{
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly ICustomerCommands _customerCommands = customerCommands;


    public async Task<Unit> Handle(
        DeleteCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var customer = await _customerCommands.FindCustomerAsync(
            request.Id,
            cancellationToken);

        if (customer is null)
            throw new KeyNotFoundException(
                "Customer was not found.");

        await _customerCommands.DeleteCustomerAsync(
            customer,
            cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}