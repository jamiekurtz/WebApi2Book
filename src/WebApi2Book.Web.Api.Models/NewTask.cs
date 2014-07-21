// NewTask.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi2Book.Web.Api.Models
{
    public class NewTask
    {
        [Required(AllowEmptyStrings = false)]
        public string Subject { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? DueDate { get; set; }

        public List<User> Assignees { get; set; }
    }
}