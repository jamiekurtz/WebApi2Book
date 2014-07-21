using WebApi2Book.Data;
using WebApi2Book.Web.Api.Models;
namespace WebApi2Book.Web.Api.InquiryProcessing
{
    public interface IAllTasksInquiryProcessor
    {
        PagedDataInquiryResponse<Task> GetTasks(PagedDataRequest requestInfo);
    }
}