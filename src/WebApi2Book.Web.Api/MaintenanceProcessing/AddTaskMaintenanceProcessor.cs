using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Data.QueryProcessors;
using WebApi2Book.Web.Api.Models;
namespace WebApi2Book.Web.Api.MaintenanceProcessing
{
    public class AddTaskMaintenanceProcessor : IAddTaskMaintenanceProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly IAddTaskQueryProcessor _queryProcessor;
        public AddTaskMaintenanceProcessor(IAddTaskQueryProcessor queryProcessor,
        IAutoMapper autoMapper)
        {
            _queryProcessor = queryProcessor;
            _autoMapper = autoMapper;
        }
        public Task AddTask(NewTask newTask)
        {
            var taskEntity = _autoMapper.Map<Data.Entities.Task>(newTask);
            _queryProcessor.AddTask(taskEntity);
            var task = _autoMapper.Map<Task>(taskEntity);
            return task;
        }
    }
}