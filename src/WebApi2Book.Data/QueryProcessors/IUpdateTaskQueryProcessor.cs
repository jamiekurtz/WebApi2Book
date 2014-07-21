// IUpdateTaskQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;
using WebApi2Book.Data.Entities;

namespace WebApi2Book.Data.QueryProcessors
{
    /// <summary>
    /// Updates Task entities.
    /// </summary>
    public interface IUpdateTaskQueryProcessor
    {
        /// <summary>
        /// Updates the specified task.
        /// </summary>
        /// <param name="taskId">Uniquely identifies the Task to update.</param>
        /// <param name="updatedPropertyValueMap">
        /// Associates names of updated properties to the corresponding new values. Note that the
        /// "Assignees" property value must either be null (to remove all assignees) or an
        /// enumerable of User Ids (type long).
        /// </param>
        /// <returns>The updated task.</returns>
        Task GetUpdatedTask(long taskId, Dictionary<string, object> updatedPropertyValueMap);

        Task ReplaceTaskUsers(long taskId, IEnumerable<long> userIds);
        Task DeleteTaskUsers(long taskId);
        Task AddTaskUser(long taskId, long userId);
        Task DeleteTaskUser(long taskId, long userId);
    }
}