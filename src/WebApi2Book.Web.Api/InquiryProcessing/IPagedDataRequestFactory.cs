using System;
using WebApi2Book.Data;
namespace WebApi2Book.Web.Api.InquiryProcessing
{
    public interface IPagedDataRequestFactory
    {
        PagedDataRequest Create(Uri requestUri);
    }
}