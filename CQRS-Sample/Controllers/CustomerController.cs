using System.Web.Http;
using CQRS_Sample.Domain.Customers;
using CQRS_Sample.Infrastructure;

namespace CQRS_Sample.Controllers
{
    public class CustomerController : BaseController
    {
        [Route("test")]
        public IHttpActionResult Get()
        {
            var cmd = new RegisterCustomerCommand()
            {
                FirstName = "Foo",
                LastName = "Bar",
                Email = "foobar@test.com"
            };

            var result = Bus.Send(cmd);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok();
        }
    }
}