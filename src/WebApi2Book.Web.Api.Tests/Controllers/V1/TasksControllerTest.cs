// TasksControllerTest.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using NUnit.Framework;
using WebApi2Book.Data;
using WebApi2Book.Web.Api.Controllers.V1;
using WebApi2Book.Web.Api.Models;
using WebApi2Book.Web.Common.Tests;

namespace WebApi2Book.Web.Api.Tests.Controllers.V1
{
    [TestFixture]
    public class TasksControllerTest
    {
        [SetUp]
        public void SetUp()
        {
            _tasksControllerDependencyBlockMock = new TasksControllerDependencyBlockMock();

            _controller = new TasksController(_tasksControllerDependencyBlockMock.Object);
        }

        private TasksControllerDependencyBlockMock _tasksControllerDependencyBlockMock;

        private TasksController _controller;

        [Test]
        public void GetTasks_returns_correct_response()
        {
            var requestMessage = HttpRequestMessageFactory.CreateRequestMessage();
            var request = new PagedDataRequest(1, 25);
            var response = new PagedDataInquiryResponse<Task>();

            _tasksControllerDependencyBlockMock.PagedDataRequestFactoryMock.Setup(
                x => x.Create(requestMessage.RequestUri)).Returns(request);
            _tasksControllerDependencyBlockMock.AllTasksInquiryProcessorMock.Setup(x => x.GetTasks(request))
                .Returns(response);

            var actualResponse = _controller.GetTasks(requestMessage);

            Assert.AreSame(response, actualResponse);
        }
    }
}