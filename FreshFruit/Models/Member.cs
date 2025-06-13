using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FreshFruit.Models;

public partial class Member
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("fullname")]
    [StringLength(50)]
    public string? Fullname { get; set; }

    [Column("phone")]
    [StringLength(15)]
    public string? Phone { get; set; }

    [Column("address")]
    [StringLength(150)]
    public string? Address { get; set; }

    [Column("email")]
    [StringLength(150)]
    public string? Email { get; set; }

    [Column("dob")]
    public DateOnly? Dob { get; set; }

    public int AccountId { get; set; }

    public int? Status { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("Members")]
    public virtual Account Account { get; set; } = null!;

    [InverseProperty("Member")]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    [InverseProperty("Member")]
    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}
