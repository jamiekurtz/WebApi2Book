// LegacyMessageHandler.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebApi2Book.Web.Common;

namespace WebApi2Book.Web.Api.LegacyProcessing
{
    public class LegacyMessageHandler : DelegatingHandler
    {
        public virtual ILegacyMessageProcessor LegacyMessageProcessor
        {
            get { return WebContainerManager.Get<ILegacyMessageProcessor>(); }
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var requestContentAsString = request.Content.ReadAsStringAsync().Result;
            var requestContentAsDocument = XDocument.Parse(requestContentAsString);

            var legacyResponse = LegacyMessageProcessor.ProcessLegacyMessage(requestContentAsDocument);

            var responseMsg = request.CreateResponse(HttpStatusCode.OK, legacyResponse);

            return Task.FromResult(responseMsg);
        }
    }
}