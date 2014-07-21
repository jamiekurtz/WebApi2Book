using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;
using WebApi2Book.Web.Common;
using WebApi2Book.Web.Common.Routing;
namespace WebApi2Book.Web.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var constraintsResolver = new DefaultInlineConstraintResolver();
            constraintsResolver.ConstraintMap.Add("apiVersionConstraint", typeof
            (ApiVersionConstraint));
            config.MapHttpAttributeRoutes(constraintsResolver);
            config.Services.Replace(typeof(IHttpControllerSelector),
            new NamespaceHttpControllerSelector(config));
        }
    }
}