using System;
using System.Collections.Generic;

namespace WebApi2Book.Web.Api.Models
{
    public class Task : ILinkContaining
    {
        private List<Link> _links;
        public long? TaskId { get; set; }
        public string Subject { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public Status Status { get; set; }
        public List<User> Assignees { get; set; }
        public List<Link> Links
        {
            get { return _links ?? (_links = new List<Link>()); }
            set { _links = value; }
        }
        public void AddLink(Link link)
        {
            Links.Add(link);
        }
    }
}