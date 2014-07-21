// UserByIdQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using NHibernate;
using WebApi2Book.Data.Entities;
using WebApi2Book.Data.QueryProcessors;

namespace WebApi2Book.Data.SqlServer.QueryProcessors
{
    public class UserByIdQueryProcessor : IUserByIdQueryProcessor
    {
        private readonly ISession _session;

        public UserByIdQueryProcessor(ISession session)
        {
            _session = session;
        }

        public User GetUser(long userId)
        {
            var user = _session.Get<User>(userId);
            return user;
        }
    }
}