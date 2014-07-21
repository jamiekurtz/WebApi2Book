using System.Web;
using System.Web.Http;
using WebApi2Book.Common.Logging;
using WebApi2Book.Common.Security;
using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Web.Api.Security;
using WebApi2Book.Web.Common;
using JwtAuthForWebAPI;

namespace WebApi2Book.Web.Api
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RegisterHandlers();
            new AutoMapperConfigurator().Configure(
                WebContainerManager.GetAll<IAutoMapperTypeConfigurator>());
        }

        private void RegisterHandlers()
        {
            var logManager = WebContainerManager.Get<ILogManager>();
            var userSession = WebContainerManager.Get<IUserSession>();

            GlobalConfiguration.Configuration.MessageHandlers.Add(
                new BasicAuthenticationMessageHandler(logManager,
                    WebContainerManager.Get<IBasicSecurityService>()));
            GlobalConfiguration.Configuration.MessageHandlers.Add(
                new TaskDataSecurityMessageHandler(logManager, userSession));

            var builder = new SecurityTokenBuilder();
            var reader = new ConfigurationReader();
            GlobalConfiguration.Configuration.MessageHandlers.Add(
            new JwtAuthenticationMessageHandler
            {
                AllowedAudience = reader.AllowedAudience,
                Issuer = reader.Issuer,
                SigningToken = builder.CreateFromKey(reader.SymmetricKey)
            });
        }

        protected void Application_Error()
        {
            var exception = Server.GetLastError();

            if (exception != null)
            {
                var log = WebContainerManager.Get<ILogManager>().GetLog(typeof (WebApiApplication));
                log.Error("Unhandled exception.", exception);
            }
        }
    }
}