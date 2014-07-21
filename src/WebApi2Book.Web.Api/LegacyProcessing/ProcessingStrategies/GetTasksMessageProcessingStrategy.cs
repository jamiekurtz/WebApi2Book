// GetTasksMessageProcessingStrategy.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using WebApi2Book.Common;
using WebApi2Book.Data;
using WebApi2Book.Web.Api.InquiryProcessing;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.LegacyProcessing.ProcessingStrategies
{
    public class GetTasksMessageProcessingStrategy : ILegacyMessageProcessingStrategy
    {
        private readonly IAllTasksInquiryProcessor _inquiryProcessor;

        public GetTasksMessageProcessingStrategy(IAllTasksInquiryProcessor inquiryProcessor)
        {
            _inquiryProcessor = inquiryProcessor;
        }


        public bool CanProcess(string operationName)
        {
            return operationName == "GetTasks";
        }
        
        public object Execute(XElement operationElement)
        {
            var modelTasks =
                _inquiryProcessor.GetTasks(new PagedDataRequest(1, 500) {ExcludeLinks = true}).Items.ToArray();

            XNamespace ns = Constants.DefaultLegacyNamespace;

            using (var stream = new MemoryStream())
            {
                var serializer = new XmlSerializer(typeof (Task[]), Constants.DefaultLegacyNamespace);
                serializer.Serialize(stream, modelTasks);

                stream.Seek(0, 0);

                var xDocument = XDocument.Load(stream, LoadOptions.None);
                var categoriesAsXElements = xDocument.Descendants(ns + "Task");
                return categoriesAsXElements;
            }
        }
    }
}