using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FreshFruit.Models;

public partial class Invoice
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string InvoicesCode { get; set; } = null!;

    public int MemberId { get; set; }
    public DateTime InvoiceDate { get; set; }= DateTime.Now;
    public string Fullname { get; set; } = null!;

    [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Số điện thoại không được bỏ trống.")]
    public string Phone { get; set; } = null!;

    [Required(ErrorMessage = "Địa chỉ không được bỏ trống.")]
    public string Address { get; set; } = null!;
    [Column(TypeName = "decimal(18, 0)")]
    public decimal Total { get; set; }

    public int Status { get; set; }

    [StringLength(50)]
    public string PaymentMethod { get; set; } = null!;

    [InverseProperty("Invoice")]
    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
    [ValidateNever]
    public virtual Member Member { get; set; } = null!;
}
