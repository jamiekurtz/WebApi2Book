// XDocSoapExtensions.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Linq;
using System.Xml.Linq;

namespace WebApi2Book.Common.Extensions
{
    /// <summary>
    ///     SOAP-specific extensions related to the <see cref="XDocument" /> type.
    /// </summary>
    public static class XDocSoapExtensions
    {
        public static XElement GetSoapBody(this XDocument doc)
        {
            var body = doc.Descendants().FirstOrDefault(x => x.Name.LocalName.ToLowerInvariant() == "body");
            return body;
        }
    }
}