using OnlineShop.Library.IdentityServer;
using OnlineShop.Library.Options;

namespace OnlineShop.Library.Clients.IdentityServer
{
    interface IIdentityServerClient
    {
        Task<Token> GetApiTokenAsync(IdentityServerApiOptions options);
    }
}
