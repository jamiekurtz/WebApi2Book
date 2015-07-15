// TaskByIdQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2015.

using EFCommonContext;
using WebApi2Book.Data.Entities;
using WebApi2Book.Data.QueryProcessors;

namespace WebApi2Book.Data.SqlServer.QueryProcessors
{
    public class TaskByIdQueryProcessor : ITaskByIdQueryProcessor
    {
        private readonly IDbContext _dbContext;

        public TaskByIdQueryProcessor(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task GetTask(long taskId)
        {
            var task = _dbContext.Set<Task>().Find(taskId);
            return task;
        }
    }
}