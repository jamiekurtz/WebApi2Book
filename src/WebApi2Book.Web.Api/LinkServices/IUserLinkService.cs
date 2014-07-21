using WebApi2Book.Web.Api.Models;
namespace WebApi2Book.Web.Api.LinkServices
{
    public interface IUserLinkService
    {
        void AddSelfLink(User user);
    }
}