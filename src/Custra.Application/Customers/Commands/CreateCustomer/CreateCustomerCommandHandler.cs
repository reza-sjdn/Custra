using Custra.Application.Common.Interfaces.CQRS;
using Custra.Application.Common.Interfaces.Persistence;
using Custra.Application.Common.Interfaces.Persistence.Commands;
using Custra.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Application.Customers.Commands.CreateCustomer;

public sealed class CreateCustomerCommandHandler(
    IApplicationDbContext dbContext,
    ICustomerCommands customerCommands)
    : ICommandHandler<CreateCustomerCommand, Guid>
{
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly ICustomerCommands _customerCommands = customerCommands;


    public async Task<Guid> Handle(
        CreateCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var customer = new Customer(request.Name);

        await _customerCommands.AddCustomerAsync(
                customer,
                cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return customer.Id;
    }
}
