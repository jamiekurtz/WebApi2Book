using WebApi2Book.Data.Entities;
namespace WebApi2Book.Data.QueryProcessors
{
    public interface IAddTaskQueryProcessor
    {
        void AddTask(Task task);
    }
}