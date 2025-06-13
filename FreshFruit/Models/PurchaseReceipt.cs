using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FreshFruit.Models;

public partial class PurchaseReceipt
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ShipmentId { get; set; }

    public DateOnly? ReceiptDate { get; set; }

    public double? Total { get; set; }

    public string? Supplier { get; set; }

    public int CreateBy { get; set; }

    public int? Status { get; set; }

    [ForeignKey("CreateBy")]
    [InverseProperty("PurchaseReceipts")]
    public virtual Account CreateByNavigation { get; set; } = null!;

    [InverseProperty("Receipt")]
    public virtual ICollection<PurchaseReceiptDetail> PurchaseReceiptDetails { get; set; } = new List<PurchaseReceiptDetail>();
}
