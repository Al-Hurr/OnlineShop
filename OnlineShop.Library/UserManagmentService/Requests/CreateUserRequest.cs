
using OnlineShop.Library.UserManagmentService.Models;

namespace OnlineShop.Library.UserManagmentService.Requests
{
    public class CreateUserRequest
    {
        public ApplicationUser User { get; set; }
        public string Password { get; set; }
    }
}
