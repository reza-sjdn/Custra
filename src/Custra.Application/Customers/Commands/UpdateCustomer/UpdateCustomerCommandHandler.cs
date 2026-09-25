using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Customers.Commands.UpdateCustomer;

public sealed class UpdateCustomerCommandHandler(
    IApplicationDbContext dbContext)
    : ICommandHandler<UpdateCustomerCommand, Unit>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Unit> Handle(
        UpdateCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var customer = await _dbContext.FindCustomerAsync(
            request.Id,
            cancellationToken);

        if (customer is null)
            throw new KeyNotFoundException(
                "Customer was not found.");

        customer.UpdateName(request.Name);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}