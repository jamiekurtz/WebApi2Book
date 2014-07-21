using WebApi2Book.Web.Api.Models;
namespace WebApi2Book.Web.Api.LinkServices
{
    public interface ITaskLinkService
    {
        Link GetAllTasksLink();
        void AddSelfLink(Task task);
        void AddLinksToChildObjects(Task task);
    }
}