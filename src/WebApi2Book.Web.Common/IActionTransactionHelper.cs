// IActionTransactionHelper.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Web.Http.Filters;

namespace WebApi2Book.Web.Common
{
    public interface IActionTransactionHelper
    {
        void BeginTransaction();
        void EndTransaction(HttpActionExecutedContext filterContext);
        void CloseSession();
    }
}