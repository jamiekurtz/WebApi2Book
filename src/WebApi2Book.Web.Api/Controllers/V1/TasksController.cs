using System.Net.Http;
using System.Web.Http;
using WebApi2Book.Web.Api.Models;
using WebApi2Book.Web.Common.Routing;

namespace WebApi2Book.Web.Api.Controllers.V1
{
    [ApiVersion1RoutePrefix("tasks")]
    public class TasksController : ApiController
    {
        [Route("", Name = "AddTaskRoute")]
        [HttpPost]
        public Task AddTask(HttpRequestMessage requestMessage, Task newTask)
        {
            return new Task
            {
                Subject = "In v1, newTask.Subject = " + newTask.Subject
            };
        }
    }
}