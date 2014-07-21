// TaskLinkService.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using System.Net.Http;
using WebApi2Book.Common;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.LinkServices
{
    public class TaskLinkService : ITaskLinkService
    {
        private readonly ICommonLinkService _commonLinkService;
        private readonly IStatusLinkService _statusLinkService;
        private readonly IUserLinkService _userLinkService;

        public TaskLinkService(ICommonLinkService commonLinkService,
            IStatusLinkService statusLinkService, IUserLinkService userLinkService)
        {
            _commonLinkService = commonLinkService;
            _statusLinkService = statusLinkService;
            _userLinkService = userLinkService;
        }

        public void AddLinks(Task task)
        {
            AddSelfLink(task);
            AddAllTasksLink(task);
            AddTaskUsersLink(task);
            AddAllStatusesLink(task);
            AddUpdateTaskLink(task);
            AddCreateNewTaskLink(task);
            AddDeleteUserLink(task);
            AddAddUserLink(task);
            AddDeleteUsersLink(task);
            AddReplaceUsersLink(task);
            AddWorkflowLink(task);
            AddLinksToChildObjects(task);
        }

        public void AddLinksToChildObjects(Task task)
        {
            task.Assignees.ForEach(x => _userLinkService.AddSelfLink(x));
        }

        public virtual void AddSelfLink(Task task)
        {
            task.AddLink(GetSelfLink(task.TaskId.Value));
        }

        public virtual Link GetAllTasksLink()
        {
            const string pathFragment = "tasks";
            return _commonLinkService.GetLink(pathFragment, Constants.CommonLinkRelValues.All, HttpMethod.Get);
        }

        public virtual Link GetSelfLink(long taskId)
        {
            var pathFragment = string.Format("tasks/{0}", taskId);
            var link = _commonLinkService.GetLink(pathFragment, Constants.CommonLinkRelValues.Self, HttpMethod.Get);
            return link;
        }

        public virtual void AddReplaceUsersLink(Task task)
        {
            var pathFragment = string.Format("tasks/{0}/users", task.TaskId.Value);
            var link = _commonLinkService.GetLink(pathFragment, "replaceUsers", HttpMethod.Put);
            task.AddLink(link);
        }

        public virtual void AddDeleteUsersLink(Task task)
        {
            var pathFragment = string.Format("tasks/{0}/users", task.TaskId.Value);
            var link = _commonLinkService.GetLink(pathFragment, "deleteUsers", HttpMethod.Delete);
            task.AddLink(link);
        }

        public virtual void AddAddUserLink(Task task)
        {
            var pathFragment = string.Format("tasks/{0}/users/userId", task.TaskId.Value);
            var link = _commonLinkService.GetLink(pathFragment, "addUser", HttpMethod.Put);
            task.AddLink(link);
        }

        public virtual void AddDeleteUserLink(Task task)
        {
            var pathFragment = string.Format("tasks/{0}/users/userId", task.TaskId.Value);
            var link = _commonLinkService.GetLink(pathFragment, "deleteUser", HttpMethod.Delete);
            task.AddLink(link);
        }

        public virtual void AddCreateNewTaskLink(Task task)
        {
            const string pathFragment = "tasks";
            var link = _commonLinkService.GetLink(pathFragment, "createTask", HttpMethod.Post);
            task.AddLink(link);
        }

        public virtual void AddUpdateTaskLink(Task task)
        {
            var pathFragment = string.Format("tasks/{0}", task.TaskId.Value);
            var link = _commonLinkService.GetLink(pathFragment, "updateTask", HttpMethod.Put);
            task.AddLink(link);
        }

        public virtual void AddWorkflowLink(Task task)
        {
            const int notStarted = 1;
            const int inProgress = 2;
            const int completed = 3;

            string pathFragment;
            string relValue;

            switch (task.Status.StatusId)
            {
                case notStarted:
                    pathFragment = string.Format("tasks/{0}/activations", task.TaskId.Value);
                    relValue = "activateTask";
                    break;
                case inProgress:
                    pathFragment = string.Format("tasks/{0}/completions", task.TaskId.Value);
                    relValue = "completeTask";
                    break;
                case completed:
                    pathFragment = string.Format("tasks/{0}/reactivations", task.TaskId.Value);
                    relValue = "re-activateTask";
                    break;
                default:
                    throw new InvalidOperationException("Invalid status: " + task.Status.StatusId);
            }

            var link = _commonLinkService.GetLink(pathFragment, relValue, HttpMethod.Put);
            task.AddLink(link);
        }

        public virtual void AddAllStatusesLink(Task task)
        {
            task.AddLink(_statusLinkService.GetAllStatusesLink());
        }

        public void AddTaskUsersLink(Task task)
        {
            var pathFragment = string.Format("tasks/{0}/users", task.TaskId.Value);
            var link = _commonLinkService.GetLink(pathFragment, "taskUsers", HttpMethod.Get);
            task.AddLink(link);
        }

        public virtual void AddAllTasksLink(Task task)
        {
            task.AddLink(GetAllTasksLink());
        }
    }
}