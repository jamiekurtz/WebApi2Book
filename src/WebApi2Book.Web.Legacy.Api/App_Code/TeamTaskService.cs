// TeamTaskService.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Web.Services;
using WebApi2Book.Web.Api.Models;

/// <summary>
///     Summary description for TeamTaskService
/// </summary>
/// <remarks>
///     This would obviously normally fetch data from the database, but we're keeping things very
///     simple to maintain focus on the Web API, not on legacy web services or data access technologies.
///     Besides, this is the web service that has been replaced by the new REST-based Web API.
/// </remarks>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class TeamTaskService : WebService
{
    [WebMethod]
    public Task[] GetTasks()
    {
        return new[]
        {
            new Task {TaskId = 1, Subject = "Fix printer."},
            new Task {TaskId = 2, Subject = "Finish API design."}
        };
    }

    [WebMethod]
    public Task GetTaskById(int taskId)
    {
        return new Task {TaskId = taskId, Subject = "Topic #" + taskId};
    }
}