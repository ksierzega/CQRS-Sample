namespace CQRS_Sample.Infrastructure.CQRS
{
    public interface ICurrentUser
    {
        string UserName { get; }
        string UserId { get; }
        int CustomerId { get; }
        bool IsCustomerCreated { get; }
    }
}
