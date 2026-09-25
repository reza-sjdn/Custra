using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence;
using Custra.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Customers.Commands.CreateCustomer;

public sealed class CreateCustomerCommandHandler(
    IApplicationDbContext dbContext)
    : ICommandHandler<CreateCustomerCommand, Guid>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Guid> Handle(
        CreateCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var customer = new Customer(request.Name);

        await _dbContext.AddCustomerAsync(
                customer,
                cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return customer.Id;
    }
}
