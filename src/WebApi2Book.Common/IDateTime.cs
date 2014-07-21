// IDateTime.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;

namespace WebApi2Book.Common
{
    public interface IDateTime
    {
        DateTime UtcNow { get; }
    }
}