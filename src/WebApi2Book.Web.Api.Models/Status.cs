// Status.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

namespace WebApi2Book.Web.Api.Models
{
    public class Status
    {
        public long StatusId { get; set; }
        public string Name { get; set; }
        public int Ordinal { get; set; }
    }
}