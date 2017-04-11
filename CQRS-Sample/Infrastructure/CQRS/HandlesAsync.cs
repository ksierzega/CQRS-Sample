using System.Threading.Tasks;

namespace CQRS_Sample.Infrastructure.CQRS
{
    public interface HandlesAsync<in T> where T : class
    {
        Task<CommandResult> HandleAsync(T message);
    }
}
