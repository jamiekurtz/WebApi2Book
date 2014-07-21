// AllUsersQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using NHibernate;
using WebApi2Book.Data.Entities;
using WebApi2Book.Data.QueryProcessors;

namespace WebApi2Book.Data.SqlServer.QueryProcessors
{
    public class AllUsersQueryProcessor : IAllUsersQueryProcessor
    {
        private readonly ISession _session;

        public AllUsersQueryProcessor(ISession session)
        {
            _session = session;
        }

        public QueryResult<User> GetUsers(PagedDataRequest requestInfo)
        {
            var query = _session.QueryOver<User>();

            var totalItemCount = query.ToRowCountQuery().RowCount();

            var startIndex = ResultsPagingUtility.CalculateStartIndex(requestInfo.PageNumber, requestInfo.PageSize);

            var users = query.Skip(startIndex).Take(requestInfo.PageSize).List();

            var queryResult = new QueryResult<User>(users, totalItemCount, requestInfo.PageSize);

            return queryResult;
        }
    }
}