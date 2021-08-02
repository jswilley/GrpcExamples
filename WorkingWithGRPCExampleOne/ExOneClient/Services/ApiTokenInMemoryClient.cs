using IdentityModel.Client;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ex1GrpcClient
{
    public class ApiTokenInMemoryClient
    {
        private readonly ILogger<ApiTokenInMemoryClient> _logger;
        private readonly HttpClient _httpClient;
      

        private class AccessTokenItem
        {
            public string AccessToken { get; set; } = string.Empty;
            public DateTime ExpiresIn { get; set; }
        }

        private ConcurrentDictionary<string, AccessTokenItem> _accessTokens = new ConcurrentDictionary<string, AccessTokenItem>();

        public ApiTokenInMemoryClient(
         
            IHttpClientFactory httpClientFactory,
            ILoggerFactory loggerFactory)
        {
            
            _httpClient = httpClientFactory.CreateClient();
            _logger = loggerFactory.CreateLogger<ApiTokenInMemoryClient>();
        }

        public async Task<string> GetApiToken(string api_name, string api_scope, string secret)
        {
            if (_accessTokens.ContainsKey(api_name))
            {
                 _accessTokens.TryGetValue(api_name, out var accessToken);
                if (accessToken.ExpiresIn > DateTime.UtcNow)
                {
                    return accessToken.AccessToken;
                }
                else
                {
                    // remove
                    _accessTokens.TryRemove(api_name, out AccessTokenItem accessTokenItem);
                }
            }

            _logger.LogDebug($"GetApiToken new from STS for {api_name}");

            // add
            var newAccessToken = await getApiToken(api_name, api_scope, secret);
            _accessTokens.TryAdd(api_name, newAccessToken);

            return newAccessToken.AccessToken;
        }

        private async Task<AccessTokenItem> getApiToken(string api_name, string api_scope, string secret)
        {
            try
            {
                var disco = await HttpClientDiscoveryExtensions.GetDiscoveryDocumentAsync(
                    _httpClient,
                    "https://localhost:5000");

                if (disco.IsError)
                {
                    _logger.LogError($"disco error Status code: {disco.IsError}, Error: {disco.Error}");
                    throw new ApplicationException($"Status code: {disco.IsError}, Error: {disco.Error}");
                }

                var tokenResponse = await HttpClientTokenRequestExtensions.RequestClientCredentialsTokenAsync(_httpClient, new ClientCredentialsTokenRequest
                {
                    Scope = api_scope,
                    ClientSecret = secret,
                    Address = disco.TokenEndpoint,
                    ClientId = api_name
                });

                if (tokenResponse.IsError)
                {
                    _logger.LogError($"tokenResponse.IsError Status code: {tokenResponse.IsError}, Error: {tokenResponse.Error}");
                    throw new ApplicationException($"Status code: {tokenResponse.IsError}, Error: {tokenResponse.Error}");
                }

                return new AccessTokenItem
                {
                    ExpiresIn = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn),
                    AccessToken = tokenResponse.AccessToken
                };

            }
            catch (Exception e)
            {
                _logger.LogError($"Exception {e}");
                throw new ApplicationException($"Exception {e}");
            }
        }
    }

    public class AuthConfigurations
    {
        public string StsServer { get; set; }
        public string ProtectedApiUrl { get; set; }
    }
}
