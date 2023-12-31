﻿using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OnlineShop.Library.IdentityServer;
using OnlineShop.Library.Options;

namespace OnlineShop.Library.Clients.IdentityServer
{
    public class IdentityServerClient : IIdentityServerClient
    {
        public HttpClient HttpClient { get; set; }

        public IdentityServerClient(HttpClient httpClient, IOptions<ServiceAddressOptions> opts)
        {
            HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            HttpClient.BaseAddress = new Uri(opts.Value.IdentityServer);
        }

        public async Task<Token> GetApiTokenAsync(IdentityServerApiOptions options)
        {
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("scope", options.Scope),
                new KeyValuePair<string, string>("client_secret", options.ClientSecret),
                new KeyValuePair<string, string>("grant_type", options.GrantType),
                new KeyValuePair<string, string>("client_id", options.ClientId)
            };

            var content = new FormUrlEncodedContent(keyValues);
            var response = await HttpClient.PostAsync("/connect/token", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<Token>(responseContent);

            return token!;
        }
    }
}
