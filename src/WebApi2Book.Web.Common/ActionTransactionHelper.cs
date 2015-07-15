// ActionTransactionHelper.cs
// Copyright Jamie Kurtz, Brian Wortman 2015.

using System.Web.Http.Filters;
using EFCommonContext;

namespace WebApi2Book.Web.Common
{
    public class ActionTransactionHelper : IActionTransactionHelper
    {
        private readonly IWebContextFactory _contextFactory;

        public ActionTransactionHelper(IWebContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public bool TransactionHandled { get; private set; }

        public bool SessionClosed { get; private set; }

        public void BeginTransaction()
        {
            if (!_contextFactory.ContextExists) return;

            var context = _contextFactory.GetCurrentContext();
            if (context != null)
            {
                context.Database.BeginTransaction();
            }
        }

        public void EndTransaction(HttpActionExecutedContext filterContext)
        {
            if (!_contextFactory.ContextExists) return;

            var context = _contextFactory.GetCurrentContext();
            if (context == null) return;

            if (context.Database.CurrentTransaction == null) return;

            if (filterContext.Exception == null)
            {
                context.SaveChanges();
                context.Database.CurrentTransaction.Commit();
            }
            else
            {
                context.Database.CurrentTransaction.Rollback();
            }

            TransactionHandled = true;
        }

        public void CloseSession()
        {
            if (!_contextFactory.ContextExists) return;

            var context = _contextFactory.GetCurrentContext();
            if (context == null) return;
            context.Database.Connection.Close();
            context.Dispose();
            _contextFactory.Reset();
            SessionClosed = true;
        }
    }
}