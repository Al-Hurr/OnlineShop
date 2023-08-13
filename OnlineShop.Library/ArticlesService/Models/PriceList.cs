﻿using OnlineShop.Library.Common.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Library.ArticlesService.Models
{
    public class PriceList : IIdentifiable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "uniqueidentifier")]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Article))]
        public Guid ArticleId { get; set; }

        [Column(TypeName = "numeric(12,4)")]
        public decimal Price { get; set; }

        [Required]
        public string Name { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime ValidFrom{ get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime ValidTo{ get; set; }
    }
}
