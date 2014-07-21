// StatusesInquiryResponse.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;

namespace WebApi2Book.Web.Api.Models
{
    public class StatusesInquiryResponse
    {
        public List<Status> Statuses { get; set; }
        public List<Link> Links { get; set; }
    }
}