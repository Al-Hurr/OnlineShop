using Microsoft.AspNetCore.Identity;
using OnlineShop.Library.UserManagmentService.Models;
using OnlineShop.Library.UserManagmentService.Requests;
using OnlineShop.Library.UserManagmentService.Responses;

namespace OnlineShop.Library.Clients.UserManagmentService
{
    interface IUsersClient
    {
        Task<IdentityResult> Add(CreateUserRequest request);

        Task<IdentityResult> Update(ApplicationUser user);

        Task<IdentityResult> Remove(ApplicationUser user);

        Task<UserManagmentServiceResponse<ApplicationUser>> Get(string name);

        Task<UserManagmentServiceResponse<IEnumerable<ApplicationUser>>> GetAll();
            
        Task<IdentityResult> ChangePassword(UserPasswordChangeRequest request);

        Task<IdentityResult> AddToRole(AddRemoveRoleRequest request);

        Task<IdentityResult> AddToRoles(AddRemoveRolesRequest request);

        Task<IdentityResult> RemoveFromRole(AddRemoveRoleRequest request);

        Task<IdentityResult> RemoveFromRoles(AddRemoveRolesRequest request);
    }
}