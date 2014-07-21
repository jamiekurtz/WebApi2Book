// TaskByIdQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using NHibernate;
using WebApi2Book.Data.Entities;
using WebApi2Book.Data.QueryProcessors;

namespace WebApi2Book.Data.SqlServer.QueryProcessors
{
    public class TaskByIdQueryProcessor : ITaskByIdQueryProcessor
    {
        private readonly ISession _session;

        public TaskByIdQueryProcessor(ISession session)
        {
            _session = session;
        }

        public Task GetTask(long taskId)
        {
            var task = _session.Get<Task>(taskId);
            return task;
        }
    }
}