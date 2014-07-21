// ILegacyMessageParser.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Xml.Linq;

namespace WebApi2Book.Web.Api.LegacyProcessing
{
    /// <summary>
    ///     Used to parse legacy web service xml/soap messages.
    /// </summary>
    public interface ILegacyMessageParser
    {
        XElement GetOperationElement(XDocument soapRequest);
        XElement GetOperationElement(XElement soapBody);
    }
}