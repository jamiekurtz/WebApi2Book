// WebContextFactory.cs
// Copyright Jamie Kurtz, Brian Wortman 2015.

using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Reflection;
using System.Web;

namespace EFCommonContext
{
    public class WebContextFactory : IWebContextFactory
    {
        public const string DbContextCacheKey = "DbContext";
        private readonly DbCompiledModel _compiledModel;
        private readonly string _nameOrConnectionString;

        private WebContextFactory(string nameOrConnectionString, DbCompiledModel compiledModel)
        {
            _nameOrConnectionString = nameOrConnectionString;
            _compiledModel = compiledModel;
        }

        public bool ContextExists
        {
            get { return HttpContext.Current.Items.Contains(DbContextCacheKey); }
        }

        public void Reset()
        {
            if (!ContextExists) return;

            var context = (IDbContext) HttpContext.Current.Items[DbContextCacheKey];
            context.Dispose();

            HttpContext.Current.Items.Remove(DbContextCacheKey);
        }

        public IDbContext GetNewOrCurrentContext()
        {
            if (!ContextExists)
            {
                var context = new CommonDbContext(_nameOrConnectionString, _compiledModel);
                context.Database.Connection.Open();
                HttpContext.Current.Items[DbContextCacheKey] = context;
            }

            return (IDbContext) HttpContext.Current.Items[DbContextCacheKey];
        }

        public IDbContext GetCurrentContext()
        {
            if (!ContextExists) return null;

            return (IDbContext) HttpContext.Current.Items[DbContextCacheKey];
        }

        public static WebContextFactory BuildFactory(
            string nameOrConnectionString,
            Assembly mappingAssembly,
            string providerName,
            string providerVersionHint
            )
        {
            var modelBuilder = new DbModelBuilder();
            modelBuilder.Configurations.AddFromAssembly(mappingAssembly);

            var providerInfo = new DbProviderInfo(providerName, providerVersionHint);
            var model = modelBuilder.Build(providerInfo);
            var compiledModel = model.Compile();

            return new WebContextFactory(nameOrConnectionString, compiledModel);
        }
    }
}