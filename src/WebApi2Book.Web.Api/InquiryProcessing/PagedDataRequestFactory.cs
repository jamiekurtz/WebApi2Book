// PagedDataRequestFactory.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using System.Net;
using System.Net.Http;
using System.Web;
using log4net;
using WebApi2Book.Common;
using WebApi2Book.Common.Extensions;
using WebApi2Book.Common.Logging;
using WebApi2Book.Data;

namespace WebApi2Book.Web.Api.InquiryProcessing
{
    public class PagedDataRequestFactory : IPagedDataRequestFactory
    {
        public const int DefaultPageSize = 25;

        public const int MaxPageSize = 50;

        private readonly ILog _log;

        public PagedDataRequestFactory(ILogManager logManager)
        {
            _log = logManager.GetLog(typeof (PagedDataRequestFactory));
        }

        public PagedDataRequest Create(Uri requestUri)
        {
            int? pageNumber;
            int? pageSize;

            try
            {
                var valueCollection = requestUri.ParseQueryString();

                pageNumber =
                    PrimitiveTypeParser.Parse<int?>(valueCollection[Constants.CommonParameterNames.PageNumber]);
                pageSize = PrimitiveTypeParser.Parse<int?>(valueCollection[Constants.CommonParameterNames.PageSize]);
            }
            catch (Exception e)
            {
                _log.Error("Error parsing input", e);
                throw new HttpException((int) HttpStatusCode.BadRequest, e.Message);
            }

            pageNumber = pageNumber.GetBoundedValue(Constants.Paging.DefaultPageNumber, Constants.Paging.MinPageNumber);
            pageSize = pageSize.GetBoundedValue(DefaultPageSize,
                Constants.Paging.MinPageSize, MaxPageSize);

            return new PagedDataRequest(pageNumber.Value, pageSize.Value);
        }
    }
}