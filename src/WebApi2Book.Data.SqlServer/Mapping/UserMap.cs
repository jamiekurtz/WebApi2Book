// UserMap.cs
// Copyright Jamie Kurtz, Brian Wortman 2015.

using WebApi2Book.Data.Entities;

namespace WebApi2Book.Data.SqlServer.Mapping
{
    public class UserMap : VersionedClassMap<User>
    {
        public UserMap()
        {
            ToTable("User");
            HasKey(x => x.UserId);
            Property(x => x.Firstname).IsRequired();
            Property(x => x.Lastname).IsRequired();
            Property(x => x.Username).IsRequired();
            Ignore(x => x.Tasks);
        }
    }
}