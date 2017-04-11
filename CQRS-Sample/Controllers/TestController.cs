using System.Web.Http;
using CQRS_Sample.Infrastructure;

namespace CQRS_Sample.Controllers
{
    public class TestController : BaseController
    {
        [Route("test")]
        public IHttpActionResult Get()
        {
            return Ok();
        }
    }
}