// AllTasksQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2015.

using System.Linq;
using WebApi2Book.Data.Entities;
using WebApi2Book.Data.QueryProcessors;

namespace WebApi2Book.Data.SqlServer.QueryProcessors
{
    public class AllTasksQueryProcessor : IAllTasksQueryProcessor
    {
        private readonly TasksDbContext _dbContext;

        public AllTasksQueryProcessor(TasksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public QueryResult<Task> GetTasks(PagedDataRequest requestInfo)
        {
            var query = _dbContext.Set<Task>();

            var totalItemCount = query.Count();

            var startIndex = ResultsPagingUtility.CalculateStartIndex(requestInfo.PageNumber, requestInfo.PageSize);

            var tasks = query.Skip(startIndex).Take(requestInfo.PageSize).ToList();

            var queryResult = new QueryResult<Task>(tasks, totalItemCount, requestInfo.PageSize);

            return queryResult;
        }
    }
}