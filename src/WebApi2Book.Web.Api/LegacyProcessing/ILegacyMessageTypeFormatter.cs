// ILegacyMessageTypeFormatter.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;

namespace WebApi2Book.Web.Api.LegacyProcessing
{
    public interface ILegacyMessageTypeFormatter
    {
        bool CanReadType(Type type);
        bool CanWriteType(Type type);
    }
}