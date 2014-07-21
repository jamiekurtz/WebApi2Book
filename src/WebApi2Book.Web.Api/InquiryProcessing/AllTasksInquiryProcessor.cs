using System.Collections.Generic;
using System.Linq;
using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Data;
using WebApi2Book.Data.QueryProcessors;
using WebApi2Book.Web.Api.Models;
using PagedTaskDataInquiryResponse =
    WebApi2Book.Web.Api.Models.PagedDataInquiryResponse<WebApi2Book.Web.Api.Models.Task>;

namespace WebApi2Book.Web.Api.InquiryProcessing
{
    public class AllTasksInquiryProcessor : IAllTasksInquiryProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly IAllTasksQueryProcessor _queryProcessor;

        public AllTasksInquiryProcessor(IAllTasksQueryProcessor queryProcessor, IAutoMapper autoMapper)
        {
            _queryProcessor = queryProcessor;
            _autoMapper = autoMapper;
        }

        public PagedTaskDataInquiryResponse GetTasks(PagedDataRequest requestInfo)
        {
            var queryResult = _queryProcessor.GetTasks(requestInfo);
            var tasks = GetTasks(queryResult.QueriedItems).ToList();
            var inquiryResponse = new PagedTaskDataInquiryResponse
            {
                Items = tasks,
                PageCount = queryResult.TotalPageCount,
                PageNumber = requestInfo.PageNumber,
                PageSize = requestInfo.PageSize
            };
            return inquiryResponse;
        }

        public virtual IEnumerable<Task> GetTasks(IEnumerable<Data.Entities.Task> taskEntities)
        {
            var tasks = taskEntities.Select(x => _autoMapper.Map<Task>(x)).ToList();
            return tasks;
        }
    }
}