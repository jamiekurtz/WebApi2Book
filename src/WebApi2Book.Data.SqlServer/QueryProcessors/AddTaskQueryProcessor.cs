// AddTaskQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2015.

using System.Linq;
using EFCommonContext;
using WebApi2Book.Common;
using WebApi2Book.Common.Security;
using WebApi2Book.Data.Entities;
using WebApi2Book.Data.Exceptions;
using WebApi2Book.Data.QueryProcessors;

namespace WebApi2Book.Data.SqlServer.QueryProcessors
{
    public class AddTaskQueryProcessor : IAddTaskQueryProcessor
    {
        private readonly IDateTime _dateTime;
        private readonly IDbContext _dbContext;
        private readonly IUserSession _userSession;

        public AddTaskQueryProcessor(IDbContext dbContext, IUserSession userSession, IDateTime dateTime)
        {
            _dbContext = dbContext;
            _userSession = userSession;
            _dateTime = dateTime;
        }

        public void AddTask(Task task)
        {
            task.Status = _dbContext.Set<Status>().SingleOrDefault(x => x.Name == "Not Started");
            task.CreatedDate = _dateTime.UtcNow;
            task.CreatedBy = _dbContext.Set<User>().SingleOrDefault(x => x.Username == _userSession.Username);

            if (task.Users != null && task.Users.Any())
            {
                for (var i = 0; i < task.Users.Count; ++i)
                {
                    var user = task.Users[i];
                    var persistedUser = _dbContext.Set<User>().Find(user.UserId);
                    if (persistedUser == null)
                    {
                        throw new ChildObjectNotFoundException("User not found");
                    }
                    task.Users[i] = persistedUser;
                }
            }

            _dbContext.SaveChanges();
        }
    }
}