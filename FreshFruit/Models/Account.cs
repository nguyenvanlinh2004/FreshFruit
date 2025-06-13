using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FreshFruit.Models;

public partial class Account
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Email là bắt buộc.")]
    [StringLength(50, ErrorMessage = "Email không được vượt quá 50 ký tự.")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
    [StringLength(255, ErrorMessage = "Mật khẩu không được vượt quá 255 ký tự.")]
    public string Password { get; set; } = null!;
    
    public int Role { get; set; }

    public int? Status { get; set; }

    [InverseProperty("Author")]
    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();

    [InverseProperty("Account")]
    public virtual ICollection<Member> Members { get; set; } = new List<Member>();

    [InverseProperty("CreateByNavigation")]
    public virtual ICollection<PurchaseReceipt> PurchaseReceipts { get; set; } = new List<PurchaseReceipt>();
}
