// DateTimeExtensionsTests.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using NUnit.Framework;
using WebApi2Book.Common.Extensions;

namespace WebApi2Book.Common.Tests.Extensions
{
    [TestFixture]
    public class DateTimeExtensionsTests
    {
        [Test]
        public void ToUrlFriendlyDate_converts_date()
        {
            const string expected = "2013-02-13";
            var actual = DateTime.Parse(expected).ToUrlFriendlyDate();
            Assert.AreEqual(expected, actual);
        }
    }
}