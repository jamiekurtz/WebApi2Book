using System.Collections.Generic;
using WebApi2Book.Data.Entities;
using PropertyValueMapType = System.Collections.Generic.Dictionary<string, object>;

namespace WebApi2Book.Data.QueryProcessors
{
    public interface IUpdateTaskQueryProcessor
    {
        Task GetUpdatedTask(long taskId, PropertyValueMapType updatedPropertyValueMap);

        Task ReplaceTaskUsers(long taskId, IEnumerable<long> userIds);
        Task DeleteTaskUsers(long taskId);
        Task AddTaskUser(long taskId, long userId);
        Task DeleteTaskUser(long taskId, long userId);
    }
}