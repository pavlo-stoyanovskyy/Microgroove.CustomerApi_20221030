using Microgroove.CustomerApi.DataAccess.DbContexts;
using Microgroove.CustomerApi.DataAccess.DbEntities;
using Microgroove.CustomerApi.DataAccess.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Microgroove.CustomerApi.DataAccess.Repositories.Impl
{
    public class CustomerRepository : ICustomerRepository
    {
        public async Task<Guid> CreateCustomerAsync(Customer customer)
        {
            var customerId = Guid.NewGuid();

            using var db = new CustomerContext();

            customer.CustomerId = customerId;

            var result = await db.Customers.AddAsync(customer);

            await db.SaveChangesAsync();

            return result.Entity.CustomerId;
        }

        public Task<Customer> GetCustomerByIdAsync(Guid id)
        {
            using var db = new CustomerContext();

            var customer = db.Customers.SingleOrDefaultAsync(_ => _.CustomerId == id);

            return customer;
        }

        public async Task<IEnumerable<Customer>> GetCustomersByAgeAsync(int age)
        {
            using var db = new CustomerContext();

            var birthDateRange = age.GetBirthDateRange();

            var result = await db.Customers
                .Where(_ =>
                    _.DateOfBirth >= birthDateRange.MinDateOfBirth && _.DateOfBirth <= birthDateRange.MaxDateOfBirth)
                .ToListAsync();

            return result;
        }
    }
}
