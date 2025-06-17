using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FreshFruit.Models;

[Table("Comment")]
public partial class Comment
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ProductId { get; set; }

    public int? MemberId { get; set; }

    [Required]
    [StringLength(1000)]
    public string Contents { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int Status { get; set; }

    // Navigation
    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("MemberId")]
    public virtual Member? Member { get; set; }

}
