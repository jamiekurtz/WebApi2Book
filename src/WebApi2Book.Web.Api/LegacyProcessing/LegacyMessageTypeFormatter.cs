// LegacyMessageTypeFormatter.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using WebApi2Book.Common;
using WebApi2Book.Common.Extensions;

namespace WebApi2Book.Web.Api.LegacyProcessing
{
    public class LegacyMessageTypeFormatter : MediaTypeFormatter, ILegacyMessageTypeFormatter
    {
        private readonly ILegacyMessageParser _legacyMessageParser;

        public LegacyMessageTypeFormatter(ILegacyMessageParser legacyMessageParser)
        {
            _legacyMessageParser = legacyMessageParser;

            SupportedMediaTypes.Add(new MediaTypeHeaderValue(Constants.MediaTypeNames.TextXml));
        }

        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            return type == typeof (LegacyResponse);
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content,
            TransportContext transportContext)
        {
            return Task.Factory.StartNew(() => WriteResponseToStream((LegacyResponse) value, writeStream));
        }

        public void WriteResponseToStream(LegacyResponse legacyResponse, Stream writeStream)
        {
            var request = legacyResponse.Request;

            var body = request.GetSoapBody();
            var operationElement = _legacyMessageParser.GetOperationElement(body);
            var operationElementName = operationElement.Name;
            var namespaceName = operationElementName.NamespaceName;
            var operationName = operationElementName.LocalName;

            var operationResultInnerElement = new XElement(
                string.Concat("{", namespaceName, "}", operationName, "Result"));

            var processResult = legacyResponse.ProcessingResult;
            if (processResult != null)
            {
                operationResultInnerElement.Add(processResult);
            }

            var operationResultOuterElement = new XElement(
                string.Concat("{", namespaceName, "}", operationName, "Response"));
            operationResultOuterElement.Add(operationResultInnerElement);

            operationElement.ReplaceWith(operationResultOuterElement);

            using (var outWriter = new XmlTextWriter(writeStream, Encoding.UTF8))
            {
                request.WriteTo(outWriter);
                outWriter.Flush();
            }
        }
    }
}