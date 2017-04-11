using System.Web.Http;
using System.Web.Http.Results;
using AutoMapper;
using CQRS_Sample.Infrastructure.CQRS;
using NLog;

namespace CQRS_Sample.Infrastructure
{
    
//    [NHibernateTransactionActionFilter]
    public abstract class BaseController : ApiController
    {
        private ILogger _logger = LogManager.CreateNullLogger();
        public ILogger Logger
        {
            get { return _logger; }
            set { _logger = value; }
        }

//        public ISession Session { get; set; }

        public IMapper Mapper { get; set; }
        public MapperConfiguration MapperConfiguration { get; set; }

        public ICommandSender Bus { get; set; }
        public IQueryExecutor QueryExecutor { get; set; }
        
        protected InvalidModelStateResult BadRequest(CommandResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Key, error.Value);
            }

            return BadRequest(ModelState);
        }
    }
}