using Entity = Microgroove.CustomerApi.DataAccess.DbEntities;
using Model = Microgroove.CustomerApi.BusinessLogic.Models;

namespace Microgroove.CustomerApi.BusinessLogic.Extensions
{
    public static class ModelsExtensions
    {
        public static Entity.Customer ToEntity(this Model.Customer customer)
        {
            if (customer == null) 
            {
                return null;
            }

            return new Entity.Customer
            {
                CustomerId = customer.CustomerId,
                FullName = customer.FullName,
                DateOfBirth = customer.DateOfBirth,
                ProfileImage = customer.ProfileImage
            };
        }

        public static Model.Customer ToModel(this Entity.Customer customer)
        {
            if (customer == null)
            {
                return null;
            }

            return new Model.Customer
            {
                CustomerId = customer.CustomerId,
                FullName = customer.FullName,
                DateOfBirth = customer.DateOfBirth,
                ProfileImage = customer.ProfileImage
            };
        }
    }
}
