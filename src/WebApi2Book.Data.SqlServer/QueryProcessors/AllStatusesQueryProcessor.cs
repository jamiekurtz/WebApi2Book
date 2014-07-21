// AllStatusesQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;
using NHibernate;
using WebApi2Book.Data.Entities;
using WebApi2Book.Data.QueryProcessors;

namespace WebApi2Book.Data.SqlServer.QueryProcessors
{
    public class AllStatusesQueryProcessor : IAllStatusesQueryProcessor
    {
        private readonly ISession _session;

        public AllStatusesQueryProcessor(ISession session)
        {
            _session = session;
        }

        public IEnumerable<Status> GetStatuses()
        {
            var statuses = _session.QueryOver<Status>().List();
            return statuses;
        }
    }
}