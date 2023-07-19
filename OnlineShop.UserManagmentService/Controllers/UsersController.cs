using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Library.Constants;
using OnlineShop.Library.UserManagmentService.Models;
using OnlineShop.Library.UserManagmentService.Requests;

namespace OnlineShop.UserManagmentService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost(RepoActions.Add)]
        public Task<IdentityResult> Add(CreateUserRequest request)
        {
            return _userManager.CreateAsync(request.User, request.Password);
        }

        [HttpPost(RepoActions.Update)]
        public async Task<IdentityResult> Update(ApplicationUser user)
        {
            var userForUpdate = await _userManager.FindByNameAsync(user.UserName);
            if (userForUpdate == null)
            {
                return UserNotFound(user.UserName);
            }

            userForUpdate.FirstName = user.FirstName;
            userForUpdate.LastName = user.LastName;
            userForUpdate.DefaultAddress = user.DefaultAddress;
            userForUpdate.DeliveryAddress = user.DeliveryAddress;
            userForUpdate.PhoneNumber = user.PhoneNumber;
            userForUpdate.Email = user.Email;

            return await _userManager.UpdateAsync(userForUpdate);
        }

        [HttpPost(RepoActions.Remove)]
        public Task<IdentityResult> Remove(ApplicationUser user)
        {
            return _userManager.DeleteAsync(user);
        }

        [HttpGet]
        public Task<ApplicationUser> Get(string name)
        {
            return _userManager.FindByNameAsync(name);
        }

        [HttpGet(RepoActions.GetAll)]
        public IEnumerable<ApplicationUser> GetAll()
        {
            return _userManager.Users.AsEnumerable();
        }

        [HttpPost(RepoActions.ChangePassword)]
        public async Task<IdentityResult> ChangePassword(UserPasswordChangeRequest request)
        {
            var userForUpdate = await _userManager.FindByNameAsync(request.UserName);
            if (userForUpdate == null)
            {
                return UserNotFound(request.UserName);
            }

            return await _userManager.ChangePasswordAsync(userForUpdate, request.CurrentPassword, request.NewPassword);
        }

        [HttpPost(RepoActions.AddToRoles)]
        public async Task<IdentityResult> AddToRoles(AddRemoveRolesRequest request)
        {
            var userForUpdate = await _userManager.FindByNameAsync(request.UserName);
            if (userForUpdate == null)
            {
                return UserNotFound(request.UserName);
            }

            return await _userManager.AddToRolesAsync(userForUpdate, request.RoleNames);
        }

        [HttpPost(RepoActions.RemoveFromRole)]
        public async Task<IdentityResult> RemoveFromRole(AddRemoveRoleRequest request)
        {
            var userForUpdate = await _userManager.FindByNameAsync(request.UserName);
            if (userForUpdate == null)
            {
                return UserNotFound(request.UserName);
            }

            return await _userManager.RemoveFromRoleAsync(userForUpdate, request.RoleName);
        }

        [HttpPost(RepoActions.RemoveFromRoles)]
        public async Task<IdentityResult> RemoveFromRoles(AddRemoveRolesRequest request)
        {
            var userForUpdate = await _userManager.FindByNameAsync(request.UserName);
            if (userForUpdate == null)
            {
                return UserNotFound(request.UserName);
            }

            return await _userManager.RemoveFromRolesAsync(userForUpdate, request.RoleNames);
        }

        private IdentityResult UserNotFound(string username)
        {
            return IdentityResult.Failed(
                    new IdentityError()
                    {
                        Description = $"User {username} was not found"
                    });
        }
    }
}