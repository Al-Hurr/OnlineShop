using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using OnlineShop.Library.Constants;
using OnlineShop.Library.Options;
using OnlineShop.Library.UserManagmentService;

namespace OnlineShop.Library.Clients.UserManagmentService
{
    public class RolesClient : UserManagmentBaseClient, IRolesClient
    {
        public RolesClient(HttpClient httpClient, IOptions<ServiceAddressOptions> opts) : base(httpClient, opts)
        {

        }

        public async Task<IdentityResult> Add(IdentityRole role)
            => await SendPostRequest(role, $"{RolesControllerRoutes.ControllerName}/{RepoActions.Add}");

        public async Task<UserManagmentServiceResponse<IdentityRole>> Get(string name)
            => await SendGetRequest<IdentityRole>($"{RolesControllerRoutes.ControllerName}?name={name}");

        public async Task<UserManagmentServiceResponse<IEnumerable<IdentityRole>>> GetAll()
            => await SendGetRequest<IEnumerable<IdentityRole>>($"{RolesControllerRoutes.ControllerName}/{RepoActions.GetAll}");

        public async Task<IdentityResult> Remove(IdentityRole role)
            => await SendPostRequest<IdentityRole>(role, $"{RolesControllerRoutes.ControllerName}/{RepoActions.Remove}");

        public async Task<IdentityResult> Update(string name, IdentityRole role)
            => await SendPostRequest<IdentityRole>(role, $"{RolesControllerRoutes.ControllerName}/{RepoActions.Update}?name={name}");
    }
}
