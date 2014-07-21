// IAllTasksQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using WebApi2Book.Data.Entities;

namespace WebApi2Book.Data.QueryProcessors
{
    public interface IAllTasksQueryProcessor
    {
        QueryResult<Task> GetTasks(PagedDataRequest requestInfo);
    }
}