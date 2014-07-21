// TaskUsersInquiryResponse.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;

namespace WebApi2Book.Web.Api.Models
{
    public class TaskUsersInquiryResponse : ILinkContaining
    {
        private List<Link> _links;
        public long TaskId { get; set; }

        public List<User> Users { get; set; }

        public List<Link> Links
        {
            get { return _links ?? (_links = new List<Link>()); }
            set { _links = value; }
        }

        public void AddLink(Link link)
        {
            Links.Add(link);
        }

        public bool ShouldSerializeTaskId()
        {
            return false;
        }
    }
}