// IDeleteTaskQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

namespace WebApi2Book.Data.QueryProcessors
{
    public interface IDeleteTaskQueryProcessor
    {
        void DeleteTask(long taskId);
    }
}