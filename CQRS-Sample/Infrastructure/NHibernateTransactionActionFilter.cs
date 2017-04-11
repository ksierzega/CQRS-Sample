namespace CQRS_Sample.Infrastructure
{
    /*public class NHibernateTransactionActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var sessionController = actionContext.ControllerContext.Controller as BaseController;
            if (sessionController == null)
            {
                return;
            }

            if (sessionController.Session == null)
            {
                return;
            }

            sessionController.Session.BeginTransaction();
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionContext)
        {
            var sessionController = actionContext.ActionContext.ControllerContext.Controller as BaseController;
            if (sessionController == null)
            {
                return;
            }

            using (var session = sessionController.Session)
            {
                if (session == null)
                {
                    return;
                }

                if (!session.Transaction.IsActive)
                {
                    return;
                }

                if (actionContext.Exception != null)
                {
                    session.Transaction.Rollback();
                }
                else
                {
                    session.Transaction.Commit();
                }
            }
        }
    }*/
}