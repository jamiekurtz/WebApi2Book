// IAllStatusesQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;
using WebApi2Book.Data.Entities;

namespace WebApi2Book.Data.QueryProcessors
{
    public interface IAllStatusesQueryProcessor
    {
        IEnumerable<Status> GetStatuses();
    }
}