// Class1.cs
// Copyright Jamie Kurtz, Brian Wortman 2015.

using System.Data.Entity;

namespace EFCommonContext
{
    public interface IDbContext
    {
        Database Database { get; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
        void Dispose();
    }
}