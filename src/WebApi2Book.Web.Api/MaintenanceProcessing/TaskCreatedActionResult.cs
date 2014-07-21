using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Task = WebApi2Book.Web.Api.Models.Task;

namespace WebApi2Book.Web.Api.MaintenanceProcessing
{
    public class TaskCreatedActionResult : IHttpActionResult
    {
        private readonly Task _createdTask;
        private readonly HttpRequestMessage _requestMessage;

        public TaskCreatedActionResult(HttpRequestMessage requestMessage,
            Task createdTask)
        {
            _requestMessage = requestMessage;
            _createdTask = createdTask;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return System.Threading.Tasks.Task.FromResult(Execute());
        }

        public HttpResponseMessage Execute()
        {
            var responseMessage = _requestMessage.CreateResponse(
                HttpStatusCode.Created, _createdTask);
            responseMessage.Headers.Location = LocationLinkCalculator.GetLocationLink(_createdTask);
            return responseMessage;
        }
    }
}