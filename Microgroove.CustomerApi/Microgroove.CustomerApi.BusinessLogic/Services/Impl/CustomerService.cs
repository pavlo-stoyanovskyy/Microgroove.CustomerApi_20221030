using Microgroove.CustomerApi.AvatarsAccess.Connectors;
using Microgroove.CustomerApi.BusinessLogic.Extensions;
using Microgroove.CustomerApi.BusinessLogic.Models;
using Microgroove.CustomerApi.DataAccess.Repositories;

namespace Microgroove.CustomerApi.BusinessLogic.Services.Impl
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAvatarConnector _avatarConnector;

        public CustomerService(ICustomerRepository customerRepository, IAvatarConnector avatarConnector)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _avatarConnector = avatarConnector ?? throw new ArgumentNullException(nameof(avatarConnector));
        }

        public async Task<Guid> CreateCustomerAsync(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            customer.ProfileImage = await _avatarConnector.GetProfileImageAsync(customer.FullName);

            var entity = customer.ToEntity();

            var result =
                await _customerRepository.CreateCustomerAsync(entity);

            return result;
        }

        public async Task<Customer> GetCustomerByIdAsync(Guid id)
        {
            var entity = await _customerRepository.GetCustomerByIdAsync(id);

            var result = entity.ToModel();

            return result;
        }

        public async Task<IEnumerable<Customer>> GetCustomersByAgeAsync(int age)
        {
            var entities = await _customerRepository.GetCustomersByAgeAsync(age);

            var result =
                entities.Select(ModelsExtensions.ToModel);

            return result;
        }
    }
}
