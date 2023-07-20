using Microsoft.AspNetCore.Identity;
using OnlineShop.Library.Common.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Library.UserManagmentService.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Column(TypeName = "nvarchar(256)")]
        public string? FirstName { get; set; }

        [Column(TypeName = "nvarchar(256)")]
        public string? LastName { get; set; }

        public Address? DefaultAddress { get; set; }

        public Address? DeliveryAddress{ get; set; }
    }
}
