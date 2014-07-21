// ICommonLinkService.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Net.Http;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.LinkServices
{
    public interface ICommonLinkService
    {
        void AddPageLinks(IPageLinkContaining linkContainer,
            string currentPageQueryString,
            string previousPageQueryString,
            string nextPageQueryString);

        Link GetLink(string pathFragment, string relValue, HttpMethod httpMethod);
    }
}