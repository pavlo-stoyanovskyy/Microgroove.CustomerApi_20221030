using Model = Microgroove.CustomerApi.BusinessLogic.Models;
using Result = Microgroove.CustomerApi.Function.Models.Response;

namespace Microgroove.CustomerApi.Function.Extensions
{
    public static class ModelsExtensions
    {
        public static Result.CustomerResult ToResult(this Model.Customer customer)
        {
            if (customer == null)
            {
                return null;
            }

            return new Result.CustomerResult
            {
                CustomerId = customer.CustomerId.ToString("D"),
                FullName = customer.FullName,
                DateOfBirth = customer.DateOfBirth.ToString("yyyyMMdd"),
                ProfileImage = customer.ProfileImage
            };
        }
    }
}
