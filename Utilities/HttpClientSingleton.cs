using System;
using System.Net.Http;

namespace ConexionAppWeb_Apigateway.Utilities
{
    public sealed class HttpClientSingleton
    {
        private static readonly Lazy<HttpClient> _httpClientInstance = new Lazy<HttpClient>(() => new HttpClient());

        public static HttpClient Instance => _httpClientInstance.Value;

        private HttpClientSingleton() { }
    }
}

