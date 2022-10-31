namespace Microgroove.CustomerApi.BusinessLogic.Models
{
    public class Customer
    {
        public Guid CustomerId { get; set; }

        public string FullName { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public string ProfileImage { get; set; }
    }
}
