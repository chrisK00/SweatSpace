using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SweatSpace.Tests.Integration.Setup
{
    public static class HttpClientExtensions
    {
        public static HttpClient WithAdminAuth(this HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(FakeAuthHandler.AuthType);
            return client;
        }

        public static HttpClient ForController(this HttpClient client, string controller)
        {
            client.BaseAddress = new Uri($"{client.BaseAddress.AbsoluteUri}{controller}");
            return client;
        }
    }
}
