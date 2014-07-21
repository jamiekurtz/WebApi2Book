// UsersController.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Net.Http;
using System.Web.Http;
using WebApi2Book.Web.Api.InquiryProcessing;
using WebApi2Book.Web.Api.Models;
using WebApi2Book.Web.Common;
using WebApi2Book.Web.Common.Routing;

namespace WebApi2Book.Web.Api.Controllers.V1
{
    [ApiVersion1RoutePrefix("users")]
    [UnitOfWorkActionFilter]
    public class UsersController : ApiController
    {
        private readonly IAllUsersInquiryProcessor _allUsersInquiryProcessor;
        private readonly IPagedDataRequestFactory _pagedDataRequestFactory;
        private readonly IUserByIdInquiryProcessor _userByIdInquiryProcessor;

        public UsersController(IPagedDataRequestFactory pagedDataRequestFactory,
            IAllUsersInquiryProcessor allUsersInquiryProcessor,
            IUserByIdInquiryProcessor userByIdInquiryProcessor)
        {
            _pagedDataRequestFactory = pagedDataRequestFactory;
            _allUsersInquiryProcessor = allUsersInquiryProcessor;
            _userByIdInquiryProcessor = userByIdInquiryProcessor;
        }

        [Route("", Name = "GetUsersRoute")]
        public PagedDataInquiryResponse<User> GetUsers(HttpRequestMessage requestMessage)
        {
            var request = _pagedDataRequestFactory.Create(requestMessage.RequestUri);

            var inquiryResponse = _allUsersInquiryProcessor.GetUsers(request);
            return inquiryResponse;
        }

        [Route("{id:long}", Name = "GetUserRoute")]
        public User GetUser(long id)
        {
            var user = _userByIdInquiryProcessor.GetUser(id);
            return user;
        }
    }
}