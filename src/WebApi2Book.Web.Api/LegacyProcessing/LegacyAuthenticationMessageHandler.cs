// LegacyAuthenticationMessageHandler.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using WebApi2Book.Common.Logging;

namespace WebApi2Book.Web.Api.LegacyProcessing
{
    public class LegacyAuthenticationMessageHandler : DelegatingHandler
    {
        private readonly ILog _log;

        public LegacyAuthenticationMessageHandler(ILogManager logManager)
        {
            _log = logManager.GetLog(typeof (LegacyAuthenticationMessageHandler));
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            bool isAuthenticated;
            try
            {
                isAuthenticated = Authenticate(request);
            }
            catch (Exception e)
            {
                _log.Error("Failure in auth processing", e);
                return CreateUnauthorizedResponse();
            }

            if (isAuthenticated)
            {
                var response = await base.SendAsync(request, cancellationToken);
                return response.StatusCode == HttpStatusCode.Unauthorized ? CreateUnauthorizedResponse() : response;
            }

            return CreateUnauthorizedResponse();
        }

        public bool Authenticate(HttpRequestMessage request)
        {
            _log.Debug("Attempting to authenticate...");

            // TODO: do it!
            return true; //BasicSecurityService.Authenticate(GetClaims(credentials));
        }

        public HttpResponseMessage CreateUnauthorizedResponse()
        {
            var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            return response;
        }
    }
}