using System.Threading.Tasks;

namespace CQRS_Sample.Infrastructure.CQRS
{
    public interface ICommandSender
    {
        CommandResult Send<T>(T command) where T : class;

        Task<CommandResult> SendAsync<T>(T command) where T : class;
    }
}