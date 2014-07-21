// WebApiConfig.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Routing;
using System.Web.Http.Tracing;
using WebApi2Book.Common.Logging;
using WebApi2Book.Web.Api.LegacyProcessing;
using WebApi2Book.Web.Common;
using WebApi2Book.Web.Common.ErrorHandling;
using WebApi2Book.Web.Common.Routing;

namespace WebApi2Book.Web.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // http://stackoverflow.com/questions/12976352/asp-net-web-api-model-binding-not-working-with-xml-data-on-post
            config.Formatters.XmlFormatter.UseXmlSerializer = true;

            config.EnableCors();

            ConfigureRouting(config);

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.Services.Replace(typeof (ITraceWriter),
                new SimpleTraceWriter(WebContainerManager.Get<ILogManager>()));

            config.Services.Add(typeof (IExceptionLogger),
                new SimpleExceptionLogger(WebContainerManager.Get<ILogManager>()));

            config.Services.Replace(typeof (IExceptionHandler), new GlobalExceptionHandler());
        }

        private static void ConfigureRouting(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "legacyRoute",
                routeTemplate: "TeamTaskService.asmx",
                defaults: null,
                constraints: null,
                handler: new LegacyAuthenticationMessageHandler(WebContainerManager.Get<ILogManager>())
                {
                    InnerHandler = new LegacyMessageHandler {InnerHandler = new HttpControllerDispatcher(config)}
                });

            var constraintsResolver = new DefaultInlineConstraintResolver();
            constraintsResolver.ConstraintMap.Add("apiVersionConstraint", typeof (ApiVersionConstraint));
            config.MapHttpAttributeRoutes(constraintsResolver);

            config.Services.Replace(typeof (IHttpControllerSelector),
                new NamespaceHttpControllerSelector(config));
        }
    }
}