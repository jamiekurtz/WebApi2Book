// AllTasksInquiryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;
using System.Linq;
using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Data;
using WebApi2Book.Data.QueryProcessors;
using WebApi2Book.Web.Api.LinkServices;
using WebApi2Book.Web.Api.Models;
using PagedTaskDataInquiryResponse =
    WebApi2Book.Web.Api.Models.PagedDataInquiryResponse<WebApi2Book.Web.Api.Models.Task>;

namespace WebApi2Book.Web.Api.InquiryProcessing
{
    public class AllTasksInquiryProcessor : IAllTasksInquiryProcessor
    {
        public const string QueryStringFormat = "pagenumber={0}&pagesize={1}";

        private readonly IAutoMapper _autoMapper;
        private readonly ICommonLinkService _commonLinkService;
        private readonly IAllTasksQueryProcessor _queryProcessor;
        private readonly ITaskLinkService _taskLinkService;

        public AllTasksInquiryProcessor(IAllTasksQueryProcessor queryProcessor, IAutoMapper autoMapper,
            ITaskLinkService taskLinkService, ICommonLinkService commonLinkService)
        {
            _queryProcessor = queryProcessor;
            _autoMapper = autoMapper;
            _taskLinkService = taskLinkService;
            _commonLinkService = commonLinkService;
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

            if (!requestInfo.ExcludeLinks)
            {
                AddLinksToInquiryResponse(inquiryResponse);
            }

            return inquiryResponse;
        }

        public virtual void AddLinksToInquiryResponse(PagedTaskDataInquiryResponse inquiryResponse)
        {
            inquiryResponse.AddLink(_taskLinkService.GetAllTasksLink());

            _commonLinkService.AddPageLinks(inquiryResponse, GetCurrentPageQueryString(inquiryResponse),
                GetPreviousPageQueryString(inquiryResponse),
                GetNextPageQueryString(inquiryResponse));
        }

        public virtual IEnumerable<Task> GetTasks(IEnumerable<Data.Entities.Task> taskEntities)
        {
            var tasks = taskEntities.Select(x => _autoMapper.Map<Task>(x)).ToList();

            tasks.ForEach(x =>
            {
                _taskLinkService.AddSelfLink(x);
                _taskLinkService.AddLinksToChildObjects(x);
            });

            return tasks;
        }

        public virtual string GetCurrentPageQueryString(PagedTaskDataInquiryResponse inquiryResponse)
        {
            return
                string.Format(QueryStringFormat,
                    inquiryResponse.PageNumber,
                    inquiryResponse.PageSize);
        }

        public virtual string GetPreviousPageQueryString(PagedTaskDataInquiryResponse inquiryResponse)
        {
            return
                string.Format(QueryStringFormat,
                    inquiryResponse.PageNumber - 1,
                    inquiryResponse.PageSize);
        }

        public virtual string GetNextPageQueryString(PagedTaskDataInquiryResponse inquiryResponse)
        {
            return string.Format(QueryStringFormat,
                inquiryResponse.PageNumber + 1,
                inquiryResponse.PageSize);
        }
    }
}