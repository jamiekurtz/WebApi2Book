// UserLinkService.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Net.Http;
using WebApi2Book.Common;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.LinkServices
{
    public class UserLinkService : IUserLinkService
    {
        private readonly ICommonLinkService _commonLinkService;

        public UserLinkService(ICommonLinkService commonLinkService)
        {
            _commonLinkService = commonLinkService;
        }

        public void AddLinks(User user)
        {
            AddSelfLink(user);
            AddAllUsersLink(user);
        }

        public virtual void AddSelfLink(User user)
        {
            user.AddLink(GetSelfLink(user));
        }

        public virtual Link GetAllUsersLink()
        {
            const string pathFragment = "users";
            return _commonLinkService.GetLink(pathFragment, Constants.CommonLinkRelValues.All, HttpMethod.Get);
        }

        public virtual void AddAllUsersLink(User user)
        {
            user.AddLink(GetAllUsersLink());
        }

        public virtual Link GetSelfLink(User user)
        {
            var pathFragment = string.Format("users/{0}", user.UserId);
            var link = _commonLinkService.GetLink(pathFragment, Constants.CommonLinkRelValues.Self, HttpMethod.Get);
            return link;
        }
    }
}