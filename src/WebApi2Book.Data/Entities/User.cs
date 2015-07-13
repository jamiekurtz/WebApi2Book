// User.cs
// Copyright Jamie Kurtz, Brian Wortman 2015.

using System.Collections.Generic;

namespace WebApi2Book.Data.Entities
{
    public class User : IVersionedEntity
    {
        public virtual long UserId { get; set; }
        public virtual string Firstname { get; set; }
        public virtual string Lastname { get; set; }
        public virtual string Username { get; set; }

        public ICollection<Task> Tasks { get; set; }
        public virtual byte[] Version { get; set; }
    }
}