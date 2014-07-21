// PrimitiveTypeParserTests.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using System.Globalization;
using NUnit.Framework;

namespace WebApi2Book.Common.Tests
{
    [TestFixture]
    public class PrimitiveTypeParserTests
    {
        [Test]
        public void Parse_non_null_DateTime()
        {
            var now = DateTime.Now;
            var converted = PrimitiveTypeParser.Parse<DateTime?>(now.ToString(CultureInfo.InvariantCulture));

            // Compare string representations b/c the reconstituted one doesn't have the exact number of ticks.
            Assert.AreEqual(now.ToString(CultureInfo.InvariantCulture),
                converted.Value.ToString(CultureInfo.InvariantCulture));
        }

        [Test]
        public void Parse_non_null_int()
        {
            int? val = 16;
            var converted = PrimitiveTypeParser.Parse<int?>(val.ToString());

            Assert.AreEqual(val.Value, converted.Value);
        }

        [Test]
        public void Parse_null_DateTime()
        {
            Assert.IsNull(PrimitiveTypeParser.Parse<DateTime?>(null));
        }

        [Test]
        public void Parse_null_int()
        {
            Assert.IsNull(PrimitiveTypeParser.Parse<int?>(null));
        }
    }
}