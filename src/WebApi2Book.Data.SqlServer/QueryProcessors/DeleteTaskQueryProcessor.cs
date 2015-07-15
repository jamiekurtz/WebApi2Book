// DeleteTaskQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2015.

using EFCommonContext;
using WebApi2Book.Data.Entities;
using WebApi2Book.Data.QueryProcessors;

namespace WebApi2Book.Data.SqlServer.QueryProcessors
{
    public class DeleteTaskQueryProcessor : IDeleteTaskQueryProcessor
    {
        private readonly IDbContext _dbContext;

        public DeleteTaskQueryProcessor(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DeleteTask(long taskId)
        {
            var task = _dbContext.Set<Task>().Find(taskId);
            if (task != null)
            {
                _dbContext.Set<Task>().Remove(task);
                _dbContext.SaveChanges();
            }
        }
    }
}