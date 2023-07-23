using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using OnlineShop.Library.Clients.IdentityServer;
using OnlineShop.Library.Clients.UserManagmentService;
using OnlineShop.Library.Common.Models;
using OnlineShop.Library.Options;
using OnlineShop.Library.UserManagmentService.Models;
using OnlineShop.Library.UserManagmentService.Requests;

namespace OnlineShop.ConsoleTestApp
{
    public class AuthenticationServiceTest
    {
        private IdentityServerApiOptions _identityServerApiOptions;
        private IdentityServerClient _identityServerClient;
        private UsersClient _usersClient;
        private RolesClient _rolesClient;

        public AuthenticationServiceTest(
            IOptions<IdentityServerApiOptions> options,
            IdentityServerClient identityServerClient,
            UsersClient usersClient,
            RolesClient rolesClient)
        {
            _identityServerApiOptions = options.Value;
            _identityServerClient = identityServerClient;
            _usersClient = usersClient;
            _rolesClient = rolesClient;
        }

        public async Task<string> RunUsersClientTests(string[] args)
        {
            Console.WriteLine("********** RunUsersClientTests **********");

            var token = await _identityServerClient.GetApiTokenAsync(_identityServerApiOptions);
            _usersClient.HttpClient.SetBearerToken(token.AccessToken);

            var userName = "UserFromTestApp";
            // ранее добавленные роли в бд
            var roleName = "testrole";
            var roleNames = new[] { "testrole", "newrole" };

            var addResult = await _usersClient.Add(new CreateUserRequest
            {
                User = new ApplicationUser
                {
                    UserName = userName
                },
                Password = "UserFromTestApp_pass_1"
            });

            Console.WriteLine($"Add result: {addResult.Succeeded}");
            Thread.Sleep(1000);

            var changePassResult = await _usersClient.ChangePassword(new UserPasswordChangeRequest
            {
                UserName = userName,
                CurrentPassword = "UserFromTestApp_pass_1",
                NewPassword = "newUserFromTestApp_pass_1"
            });

            Console.WriteLine($"ChangePassword result: {changePassResult.Succeeded}");
            Thread.Sleep(1000);

            var getUserResult = await _usersClient.Get(userName);

            Console.WriteLine($"Get result: {getUserResult.Code}");
            Thread.Sleep(1000);

            var getAllResult = await _usersClient.GetAll();

            Console.WriteLine($"GetAll result: {getAllResult.Code}");
            Thread.Sleep(1000);

            var userToUpdate = getUserResult.Payload;
            userToUpdate.DefaultAddress = new Address
            {
                AddressLine1 = "new user address 1",
                AddressLine2 = "new user address 2",
                City = "new-user-city",
                Country = "new-user-country",
                PostalCode = "12345"
            };
            var updateUserResult = await _usersClient.Update(userToUpdate);

            Console.WriteLine($"Update result: {updateUserResult.Succeeded}");
            Thread.Sleep(1000);
            // false
            var addToRoleResult = await _usersClient.AddToRole(new AddRemoveRoleRequest
            {
                UserName = userName,
                RoleName = roleName
            });

            Console.WriteLine($"AddToRole result: {addToRoleResult.Succeeded}");
            Thread.Sleep(1000);

            var removeFromRoleResult = await _usersClient.RemoveFromRole(new AddRemoveRoleRequest
            {
                UserName = userName,
                RoleName = roleName
            });

            Console.WriteLine($"RemoveFromRole result: {removeFromRoleResult.Succeeded}");
            Thread.Sleep(1000);

            var addToRolesResult = await _usersClient.AddToRoles(new AddRemoveRolesRequest
            {
                UserName = userName,
                RoleNames = roleNames
            });

            Console.WriteLine($"AddToRoles result: {addToRolesResult.Succeeded}");
            Thread.Sleep(1000);
            // false
            var removeFromRolesResult = await _usersClient.RemoveFromRoles(new AddRemoveRolesRequest
            {
                UserName = userName,
                RoleNames = roleNames
            });

            Console.WriteLine($"RemoveFromRoles result: {removeFromRolesResult.Succeeded}");
            Thread.Sleep(1000);

            var getUserFroRemoveResult = await _usersClient.Get(userName);

            Console.WriteLine($"Get result: {getUserFroRemoveResult.Code}");
            Thread.Sleep(1000);

            var removeResult = await _usersClient.Remove(getUserFroRemoveResult.Payload);

            Console.WriteLine($"Remove result: {removeResult.Succeeded}");
            Thread.Sleep(1000);

            return await Task.FromResult("Ok");
        }

        public async Task<string> RunRolesClientTests(string[] args)
        {
            Console.WriteLine("********** RunRolesClientTests **********");

            var token = await _identityServerClient.GetApiTokenAsync(_identityServerApiOptions);
            _rolesClient.HttpClient.SetBearerToken(token.AccessToken);

            var roleName = "roleFromTestApp";

            var addRoleResult = await _rolesClient.Add(new IdentityRole(roleName));

            Console.WriteLine($"Add result: {addRoleResult.Succeeded}");
            Thread.Sleep(1000);

            var getRoleResult = await _rolesClient.Get(roleName);

            Console.WriteLine($"Get result: {getRoleResult.Code}");
            Thread.Sleep(1000);

            var getAllRolesResult = await _rolesClient.GetAll();

            Console.WriteLine($"GetAll result: {getAllRolesResult.Code}");
            Thread.Sleep(1000);

            var role = getRoleResult.Payload;
            role.Name = "newRoleFromTestApp";
            var updateRoleResult = await _rolesClient.Update(roleName, role);

            Console.WriteLine($"Update result: {updateRoleResult.Succeeded}");
            Thread.Sleep(1000);

            var removeRoleResult = await _rolesClient.Remove(role);

            Console.WriteLine($"Remove result: {removeRoleResult.Succeeded}");
            Thread.Sleep(1000);

            return await Task.FromResult("Ok");
        }
    }
}
