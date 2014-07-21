// WebClientHelper.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using System.Net;
using System.Text;
using WebApi2Book.Common;

namespace WebApi2Book.Web.Api.IntegrationTests
{
    public class WebClientHelper
    {
        public WebClient CreateWebClient(string username = "bhogg",
            string contentType = Constants.MediaTypeNames.TextJson)
        {
            var webClient = new WebClient();

            var creds = username + ":" + "ignored";
            var bcreds = Encoding.ASCII.GetBytes(creds);
            var base64Creds = Convert.ToBase64String(bcreds);
            webClient.Headers.Add("Authorization", "Basic " + base64Creds);
            webClient.Headers.Add("Content-Type", contentType);
            return webClient;
        }
    }
}