using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FreshFruit.Models;

public partial class InvoiceDetail
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    public int InvoiceId { get; set; }

    public int ProductId { get; set; }

    [Column(TypeName = "decimal(18, 3)")]
    public decimal Quantity { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal UnitPrice { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal Total { get; set; }

    public int? Status { get; set; }

    [ForeignKey("InvoiceId")]
    [InverseProperty("InvoiceDetails")]
    public virtual Invoice Invoice { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("InvoiceDetails")]
    public virtual Product Product { get; set; } = null!;
}
