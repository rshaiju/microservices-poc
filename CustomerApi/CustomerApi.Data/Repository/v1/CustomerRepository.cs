﻿using CustomerApi.Data.Database;
using CustomerApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerApi.Data.Repository.v1
{
    public class CustomerRepository: Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(CustomerContext customerContext):base(customerContext)
        {

        }

        public async Task<Customer> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await customerContext.Customers.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }
    }
}
