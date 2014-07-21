using WebApi2Book.Web.Api.Models;
namespace WebApi2Book.Web.Api.InquiryProcessing
{
    public interface ITaskByIdInquiryProcessor
    {
        Task GetTask(long taskId);
    }
}