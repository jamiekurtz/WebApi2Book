// ILogManager.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using log4net;

namespace WebApi2Book.Common.Logging
{
    /// <summary>
    ///     Used to request <see cref="ILog" /> instances.
    /// </summary>
    public interface ILogManager
    {
        ILog GetLog(Type typeAssociatedWithRequestedLog);
    }
}