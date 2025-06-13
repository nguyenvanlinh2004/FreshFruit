using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FreshFruit.Models;

public partial class Invoice
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string InvoicesCode { get; set; } = null!;

    public int MemberId { get; set; }

    public DateOnly InvoiceDate { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal Total { get; set; }

    public int Status { get; set; }

    [StringLength(50)]
    public string PaymentMethod { get; set; } = null!;

    [InverseProperty("Invoice")]
    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    [ForeignKey("MemberId")]
    [InverseProperty("Invoices")]
    public virtual Member Member { get; set; } = null!;
}
