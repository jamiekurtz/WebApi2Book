// UpdateTaskQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2015.

using System.Collections.Generic;
using System.Linq;
using EFCommonContext;
using WebApi2Book.Data.Entities;
using WebApi2Book.Data.Exceptions;
using WebApi2Book.Data.QueryProcessors;
using PropertyValueMapType = System.Collections.Generic.Dictionary<string, object>;

namespace WebApi2Book.Data.SqlServer.QueryProcessors
{
    public class UpdateTaskQueryProcessor : IUpdateTaskQueryProcessor
    {
        private readonly IDbContext _dbContext;

        public UpdateTaskQueryProcessor(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        ///     Updates the specified task.
        /// </summary>
        /// <param name="taskId">Uniquely identifies the Task to update.</param>
        /// <param name="updatedPropertyValueMap">
        ///     Associates names of updated properties to the corresponding new values.
        /// </param>
        /// <returns>The updated task.</returns>
        public Task GetUpdatedTask(long taskId, PropertyValueMapType updatedPropertyValueMap)
        {
            var task = GetValidTask(taskId);

            var propertyInfos = typeof (Task).GetProperties();
            foreach (var propertyValuePair in updatedPropertyValueMap)
            {
                propertyInfos.Single(x => x.Name == propertyValuePair.Key)
                    .SetValue(task, propertyValuePair.Value);
            }

            _dbContext.SaveChanges();

            return task;
        }

        public Task ReplaceTaskUsers(long taskId, IEnumerable<long> userIds)
        {
            var task = GetValidTask(taskId);

            UpdateTaskUsers(task, userIds, false);

            _dbContext.SaveChanges();

            return task;
        }

        public Task DeleteTaskUsers(long taskId)
        {
            var task = GetValidTask(taskId);

            UpdateTaskUsers(task, null, false);

            _dbContext.SaveChanges();

            return task;
        }

        public Task AddTaskUser(long taskId, long userId)
        {
            var task = GetValidTask(taskId);

            UpdateTaskUsers(task, new[] {userId}, true);

            _dbContext.SaveChanges();

            return task;
        }

        public Task DeleteTaskUser(long taskId, long userId)
        {
            var task = GetValidTask(taskId);

            var user = task.Users.FirstOrDefault(x => x.UserId == userId);
            if (user != null)
            {
                task.Users.Remove(user);
                _dbContext.SaveChanges();
            }

            return task;
        }

        public virtual Task GetValidTask(long taskId)
        {
            var task = _dbContext.Set<Task>().Find(taskId);
            if (task == null)
            {
                throw new RootObjectNotFoundException("Task not found");
            }

            return task;
        }

        public virtual User GetValidUser(long userId)
        {
            var user = _dbContext.Set<User>().Find(userId);
            if (user == null)
            {
                throw new ChildObjectNotFoundException("User not found");
            }

            return user;
        }

        public virtual void UpdateTaskUsers(Task task, IEnumerable<long> userIds, bool appendToExisting)
        {
            if (!appendToExisting)
            {
                task.Users.Clear();
            }

            if (userIds != null)
            {
                foreach (var user in userIds.Select(GetValidUser))
                {
                    if (!task.Users.Contains(user))
                    {
                        task.Users.Add(user);
                    }
                }
            }
        }
    }
}