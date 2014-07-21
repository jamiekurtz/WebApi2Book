using System;

namespace WebApi2Book.Common
{
    public class BusinessRuleViolationException : Exception
    {
        public BusinessRuleViolationException(string incorrectTaskStatus) :
            base(incorrectTaskStatus)
        {
        }
    }
}