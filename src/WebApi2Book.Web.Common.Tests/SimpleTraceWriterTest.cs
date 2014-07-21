// SimpleTraceWriterTest.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using System.Net;
using System.Web.Http.Tracing;
using log4net;
using Moq;
using NUnit.Framework;
using WebApi2Book.Common.Logging;

namespace WebApi2Book.Web.Common.Tests
{
    [TestFixture]
    public class SimpleTraceWriterTest
    {
        [SetUp]
        public void SetUp()
        {
            _logManagerMock = new Mock<ILogManager>();
            _logMock = new Mock<ILog>();

            _logManagerMock.Setup(x => x.GetLog(typeof (SimpleTraceWriter))).Returns(_logMock.Object);

            _simpleTraceWriter = new SimpleTraceWriterTestDouble(_logManagerMock.Object);
        }

        private const string TraceFormat =
            "RequestId={0};{1}Kind={2};{3}Status={4};{5}Operation={6};{7}Operator={8};{9}Category={10}{11}Request={12}{13}Message={14}";

        private Mock<ILogManager> _logManagerMock;
        private Mock<ILog> _logMock;
        private SimpleTraceWriterTestDouble _simpleTraceWriter;

        private TraceRecord GetTraceRecord(TraceLevel traceLevel)
        {
            return new TraceRecord(HttpRequestMessageFactory.CreateRequestMessage(), "foo", traceLevel)
            {
                Kind = TraceKind.Begin,
                Status = HttpStatusCode.Ambiguous,
                Operation = "baz",
                Operator = "jdoe",
                Category = "cat",
                Message = "message"
            };
        }

        private class SimpleTraceWriterTestDouble : SimpleTraceWriter
        {
            public SimpleTraceWriterTestDouble(ILogManager logManager) : base(logManager)
            {
            }

            public Action<TraceRecord> WriteTrace_TestDouble { get; set; }

            public override void WriteTrace(TraceRecord rec)
            {
                if (WriteTrace_TestDouble == null)
                {
                    base.WriteTrace(rec);
                }
                else
                {
                    WriteTrace_TestDouble(rec);
                }
            }
        }

        [Test]
        public void Trace_invokes_WriteTrace_with_correct_data()
        {
            var requestMessage = HttpRequestMessageFactory.CreateRequestMessage();
            const string category = "foo";
            const TraceLevel level = TraceLevel.Debug;

            TraceRecord actualTraceRecord = null;
            _simpleTraceWriter.WriteTrace_TestDouble = record => actualTraceRecord = record;

            _simpleTraceWriter.Trace(requestMessage, category, level, record => { });

            Assert.AreEqual(requestMessage, actualTraceRecord.Request, "Incorrect request");
            Assert.AreEqual(category, actualTraceRecord.Category, "Incorrect category");
            Assert.AreEqual(level, actualTraceRecord.Level, "Incorrect level");
        }

        [Test]
        public void Trace_invokes_action()
        {
            var requestMessage = HttpRequestMessageFactory.CreateRequestMessage();
            const string category = "foo";
            const TraceLevel level = TraceLevel.Debug;
            var wasInvoked = false;

            _simpleTraceWriter.WriteTrace_TestDouble = record => { };

            _simpleTraceWriter.Trace(requestMessage, category, level, record => { wasInvoked = true; });

            Assert.IsTrue(wasInvoked);
        }

        [Test]
        public void WriteTrace_writes_debug_trace()
        {
            var rec = GetTraceRecord(TraceLevel.Debug);
            _simpleTraceWriter.WriteTrace(rec);
            _logMock.Verify(x => x.DebugFormat(TraceFormat,
                rec.RequestId,
                Environment.NewLine,
                rec.Kind,
                Environment.NewLine,
                rec.Status,
                Environment.NewLine,
                rec.Operation,
                Environment.NewLine,
                rec.Operator,
                Environment.NewLine,
                rec.Category,
                Environment.NewLine,
                rec.Request,
                Environment.NewLine,
                rec.Message));
        }

        [Test]
        public void WriteTrace_writes_error_trace()
        {
            var rec = GetTraceRecord(TraceLevel.Error);
            _simpleTraceWriter.WriteTrace(rec);
            _logMock.Verify(x => x.ErrorFormat(TraceFormat,
                rec.RequestId,
                Environment.NewLine,
                rec.Kind,
                Environment.NewLine,
                rec.Status,
                Environment.NewLine,
                rec.Operation,
                Environment.NewLine,
                rec.Operator,
                Environment.NewLine,
                rec.Category,
                Environment.NewLine,
                rec.Request,
                Environment.NewLine,
                rec.Message));
        }

        [Test]
        public void WriteTrace_writes_fatal_trace()
        {
            var rec = GetTraceRecord(TraceLevel.Fatal);
            _simpleTraceWriter.WriteTrace(rec);
            _logMock.Verify(x => x.FatalFormat(TraceFormat,
                rec.RequestId,
                Environment.NewLine,
                rec.Kind,
                Environment.NewLine,
                rec.Status,
                Environment.NewLine,
                rec.Operation,
                Environment.NewLine,
                rec.Operator,
                Environment.NewLine,
                rec.Category,
                Environment.NewLine,
                rec.Request,
                Environment.NewLine,
                rec.Message));
        }

        [Test]
        public void WriteTrace_writes_info_trace()
        {
            var rec = GetTraceRecord(TraceLevel.Info);
            _simpleTraceWriter.WriteTrace(rec);
            _logMock.Verify(x => x.InfoFormat(TraceFormat,
                rec.RequestId,
                Environment.NewLine,
                rec.Kind,
                Environment.NewLine,
                rec.Status,
                Environment.NewLine,
                rec.Operation,
                Environment.NewLine,
                rec.Operator,
                Environment.NewLine,
                rec.Category,
                Environment.NewLine,
                rec.Request,
                Environment.NewLine,
                rec.Message));
        }

        [Test]
        public void WriteTrace_writes_warn_trace()
        {
            var rec = GetTraceRecord(TraceLevel.Warn);
            _simpleTraceWriter.WriteTrace(rec);
            _logMock.Verify(x => x.WarnFormat(TraceFormat,
                rec.RequestId,
                Environment.NewLine,
                rec.Kind,
                Environment.NewLine,
                rec.Status,
                Environment.NewLine,
                rec.Operation,
                Environment.NewLine,
                rec.Operator,
                Environment.NewLine,
                rec.Category,
                Environment.NewLine,
                rec.Request,
                Environment.NewLine,
                rec.Message));
        }
    }
}