using Extend;
using Microgroove.CustomerApi.BusinessLogic.Models;
using Microgroove.CustomerApi.BusinessLogic.Services;
using Microgroove.CustomerApi.Function.Extensions;
using Microgroove.CustomerApi.Function.Models.Request;
using Microgroove.CustomerApi.Function.Models.Response;
using Microgroove.CustomerApi.Function.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Microgroove.CustomerApi.Function
{
    public class CustomerFunction
    {
        private readonly ICustomerService _customerService;

        public CustomerFunction(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// HTTP POST /api/customers - create a customer.
        /// </summary>
        /// <param name="req">The request.</param>
        /// <param name="log">The logger.</param>
        /// <returns></returns>
        [FunctionName("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "customers")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"CreateCustomer request received.");

            var response = new CustomerResponse();

            try
            {
                var requestData = await new StreamReader(req.Body).ReadToEndAsync();

                var request = JsonConvert.DeserializeObject<CreateCustomerRequest>(requestData);

                log.LogInformation($"CreateCustomer request parsed. " +
                    $"Parameters: FullName = '{request.FullName}', DateOfBirth = '{request.DateOfBirth}'.");

                var requestValidator = new CreateCustomerRequestValidator();

                var validationResult = await requestValidator.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    response.Message = validationResult.ToString(" | ");

                    log.LogInformation($"CreateCustomer response sent. Message: {response.Message}.");

                    return new BadRequestObjectResult(response);
                }

                var customerToCreate = new Customer()
                {
                    FullName = request.FullName,
                    DateOfBirth =
                        DateOnly.ParseExact(request.DateOfBirth, "yyyyMMdd")
                };

                var customerId =
                    await _customerService.CreateCustomerAsync(customerToCreate);

                var customerToResult =
                    await _customerService.GetCustomerByIdAsync(customerId);

                response.Customer = customerToResult.ToResult();

                if (response.Customer == null)
                {
                    response.Message = $"customer not created (id = '{customerId}').";
                }
                else
                {
                    response.Message = $"customer created successfully (id = '{customerId}').";
                }

                log.LogInformation($"CreateCustomer response sent. Message: {response.Message}.");

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                log.LogError("CreateCustomer error.", ex);

                response.Message = "CreateCustomer error.";

                return new ObjectResult(response) { StatusCode = 500 };
            }
        }

        /// <summary>
        /// HTTP GET /api/customers/GUID return a customer by ID.
        /// </summary>
        /// <param name="req">The request.</param>
        /// <param name="log">The logger.</param>
        /// <returns></returns>
        [FunctionName("GetCustomerById")]
        public async Task<IActionResult> GetCustomerById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "customers/{id:Guid}")] HttpRequest req,
            Guid id,
            ILogger log)
        {
            log.LogInformation($"GetCustomerById request received. Parameters: id = '{id}'.");

            var response = new CustomerResponse();

            try
            {
                var customer = await _customerService.GetCustomerByIdAsync(id);

                response.Customer = customer.ToResult();

                if (response.Customer == null)
                {
                    response.Message = $"customer not found (id = '{id}').";
                }
                else
                {
                    response.Message = $"customer found (id = '{id}')";
                }

                log.LogInformation($"GetCustomerById response sent. Message: '{response.Message}'.");

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                log.LogError("GetCustomerById error.", ex);

                response.Message = "GetCustomerById error.";

                return new ObjectResult(response) { StatusCode = 500 };
            }
        }

        /// <summary>
        /// HTTP GET /api/customers/int return customers of a certain age.
        /// </summary>
        /// <param name="req">The request.</param>
        /// <param name="log">The logger.</param>
        /// <returns></returns>
        [FunctionName("GetCustomersByAge")]
        public async Task<IActionResult> GetCustomersByAge(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "customers/{age:int}")] HttpRequest req,
            int age,
            ILogger log)
        {
            log.LogInformation($"GetCustomersByAge request received. Parameters: age = '{age}'.");

            var response = new CustomersResponse();

            try
            {
                var customers = await _customerService.GetCustomersByAgeAsync(age);

                response.Customers = customers.Select(ModelsExtensions.ToResult);

                if (response.Customers.NotAny())
                {
                    response.Message = $"any customers not found (age = '{age}').";
                }
                else
                {
                    response.Message = $"{response.Customers.Count()} customers found (age = '{age}').";
                }

                log.LogInformation($"GetCustomersByAge response sent. Message: '{response.Message}'.");

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                log.LogError("GetCustomersByAge error.", ex);

                response.Message = "GetCustomersByAge error.";

                return new ObjectResult(response) { StatusCode = 500 };
            }
        }
    }
}
