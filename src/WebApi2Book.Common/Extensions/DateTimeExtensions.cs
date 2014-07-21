// DateTimeExtensions.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;

namespace WebApi2Book.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToUrlFriendlyDate(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd");
        }
    }
}