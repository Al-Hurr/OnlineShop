using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using OnlineShop.Library.Constants;
using OnlineShop.Library.Options;
using OnlineShop.Library.UserManagmentService.Models;
using OnlineShop.Library.UserManagmentService.Requests;
using OnlineShop.Library.UserManagmentService.Responses;

namespace OnlineShop.Library.Clients.UserManagmentService
{
    public class UsersClient : UserManagmentBaseClient, IUsersClient
    {
        public UsersClient(HttpClient httpClient, IOptions<ServiceAddressOptions> opts) : base(httpClient, opts)
        {
                
        }

        public async Task<IdentityResult> Add(CreateUserRequest request)
            => await SendPostRequest(request, $"{UsersControllerRoutes.ControllerName}/{RepoActions.Add}");

        public async Task<IdentityResult> AddToRole(AddRemoveRoleRequest request)
            => await SendPostRequest(request, $"{UsersControllerRoutes.ControllerName}/{RepoActions.AddToRole}");

        public async Task<IdentityResult> AddToRoles(AddRemoveRolesRequest request)
            => await SendPostRequest(request, $"{UsersControllerRoutes.ControllerName}/{RepoActions.AddToRoles}");

        public async Task<IdentityResult> ChangePassword(UserPasswordChangeRequest request)
            => await SendPostRequest(request, $"{UsersControllerRoutes.ControllerName}/{RepoActions.ChangePassword}");

        public async Task<UserManagmentServiceResponse<ApplicationUser>> Get(string name)
            => await SendGetRequest<ApplicationUser>($"{UsersControllerRoutes.ControllerName}?name={name}");

        public async Task<UserManagmentServiceResponse<IEnumerable<ApplicationUser>>> GetAll()
            => await SendGetRequest<IEnumerable<ApplicationUser>>($"{UsersControllerRoutes.ControllerName}/{RepoActions.GetAll}");

        public async Task<IdentityResult> Remove(ApplicationUser user)
            => await SendPostRequest(user, $"{UsersControllerRoutes.ControllerName}/{RepoActions.Remove}");

        public async Task<IdentityResult> RemoveFromRole(AddRemoveRoleRequest request)
            => await SendPostRequest(request, $"{UsersControllerRoutes.ControllerName}/{RepoActions.RemoveFromRole}");

        public async Task<IdentityResult> RemoveFromRoles(AddRemoveRolesRequest request)
            => await SendPostRequest(request, $"{UsersControllerRoutes.ControllerName}/{RepoActions.RemoveFromRoles}");

        public async Task<IdentityResult> Update(ApplicationUser user)
            => await SendPostRequest(user, $"{UsersControllerRoutes.ControllerName}/{RepoActions.Update}");
    }
}