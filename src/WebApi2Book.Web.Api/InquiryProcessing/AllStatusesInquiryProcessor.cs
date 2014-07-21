// AllStatusesInquiryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;
using System.Linq;
using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Data.QueryProcessors;
using WebApi2Book.Web.Api.LinkServices;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.InquiryProcessing
{
    public class AllStatusesInquiryProcessor : IAllStatusesInquiryProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly IAllStatusesQueryProcessor _queryProcessor;
        private readonly IStatusLinkService _statusLinkService;

        public AllStatusesInquiryProcessor(IAllStatusesQueryProcessor queryProcessor, IAutoMapper autoMapper,
            IStatusLinkService statusLinkService)
        {
            _queryProcessor = queryProcessor;
            _autoMapper = autoMapper;
            _statusLinkService = statusLinkService;
        }

        public StatusesInquiryResponse GetStatuses()
        {
            var statusEntities = _queryProcessor.GetStatuses();

            var statuses = GetStatuses(statusEntities);
            var allLink = _statusLinkService.GetAllStatusesLink();
            var inquiryResponse = new StatusesInquiryResponse {Statuses = statuses, Links = new List<Link> {allLink}};

            return inquiryResponse;
        }

        public virtual List<Status> GetStatuses(IEnumerable<Data.Entities.Status> statusEntities)
        {
            var statuses = statusEntities.Select(x => _autoMapper.Map<Status>(x)).ToList();
            return statuses;
        }
    }
}