// StatusesController.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Web.Http;
using WebApi2Book.Web.Api.InquiryProcessing;
using WebApi2Book.Web.Api.Models;
using WebApi2Book.Web.Common;
using WebApi2Book.Web.Common.Routing;

namespace WebApi2Book.Web.Api.Controllers.V1
{
    [ApiVersion1RoutePrefix("statuses")]
    [UnitOfWorkActionFilter]
    public class StatusesController : ApiController
    {
        private readonly IAllStatusesInquiryProcessor _allStatusesInquiryProcessor;

        public StatusesController(IAllStatusesInquiryProcessor allStatusesInquiryProcessor)
        {
            _allStatusesInquiryProcessor = allStatusesInquiryProcessor;
        }

        [Route("", Name = "GetStatusesRoute")]
        public StatusesInquiryResponse GetStatuses()
        {
            var inquiryResponse = _allStatusesInquiryProcessor.GetStatuses();
            return inquiryResponse;
        }
    }
}