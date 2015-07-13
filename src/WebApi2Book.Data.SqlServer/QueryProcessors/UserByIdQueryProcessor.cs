// UserByIdQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2015.

using WebApi2Book.Data.Entities;
using WebApi2Book.Data.QueryProcessors;

namespace WebApi2Book.Data.SqlServer.QueryProcessors
{
    public class UserByIdQueryProcessor : IUserByIdQueryProcessor
    {
        private readonly TasksDbContext _dbContext;

        public UserByIdQueryProcessor(TasksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User GetUser(long userId)
        {
            var user = _dbContext.Set<User>().Find(userId);
            return user;
        }
    }
}