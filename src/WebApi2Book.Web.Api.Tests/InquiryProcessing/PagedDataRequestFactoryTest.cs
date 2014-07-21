// PagedDataRequestFactoryTest.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using System.Net;
using System.Net.Http;
using System.Web;
using log4net;
using Moq;
using NUnit.Framework;
using WebApi2Book.Common.Logging;
using WebApi2Book.Web.Api.InquiryProcessing;
using WebApi2Book.Web.Common.Tests;

namespace WebApi2Book.Web.Api.Tests.InquiryProcessing
{
    [TestFixture]
    public class PagedDataRequestFactoryTest
    {
        [SetUp]
        public void SetUp()
        {
            _logMock = new Mock<ILog>();
            _logManagerMock = new Mock<ILogManager>();

            _logManagerMock.Setup(x => x.GetLog(It.IsAny<Type>())).Returns(_logMock.Object);

            _requestFactory = new PagedDataRequestFactory(_logManagerMock.Object);
        }

        private const int DefaultPageNumber = 1;
        private const int MaxPageSize = 50;
        private const int DefaultPageSize = 25;

        private Mock<ILog> _logMock;
        private Mock<ILogManager> _logManagerMock;

        private PagedDataRequestFactory _requestFactory;

        [Test]
        public void Create_throws_HttpException_when_given_invalid_query_string()
        {
            var requestMessage = HttpRequestMessageFactory.CreateRequestMessage(HttpMethod.Get,
                "http://www.foo.com/bar?pageNumber=2&pageSize=10&pageNumber=50");

            try
            {
                _requestFactory.Create(requestMessage.RequestUri);
                Assert.Fail();
            }
            catch (HttpException e)
            {
                Assert.AreEqual((int) HttpStatusCode.BadRequest, e.GetHttpCode());
            }
        }

        [Test]
        public void Create_uses_corrected_supplied_pageNumber()
        {
            const int pageNumber = 0;

            var requestMessage = HttpRequestMessageFactory.CreateRequestMessage(HttpMethod.Get,
                string.Format("http://www.foo.com/bar?pageNumber={0}", pageNumber));

            var inquiryRequestData = _requestFactory.Create(requestMessage.RequestUri);

            Assert.AreEqual(DefaultPageNumber, inquiryRequestData.PageNumber);
        }

        [Test]
        public void Create_uses_corrected_supplied_pageSize()
        {
            const int pageSize = 2000;

            var requestMessage = HttpRequestMessageFactory.CreateRequestMessage(HttpMethod.Get,
                string.Format("http://www.foo.com/bar?pageSize={0}", pageSize));

            var inquiryRequestData = _requestFactory.Create(requestMessage.RequestUri);

            Assert.AreEqual(MaxPageSize, inquiryRequestData.PageSize);
        }

        [Test]
        public void Create_uses_default_pageNumber()
        {
            var requestMessage = HttpRequestMessageFactory.CreateRequestMessage();
            var inquiryRequestData = _requestFactory.Create(requestMessage.RequestUri);

            Assert.AreEqual(DefaultPageNumber, inquiryRequestData.PageNumber);
        }

        [Test]
        public void Create_uses_default_pageSize()
        {
            var requestMessage = HttpRequestMessageFactory.CreateRequestMessage();
            var inquiryRequestData = _requestFactory.Create(requestMessage.RequestUri);

            Assert.AreEqual(DefaultPageSize, inquiryRequestData.PageSize);
        }

        [Test]
        public void Create_uses_supplied_pageNumber()
        {
            const int pageNumber = 1;

            var requestMessage = HttpRequestMessageFactory.CreateRequestMessage(HttpMethod.Get,
                string.Format(
                    "http://www.foo.com/bar?pageNumber={0}", pageNumber));

            var inquiryRequestData = _requestFactory.Create(requestMessage.RequestUri);

            Assert.AreEqual(pageNumber, inquiryRequestData.PageNumber);
        }

        [Test]
        public void Create_uses_supplied_pageSize()
        {
            const int pageSize = 20;

            var requestMessage = HttpRequestMessageFactory.CreateRequestMessage(HttpMethod.Get,
                string.Format(
                    "http://www.foo.com/bar?pageSize={0}", pageSize));

            var inquiryRequestData = _requestFactory.Create(requestMessage.RequestUri);

            Assert.AreEqual(pageSize, inquiryRequestData.PageSize);
        }
    }
}