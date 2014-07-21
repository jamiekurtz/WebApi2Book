// HttpRequestMessageFactory.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using System.Net.Http;
using System.Web.Http;

namespace WebApi2Book.Web.Common.Tests
{
    public static class HttpRequestMessageFactory
    {
        public static HttpRequestMessage CreateRequestMessage(HttpMethod method = null, string uriString = null)
        {
            method = method ?? HttpMethod.Get;
            var uri = string.IsNullOrWhiteSpace(uriString)
                ? new Uri("http://localhost:12345/api/whatever")
                : new Uri(uriString);
            var requestMessage = new HttpRequestMessage(method, uri);
            requestMessage.SetConfiguration(new HttpConfiguration());
            return requestMessage;
        }
    }
}