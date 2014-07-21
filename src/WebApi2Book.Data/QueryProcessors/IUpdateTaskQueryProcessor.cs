using System.Collections.Generic;
using WebApi2Book.Data.Entities;
namespace WebApi2Book.Data.QueryProcessors
{
    public interface IUpdateTaskQueryProcessor
    {
        Task ReplaceTaskUsers(long taskId, IEnumerable<long> userIds);
        Task DeleteTaskUsers(long taskId);
        Task AddTaskUser(long taskId, long userId);
        Task DeleteTaskUser(long taskId, long userId);
    }
}