using Newtonsoft.Json.Linq;
using NUnit.Framework;
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