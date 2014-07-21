// TasksControllerTest.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Net;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using WebApi2Book.Web.Api.MaintenanceProcessing;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.IntegrationTests
{
    [TestFixture]
    public class TasksControllerTest
    {
        [SetUp]
        public void Setup()
        {
            _webClientHelper = new WebClientHelper();
        }

        private const string UriRoot = "http://localhost:61589/api/v1/";

        private WebClientHelper _webClientHelper;

        [Test]
        public void AddTask()
        {
            const string data = "{\"Subject\":\"Fix something important\"}";
            const string address = UriRoot + "tasks";

            var client = _webClientHelper.CreateWebClient();

            try
            {
                var responseString = client.UploadString(address, HttpMethod.Post.Method, data);

                var jsonResponse = JObject.Parse(responseString);
                Assert.IsNotNull(jsonResponse.ToObject<TaskCreatedActionResult>());
            }
            finally
            {
                client.Dispose();
            }
        }

        [Test]
        public void AddTask_denied()
        {
            const string data = "{\"Subject\":\"Fix something important\"}";
            const string address = UriRoot + "tasks";

            var client = _webClientHelper.CreateWebClient(username: "jdoe");

            try
            {
                client.UploadString(address, HttpMethod.Post.Method, data);
                Assert.Fail();
            }
            catch (WebException e)
            {
                var statusCode = ((HttpWebResponse) (e.Response)).StatusCode;
                Assert.AreEqual(HttpStatusCode.Unauthorized, statusCode);
            }
            finally
            {
                client.Dispose();
            }
        }

        [Test]
        public void GetTasks()
        {
            var client = _webClientHelper.CreateWebClient();

            try
            {
                const string address = UriRoot + "tasks";

                var responseString = client.DownloadString(address);

                var jsonResponse = JObject.Parse(responseString);
                Assert.IsNotNull(jsonResponse.ToObject<PagedDataInquiryResponse<Task>>());
            }
            finally
            {
                client.Dispose();
            }
        }
    }
}