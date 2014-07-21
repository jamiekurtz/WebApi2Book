using System.Collections.Generic;

namespace WebApi2Book.Web.Api.Models
{
    public class User
    {
        private List<Link> _links;
        public long UserId { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
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