using Microsoft.AspNetCore.Identity;
using OnlineShop.Library.Common.Models;

namespace OnlineShop.Library.UserManagmentService.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public Address? DefaultAddress { get; set; }

        public Address? DeliveryAddress{ get; set; }
    }
}
