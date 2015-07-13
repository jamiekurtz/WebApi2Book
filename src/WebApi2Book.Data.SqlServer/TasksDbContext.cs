// TasksDbContext.cs
// Copyright Jamie Kurtz, Brian Wortman 2015.

using System.Data.Common;
using System.Data.Entity;

namespace WebApi2Book.Data.SqlServer
{
    public class TasksDbContext : DbContext
    {
        public TasksDbContext(DbConnection connection)
            : base(connection, false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(GetType().Assembly);
        }
    }
}