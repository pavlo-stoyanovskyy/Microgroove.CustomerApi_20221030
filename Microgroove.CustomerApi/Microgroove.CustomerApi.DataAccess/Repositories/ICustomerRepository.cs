using Microgroove.CustomerApi.DataAccess.DbEntities;

namespace Microgroove.CustomerApi.DataAccess.Repositories
{
    public interface ICustomerRepository
    {
        Task<Guid> CreateCustomerAsync(Customer customer);

        Task<Customer> GetCustomerByIdAsync(Guid id);

        Task<IEnumerable<Customer>> GetCustomersByAgeAsync(int age);
    }
}
