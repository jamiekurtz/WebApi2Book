// TaskUsersLinkService.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.LinkServices
{
    public class TaskUsersLinkService : ITaskUsersLinkService
    {
        private readonly ITaskLinkService _taskLinkService;
        private readonly IUserLinkService _userLinkService;

        public TaskUsersLinkService(ITaskLinkService taskLinkService,
            IUserLinkService userLinkService)
        {
            _taskLinkService = taskLinkService;
            _userLinkService = userLinkService;
        }

        public void AddLinks(TaskUsersInquiryResponse inquiryResponse)
        {
            var taskDetailLink = _taskLinkService.GetSelfLink(inquiryResponse.TaskId);
            taskDetailLink.Rel = "Task";
            inquiryResponse.AddLink(taskDetailLink);

            inquiryResponse.Users.ForEach(x => _userLinkService.AddSelfLink(x));
        }
    }
}