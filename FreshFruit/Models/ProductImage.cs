using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FreshFruit.Models;

public partial class ProductImage
{
    [Key]
    public int Id { get; set; }

    public int ProductId { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string ImageUrl { get; set; } = null!;

    public int? Status { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("ProductImages")]
    public virtual Product Product { get; set; } = null!;
}
