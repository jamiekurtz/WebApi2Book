// UriExtensionsTest.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using NUnit.Framework;
using WebApi2Book.Common.Extensions;

namespace WebApi2Book.Common.Tests.Extensions
{
    [TestFixture]
    public class UriExtensionsTest
    {
        private const string BaseUri = "http://foo.com/api/bar";
        private const string QueryString = "?baz=123&externalmemberid=1954";

        [Test]
        public void GetBaseUri_with_query()
        {
            var uriWithQuery = new Uri(BaseUri + QueryString);
            var result = uriWithQuery.GetBaseUri();
            Assert.AreEqual(BaseUri, result.AbsoluteUri);
        }

        [Test]
        public void GetBaseUri_without_query()
        {
            var uriWithoutQuery = new Uri(BaseUri);
            var result = uriWithoutQuery.GetBaseUri();
            Assert.AreEqual(BaseUri, result.AbsoluteUri);
        }

        [Test]
        public void QueryWithoutLeadingQuestionMark_with_query()
        {
            var uriWithQuery = new Uri(BaseUri + QueryString);
            var result = uriWithQuery.QueryWithoutLeadingQuestionMark();
            Assert.AreEqual(QueryString.Substring(1), result);
        }

        [Test]
        public void QueryWithoutLeadingQuestionMark_without_query()
        {
            var uriWithQuery = new Uri(BaseUri);
            var result = uriWithQuery.QueryWithoutLeadingQuestionMark();
            Assert.AreEqual(string.Empty, result);
        }
    }
}