// PrimitiveTypeParser.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.ComponentModel;

namespace WebApi2Book.Common
{
    public static class PrimitiveTypeParser
    {
        public static T Parse<T>(string valueAsString)
        {
            var converter = TypeDescriptor.GetConverter(typeof (T));
            var result = converter.ConvertFromString(valueAsString);
            return (T) result;
        }
    }
}