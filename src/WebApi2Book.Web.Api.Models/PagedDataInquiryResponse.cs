// PagedDataInquiryResponse.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;

namespace WebApi2Book.Web.Api.Models
{
    public class PagedDataInquiryResponse<T> : IPageLinkContaining
    {
        private List<T> _items;
        private List<Link> _links;

        public List<T> Items
        {
            get { return _items ?? (_items = new List<T>()); }
            set { _items = value; }
        }

        public int PageSize { get; set; }

        public List<Link> Links
        {
            get { return _links ?? (_links = new List<Link>()); }
            set { _links = value; }
        }

        public void AddLink(Link link)
        {
            Links.Add(link);
        }

        public int PageNumber { get; set; }
        public int PageCount { get; set; }
    }
}