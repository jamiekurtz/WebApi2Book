// BasicAuthenticationMessageHandler.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using log4net;
using WebApi2Book.Common;
using WebApi2Book.Common.Logging;

namespace WebApi2Book.Web.Api.Security
{
    public class BasicAuthenticationMessageHandler : DelegatingHandler
    {
        public const char AuthorizationHeaderSeparator = ':';
        private const int UsernameIndex = 0;
        private const int PasswordIndex = 1;
        private const int ExpectedCredentialCount = 2;

        private readonly IBasicSecurityService _basicSecurityService;
        private readonly ILog _log;

        public BasicAuthenticationMessageHandler(ILogManager logManager, IBasicSecurityService basicSecurityService)
        {
            _basicSecurityService = basicSecurityService;
            _log = logManager.GetLog(typeof (BasicAuthenticationMessageHandler));
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                _log.Debug("Already authenticated; passing on to next handler...");
                return await base.SendAsync(request, cancellationToken);
            }

            if (!CanHandleAuthentication(request))
            {
                _log.Debug("Not a basic auth request; passing on to next handler...");
                return await base.SendAsync(request, cancellationToken);
            }

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
                return response;
            }

            return CreateUnauthorizedResponse();
        }

        public bool CanHandleAuthentication(HttpRequestMessage request)
        {
            return (request.Headers != null
                    && request.Headers.Authorization != null
                    && request.Headers.Authorization.Scheme.ToLowerInvariant() == Constants.SchemeTypes.Basic);
        }

        public bool Authenticate(HttpRequestMessage request)
        {
            _log.Debug("Attempting to authenticate...");

            var authHeader = request.Headers.Authorization;
            if (authHeader == null)
            {
                return false;
            }

            var credentialParts = GetCredentialParts(authHeader);
            if (credentialParts.Length != ExpectedCredentialCount)
            {
                return false;
            }

            return _basicSecurityService.SetPrincipal(credentialParts[UsernameIndex], credentialParts[PasswordIndex]);
        }

        public string[] GetCredentialParts(AuthenticationHeaderValue authHeader)
        {
            var encodedCredentials = authHeader.Parameter;
            var credentialBytes = Convert.FromBase64String(encodedCredentials);
            var credentials = Encoding.ASCII.GetString(credentialBytes);
            var credentialParts = credentials.Split(AuthorizationHeaderSeparator);
            return credentialParts;
        }

        public HttpResponseMessage CreateUnauthorizedResponse()
        {
            var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue(Constants.SchemeTypes.Basic));
            return response;
        }
    }
}