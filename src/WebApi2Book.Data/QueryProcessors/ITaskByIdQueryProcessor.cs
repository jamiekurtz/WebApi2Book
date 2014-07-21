using WebApi2Book.Data.Entities;
namespace WebApi2Book.Data.QueryProcessors
{
    public interface ITaskByIdQueryProcessor
    {
        Task GetTask(long taskId);
    }
}