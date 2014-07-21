// UpdateTaskStatusQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using NHibernate;
using WebApi2Book.Data.Entities;
using WebApi2Book.Data.QueryProcessors;

namespace WebApi2Book.Data.SqlServer.QueryProcessors
{
    public class UpdateTaskStatusQueryProcessor : IUpdateTaskStatusQueryProcessor
    {
        private readonly ISession _session;

        public UpdateTaskStatusQueryProcessor(ISession session)
        {
            _session = session;
        }

        public void UpdateTaskStatus(Task taskToUpdate, string statusName)
        {
            var status = _session.QueryOver<Status>().Where(x => x.Name == statusName).SingleOrDefault();

            taskToUpdate.Status = status;

            _session.SaveOrUpdate(taskToUpdate);
        }
    }
}