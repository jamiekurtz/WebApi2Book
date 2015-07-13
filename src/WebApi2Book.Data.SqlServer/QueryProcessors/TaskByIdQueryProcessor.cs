// TaskByIdQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2015.

using WebApi2Book.Data.Entities;
using WebApi2Book.Data.QueryProcessors;

namespace WebApi2Book.Data.SqlServer.QueryProcessors
{
    public class TaskByIdQueryProcessor : ITaskByIdQueryProcessor
    {
        private readonly TasksDbContext _dbContext;

        public TaskByIdQueryProcessor(TasksDbContext dbContext)
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