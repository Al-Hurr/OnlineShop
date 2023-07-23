
using Microsoft.AspNetCore.Identity;
using OnlineShop.Library.UserManagmentService;

namespace OnlineShop.Library.Clients.UserManagmentService
{
    interface IRolesClient
    {
        Task<IdentityResult> Add(IdentityRole role);

        Task<UserManagmentServiceResponse<IdentityRole>> Get(string name);

        Task<UserManagmentServiceResponse<IEnumerable<IdentityRole>>> GetAll();
            
        Task<IdentityResult> Update(string name, IdentityRole role);

        Task<IdentityResult> Remove(IdentityRole role);
    }
}
