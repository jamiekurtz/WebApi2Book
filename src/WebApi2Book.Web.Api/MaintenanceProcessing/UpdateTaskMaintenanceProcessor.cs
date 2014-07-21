using System.Linq;
using Newtonsoft.Json.Linq;
using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Data.QueryProcessors;
using WebApi2Book.Web.Api.Models;
using WebApi2Book.Web.Common;
using PropertyValueMapType = System.Collections.Generic.Dictionary<string, object>;

namespace WebApi2Book.Web.Api.MaintenanceProcessing
{
    public class UpdateTaskMaintenanceProcessor : IUpdateTaskMaintenanceProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly IUpdateTaskQueryProcessor _queryProcessor;
        private readonly IUpdateablePropertyDetector _updateablePropertyDetector;

        public UpdateTaskMaintenanceProcessor(IUpdateTaskQueryProcessor queryProcessor,
            IAutoMapper autoMapper,
            IUpdateablePropertyDetector updateablePropertyDetector)
        {
            _queryProcessor = queryProcessor;
            _autoMapper = autoMapper;
            _updateablePropertyDetector = updateablePropertyDetector;
        }

        public Task UpdateTask(long taskId, object taskFragment)
        {
            var taskFragmentAsJObject = (JObject) taskFragment;
            var taskContainingUpdateData = taskFragmentAsJObject.ToObject<Task>();
            var updatedPropertyValueMap = GetPropertyValueMap(
                taskFragmentAsJObject, taskContainingUpdateData);
            var updatedTaskEntity = _queryProcessor.GetUpdatedTask(taskId, updatedPropertyValueMap);
            var task = _autoMapper.Map<Task>(updatedTaskEntity);
            return task;
        }

        public virtual PropertyValueMapType GetPropertyValueMap(
            JObject taskFragment, Task taskContainingUpdateData)
        {
            var namesOfModifiedProperties = _updateablePropertyDetector
                .GetNamesOfPropertiesToUpdate<Task>(taskFragment).ToList();
            var propertyInfos = typeof (Task).GetProperties();
            var updatedPropertyValueMap = new PropertyValueMapType();

            foreach (var propertyName in namesOfModifiedProperties)
            {
                var propertyValue = propertyInfos.Single(
                    x => x.Name == propertyName).GetValue(taskContainingUpdateData);
                updatedPropertyValueMap.Add(propertyName, propertyValue);
            }
            return updatedPropertyValueMap;
        }
    }
}