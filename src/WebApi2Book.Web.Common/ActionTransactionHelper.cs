// ActionTransactionHelper.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Web.Http.Filters;
using NHibernate;
using NHibernate.Context;

namespace WebApi2Book.Web.Common
{
    public class ActionTransactionHelper : IActionTransactionHelper
    {
        private readonly ISessionFactory _sessionFactory;

        public ActionTransactionHelper(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public bool TransactionHandled { get; private set; }

        public bool SessionClosed { get; private set; }

        public void BeginTransaction()
        {
            if (!CurrentSessionContext.HasBind(_sessionFactory)) return;

            var session = _sessionFactory.GetCurrentSession();
            if (session != null)
            {
                session.BeginTransaction();
            }
        }

        public void EndTransaction(HttpActionExecutedContext filterContext)
        {
            if (!CurrentSessionContext.HasBind(_sessionFactory)) return;

            var session = _sessionFactory.GetCurrentSession();

            if (session == null) return;
            if (!session.Transaction.IsActive) return;

            if (filterContext.Exception == null)
            {
                session.Flush();
                session.Transaction.Commit();
            }
            else
            {
                session.Transaction.Rollback();
            }

            TransactionHandled = true;
        }

        public void CloseSession()
        {
            if (!CurrentSessionContext.HasBind(_sessionFactory)) return;

            var session = _sessionFactory.GetCurrentSession();
            session.Close();
            session.Dispose();
            CurrentSessionContext.Unbind(_sessionFactory);
            SessionClosed = true;
        }
    }
}