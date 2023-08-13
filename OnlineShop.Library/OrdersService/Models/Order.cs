using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OnlineShop.Library.Common.Interfaces;
using OnlineShop.Library.ArticlesService.Models;

namespace OnlineShop.Library.OrdersService.Models
{
    public class Order : IIdentifiable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "uniqueidentifier")]
        public Guid Id { get; set; }

        [Required]
        [Column(TypeName = "uniqueidentifier")]
        public Guid AddressId { get; set; }

        [Required]
        [Column(TypeName = "uniqueidentifier")]
        public Guid UserId { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime Modified { get; set; }

        public List<OrderedArticle> Articles { get; set; }
    }
}
