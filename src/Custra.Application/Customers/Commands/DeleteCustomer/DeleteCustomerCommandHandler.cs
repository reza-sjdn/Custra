using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Customers.Commands.DeleteCustomer;

public sealed class DeleteCustomerCommandHandler(
    IApplicationDbContext dbContext)
    : ICommandHandler<DeleteCustomerCommand, Unit>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Unit> Handle(
        DeleteCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var customer = await _dbContext.FindCustomerAsync(
            request.Id,
            cancellationToken);

        if (customer is null)
            throw new KeyNotFoundException(
                "Customer was not found.");

        await _dbContext.DeleteCustomerAsync(
            customer,
            cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}