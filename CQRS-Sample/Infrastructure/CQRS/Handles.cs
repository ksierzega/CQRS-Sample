namespace CQRS_Sample.Infrastructure.CQRS
{
    public interface Handles<in T> where T : class
    {
        CommandResult Handle(T message);
    }
}