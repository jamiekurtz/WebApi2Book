using System.Collections.Generic;
namespace WebApi2Book.Web.Api.Models
{
    public interface ILinkContaining
    {
        List<Link> Links { get; set; }
        void AddLink(Link link);
    }
}