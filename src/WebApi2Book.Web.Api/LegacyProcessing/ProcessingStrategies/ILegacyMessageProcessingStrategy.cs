// ILegacyMessageProcessingStrategy.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Xml.Linq;

namespace WebApi2Book.Web.Api.LegacyProcessing.ProcessingStrategies
{
    public interface ILegacyMessageProcessingStrategy
    {
        object Execute(XElement operationElement);
        bool CanProcess(string operationName);
    }
}