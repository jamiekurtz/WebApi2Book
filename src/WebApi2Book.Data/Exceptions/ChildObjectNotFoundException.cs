// ChildObjectNotFoundException.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;

namespace WebApi2Book.Data.Exceptions
{
    /// <summary>
    ///     Exception thrown when a required child of the primary object is not found.
    /// </summary>
    [Serializable]
    public class ChildObjectNotFoundException : Exception
    {
        public ChildObjectNotFoundException(string message) : base(message)
        {
        }
    }
}