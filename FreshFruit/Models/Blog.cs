using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FreshFruit.Models;

public partial class Blog
{
    [Key]
    public int Id { get; set; }

    public string? Title { get; set; }

    [Unicode(false)]
    public string? Slug { get; set; }

    public string? Contents { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Image { get; set; }

    public int? AuthorId { get; set; }

    public DateOnly? CreatedAt { get; set; }

    public int? Status { get; set; }

    [ForeignKey("AuthorId")]
    [InverseProperty("Blogs")]
    public virtual Account? Author { get; set; }

    [InverseProperty("Blog")]
    public virtual ICollection<BlogImage> BlogImages { get; set; } = new List<BlogImage>();
}
