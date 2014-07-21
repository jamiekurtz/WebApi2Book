// UserByIdInquiryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Data.Exceptions;
using WebApi2Book.Data.QueryProcessors;
using WebApi2Book.Web.Api.LinkServices;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.InquiryProcessing
{
    public class UserByIdInquiryProcessor : IUserByIdInquiryProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly IUserByIdQueryProcessor _queryProcessor;
        private readonly IUserLinkService _userLinkService;

        public UserByIdInquiryProcessor(IUserByIdQueryProcessor queryProcessor, IAutoMapper autoMapper,
            IUserLinkService userLinkService)
        {
            _queryProcessor = queryProcessor;
            _autoMapper = autoMapper;
            _userLinkService = userLinkService;
        }

        public User GetUser(long userId)
        {
            var userEntity = _queryProcessor.GetUser(userId);
            if (userEntity == null)
            {
                throw new RootObjectNotFoundException("User not found");
            }

            var user = _autoMapper.Map<User>(userEntity);

            _userLinkService.AddLinks(user);

            return user;
        }
    }
}