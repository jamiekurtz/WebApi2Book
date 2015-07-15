using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace EFCommonContext
{
    internal class CommonDbContext : DbContext, IDbContext
    {
        internal CommonDbContext(string nameOrConnectionString, DbCompiledModel model)
            : base(nameOrConnectionString, model)
        {
        }
    }
}