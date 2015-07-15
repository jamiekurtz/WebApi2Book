// AllStatusesQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2015.

using System.Collections.Generic;
using System.Linq;
using EFCommonContext;
using WebApi2Book.Data.Entities;
using WebApi2Book.Data.QueryProcessors;

namespace WebApi2Book.Data.SqlServer.QueryProcessors
{
    public class AllStatusesQueryProcessor : IAllStatusesQueryProcessor
    {
        private readonly IDbContext _dbContext;

        public AllStatusesQueryProcessor(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Status> GetStatuses()
        {
            var statuses = _dbContext.Set<Status>().ToList();
            return statuses;
        }
    }
}