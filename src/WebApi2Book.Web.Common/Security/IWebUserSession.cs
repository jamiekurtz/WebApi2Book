// IWebUserSession.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using WebApi2Book.Common.Security;

namespace WebApi2Book.Web.Common.Security
{
    /// <summary>
    ///     Provides access to information pertaining to the current
    ///     web request and security principal.
    /// </summary>
    public interface IWebUserSession : IUserSession
    {
        string ApiVersionInUse { get; }
        Uri RequestUri { get; }
        string HttpRequestMethod { get; }
    }
}