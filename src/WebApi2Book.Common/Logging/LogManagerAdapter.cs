// LogManagerAdapter.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using log4net;

namespace WebApi2Book.Common.Logging
{
    public class LogManagerAdapter : ILogManager
    {
        public ILog GetLog(Type typeAssociatedWithRequestedLog)
        {
            var log = LogManager.GetLogger(typeAssociatedWithRequestedLog);
            return log;
        }
    }
}