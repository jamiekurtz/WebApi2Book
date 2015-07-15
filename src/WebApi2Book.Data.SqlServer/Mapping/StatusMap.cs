// StatusMap.cs
// Copyright Jamie Kurtz, Brian Wortman 2015.

using WebApi2Book.Data.Entities;

namespace WebApi2Book.Data.SqlServer.Mapping
{
    public class StatusMap : VersionedClassMap<Status>
    {
        public StatusMap()
        {
            ToTable("Status");
            HasKey(x => x.StatusId);
            Property(x => x.Name).IsRequired();
            Property(x => x.Ordinal).IsRequired();
        }
    }
}