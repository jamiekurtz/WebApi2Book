// LegacyMessageProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using System.Collections.Generic;
using System.Xml.Linq;
using WebApi2Book.Web.Api.LegacyProcessing.ProcessingStrategies;

namespace WebApi2Book.Web.Api.LegacyProcessing
{
    public class LegacyMessageProcessor : ILegacyMessageProcessor
    {
        private readonly ILegacyMessageParser _legacyMessageParser;
        private readonly IEnumerable<ILegacyMessageProcessingStrategy> _legacyMessageProcessingStrategies;

        public LegacyMessageProcessor(ILegacyMessageParser legacyMessageParser,
            IEnumerable<ILegacyMessageProcessingStrategy> legacyMessageProcessingStrategies)
        {
            _legacyMessageParser = legacyMessageParser;
            _legacyMessageProcessingStrategies = legacyMessageProcessingStrategies;
        }

        public virtual LegacyResponse ProcessLegacyMessage(XDocument request)
        {
            var operationElement = _legacyMessageParser.GetOperationElement(request);
            var opName = operationElement.Name.LocalName;

            foreach (var legacyMessageProcessingStrategy in _legacyMessageProcessingStrategies)
            {
                if (legacyMessageProcessingStrategy.CanProcess(opName))
                {
                    var legacyResponse = new LegacyResponse
                    {
                        Request = request,
                        ProcessingResult = legacyMessageProcessingStrategy.Execute(operationElement)
                    };
                    return legacyResponse;
                }
            }

            throw new NotSupportedException(opName);
        }
    }
}