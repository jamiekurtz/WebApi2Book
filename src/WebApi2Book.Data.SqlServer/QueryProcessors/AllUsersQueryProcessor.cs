// AllUsersQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2015.

using System.Linq;
using WebApi2Book.Data.Entities;
using WebApi2Book.Data.QueryProcessors;

namespace WebApi2Book.Data.SqlServer.QueryProcessors
{
    public class AllUsersQueryProcessor : IAllUsersQueryProcessor
    {
        private readonly TasksDbContext _dbContext;

        public AllUsersQueryProcessor(TasksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public QueryResult<User> GetUsers(PagedDataRequest requestInfo)
        {
            var query = _dbContext.Set<User>();

            var totalItemCount = query.Count();

            var startIndex = ResultsPagingUtility.CalculateStartIndex(requestInfo.PageNumber, requestInfo.PageSize);

            var users = query.Skip(startIndex).Take(requestInfo.PageSize).ToList();

            var queryResult = new QueryResult<User>(users, totalItemCount, requestInfo.PageSize);

            return queryResult;
        }
    }
}