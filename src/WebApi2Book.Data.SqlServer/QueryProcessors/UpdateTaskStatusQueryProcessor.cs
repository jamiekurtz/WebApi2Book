// UpdateTaskStatusQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2015.

using System.Linq;
using EFCommonContext;
using WebApi2Book.Data.Entities;
using WebApi2Book.Data.QueryProcessors;

namespace WebApi2Book.Data.SqlServer.QueryProcessors
{
    public class UpdateTaskStatusQueryProcessor : IUpdateTaskStatusQueryProcessor
    {
        private readonly IDbContext _dbContext;

        public UpdateTaskStatusQueryProcessor(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void UpdateTaskStatus(Task taskToUpdate, string statusName)
        {
            var status = _dbContext.Set<Status>().SingleOrDefault(x => x.Name == statusName);

            taskToUpdate.Status = status;

            _dbContext.SaveChanges();
        }
    }
}