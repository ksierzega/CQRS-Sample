namespace CQRS_Sample.Infrastructure.CQRS
{
    public interface IQuery<out T>
    {
        T Execute();
    }
}
