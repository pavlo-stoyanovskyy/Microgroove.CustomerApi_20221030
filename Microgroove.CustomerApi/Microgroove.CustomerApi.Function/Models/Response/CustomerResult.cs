using System;

namespace Microgroove.CustomerApi.Function.Models.Response
{
    public class CustomerResult
    {
        public string CustomerId { get; set; }

        public string FullName { get; set; }

        public string DateOfBirth { get; set; }

        public string ProfileImage { get; set; }
    }
}
