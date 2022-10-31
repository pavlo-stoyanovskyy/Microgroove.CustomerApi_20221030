using System.Collections.Generic;

namespace Microgroove.CustomerApi.Function.Models.Response
{
    public class CustomersResponse
    {
        public IEnumerable<CustomerResult> Customers { get; set; }

        public string Message { get; set; }
    }
}
