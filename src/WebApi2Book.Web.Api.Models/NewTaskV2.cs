// NewTaskV2.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi2Book.Web.Api.Models
{
    /// <summary>
    ///     Web model used to support creation of a new task.
    /// </summary>
    /// <remarks>
    ///     Normally this would be separated from the V1 models... in a separate
    ///     assembly, or at least in a separate namespece. We're focusing on
    ///     Web API, so to keep this contrived example simple we'll just use
    ///     a class name that indicates the intent.
    /// </remarks>
    public class NewTaskV2
    {
        [Required(AllowEmptyStrings = false)]
        public string Subject { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? DueDate { get; set; }

        [Required]
        public User Assignee { get; set; }
    }
}