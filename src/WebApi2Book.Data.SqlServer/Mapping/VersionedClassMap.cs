// VersionedClassMap.cs
// Copyright Jamie Kurtz, Brian Wortman 2015.

using System.Data.Entity.ModelConfiguration;
using WebApi2Book.Data.Entities;

namespace WebApi2Book.Data.SqlServer.Mapping
{
    public abstract class VersionedClassMap<T> : EntityTypeConfiguration<T> where T : class, IVersionedEntity
    {
        protected VersionedClassMap()
        {
            Property(x => x.Version)
                .HasColumnName("ts")
                .IsRowVersion();
        }
    }
}