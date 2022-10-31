using Microgroove.CustomerApi.BusinessLogic.Models;

namespace Microgroove.CustomerApi.BusinessLogic.Services
{
    public interface ICustomerService
    {
        Task<Guid> CreateCustomerAsync(Customer customer);

        Task<Customer> GetCustomerByIdAsync(Guid id);

        Task<IEnumerable<Customer>> GetCustomersByAgeAsync(int age);
    }
}
