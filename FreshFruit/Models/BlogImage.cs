using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FreshFruit.Models;

public partial class BlogImage
{
    [Key]
    public int Id { get; set; }

    public int BlogId { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string ImageUrl { get; set; } = null!;

    public int? Status { get; set; }

    [ForeignKey("BlogId")]
    [InverseProperty("BlogImages")]
    public virtual Blog Blog { get; set; } = null!;
}
