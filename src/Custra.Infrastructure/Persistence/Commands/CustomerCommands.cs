using Custra.Application.Common.Interfaces.Persistence.Commands;
using Custra.Domain.Customers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static Custra.Application.Common.Authorization.Permissions;

namespace Custra.Infrastructure.Persistence.Commands;

public sealed class CustomerCommands(CustraDbContext dbContext)
    : ICustomerCommands
{
    private readonly CustraDbContext _dbContext = dbContext;

    public async Task AddCustomerAsync(
       Customer customer,
       CancellationToken cancellationToken = default)
    {
        await _dbContext.Customers.AddAsync(customer, cancellationToken);
    }

    public async Task<Customer?> FindCustomerAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Customers
            .SingleOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public Task DeleteCustomerAsync(
        Customer customer,
        CancellationToken cancellationToken = default)
    {
        _dbContext.Customers.Remove(customer);
        return Task.CompletedTask;
    }

}
