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

    public int RatingId { get; set; }

    public string? CommentText { get; set; }

    public DateOnly? CreatedAt { get; set; }

    public int? Status { get; set; }

    [ForeignKey("RatingId")]
    [InverseProperty("Comments")]
    public virtual Rating Rating { get; set; } = null!;
}
