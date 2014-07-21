using System;

namespace WebApi2Book.Common
{
    public interface IDateTime
    {
        DateTime UtcNow { get; }
    }
}