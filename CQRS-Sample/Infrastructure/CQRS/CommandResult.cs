using System.Collections.Generic;
using System.Linq;

namespace CQRS_Sample.Infrastructure.CQRS
{
    public class CommandResult
    {
        private readonly IList<KeyValuePair<string, string>> _errors;

        public object Result { get; set; }

        public IList<KeyValuePair<string, string>> Errors
        {
            get { return _errors; }
        }

        public bool IsSuccess
        {
            get { return !_errors.Any(); }
        }

        public CommandResult()
        {
            _errors = new List<KeyValuePair<string, string>>();
        }

        public void AddError(string key, string errorMessage)
        {
            _errors.Add(new KeyValuePair<string, string>(key, errorMessage));
        }

        public static CommandResult Success()
        {
            return new CommandResult();
        }

        public static CommandResult Success(object result)
        {
            return new CommandResult
            {
                Result = result
            };
        }

        public static CommandResult Error(IEnumerable<string> errors)
        {
            var result = new CommandResult();
            foreach (var error in errors)
            {
                result.AddError(string.Empty, error);
            }

            return result;
        }

        public static CommandResult Error(string error)
        {
            var result = new CommandResult();
            result.AddError(string.Empty, error);
            
            return result;
        }
    }
}
