// ITaskLinkService.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.LinkServices
{
    public interface ITaskLinkService
    {
        void AddLinks(Task task);
        Link GetAllTasksLink();
        void AddSelfLink(Task task);
        void AddLinksToChildObjects(Task task);
        Link GetSelfLink(long taskId);
    }
}