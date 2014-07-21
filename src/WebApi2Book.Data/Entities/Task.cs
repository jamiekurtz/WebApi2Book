// Task.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using System.Collections.Generic;

namespace WebApi2Book.Data.Entities
{
    public class Task : IVersionedEntity
    {
        public virtual long TaskId { get; set; }
        public virtual string Subject { get; set; }
        public virtual DateTime? StartDate { get; set; }
        public virtual DateTime? DueDate { get; set; }
        public virtual DateTime? CompletedDate { get; set; }
        public virtual Status Status { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual User CreatedBy { get; set; }

        private readonly IList<User> _users = new List<User>();

        public virtual IList<User> Users
        {
            get { return _users; }
        }

        public virtual byte[] Version { get; set; }
    }
}