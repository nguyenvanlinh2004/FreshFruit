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

    [Required]
    public int ProductId { get; set; }

    public int? MemberId { get; set; }

    [Range(1, 5)]
    public int RatingValue { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int Status { get; set; }

    // Navigation
    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("MemberId")]
    public virtual Member? Member { get; set; }
}
