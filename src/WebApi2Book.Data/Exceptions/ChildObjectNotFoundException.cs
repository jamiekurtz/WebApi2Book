using System;
namespace WebApi2Book.Data.Exceptions
{
    [Serializable]
    public class ChildObjectNotFoundException : Exception
    {
        public ChildObjectNotFoundException(string message)
            : base(message)
        {
        }
    }
}