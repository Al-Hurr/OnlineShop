
using Microsoft.Extensions.Options;
using OnlineShop.Library.Options;

namespace OnlineShop.ConsoleTestApp
{
    public class AuthenticationServiceTest
    {
        private IdentityServerApiOptions _identityServerApiOptions;

        public AuthenticationServiceTest(IOptions<IdentityServerApiOptions> options)
        {
            _identityServerApiOptions = options.Value;
        }

        public async Task<string> RunUsersClientTests(string[] args)
        {
            return await Task.FromResult("Ok");
        }

        public async Task<string> RunRolesClientTests(string[] args)
        {
            return await Task.FromResult("Ok");
        }
    }
}
