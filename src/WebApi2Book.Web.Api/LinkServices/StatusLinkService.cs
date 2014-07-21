// StatusLinkService.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Net.Http;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.LinkServices
{
    public class StatusLinkService : IStatusLinkService
    {
        private readonly ICommonLinkService _commonLinkService;

        public StatusLinkService(ICommonLinkService commonLinkService)
        {
            _commonLinkService = commonLinkService;
        }

        public virtual Link GetAllStatusesLink()
        {
            const string pathFragment = "statuses";
            const string relValue = "statuses";
            return _commonLinkService.GetLink(pathFragment, relValue, HttpMethod.Get);
        }
    }
}