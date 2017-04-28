namespace CQRS_Sample.Domain.Customers
{
    internal class RegisterCustomerCommand 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
