using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FreshFruit.Models;

[Table("Rating")]
public partial class Rating
{
    [Key]
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int MemberId { get; set; }

    [Column("Rating")]
    public int Rating1 { get; set; }

    public DateOnly? CreatedAt { get; set; }

    public int? Status { get; set; }

    [InverseProperty("Rating")]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [ForeignKey("MemberId")]
    [InverseProperty("Ratings")]
    public virtual Member Member { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("Ratings")]
    public virtual Product Product { get; set; } = null!;
}
