// IntExtensions.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;

namespace WebApi2Book.Common.Extensions
{
    public static class IntExtensions
    {
        public static int GetBoundedValue(this int value, int min, int max)
        {
            var boundedValue = Math.Min(Math.Max(value, min), max);
            return boundedValue;
        }

        public static int GetBoundedValue(this int? value, int defaultValue, int min)
        {
            var valToBound = value ?? defaultValue;
            var boundedValue = Math.Max(valToBound, min);
            return boundedValue;
        }

        public static int GetBoundedValue(this int? value, int defaultValue, int min, int max)
        {
            var valToBound = value ?? defaultValue;
            var boundedValue = GetBoundedValue(valToBound, min, max);
            return boundedValue;
        }
    }
}