using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Library.Constants;

namespace OnlineShop.UserManagmentService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost(RepoActions.Add)]
        public Task<IdentityResult> Add(IdentityRole role)
        {
            return _roleManager.CreateAsync(role);
        }

        [HttpPost(RepoActions.Update)]
        public async Task<IdentityResult> Update(string name, IdentityRole role)
        {
            var roleForUpdate = await _roleManager.FindByNameAsync(name);
            if (roleForUpdate == null)
            {
                return UserNotFound(name);
            }

            roleForUpdate.Name = role.Name;

            return await _roleManager.UpdateAsync(roleForUpdate);
        }

        [HttpPost(RepoActions.Remove)]
        public Task<IdentityResult> Remove(IdentityRole role)
        {
            return _roleManager.DeleteAsync(role);
        }

        [HttpGet]
        public Task<IdentityRole> Get(string name)
        {
            return _roleManager.FindByNameAsync(name);
        }

        [HttpGet(RepoActions.GetAll)]
        public IEnumerable<IdentityRole> GetAll()
        {
            return _roleManager.Roles.AsEnumerable();
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