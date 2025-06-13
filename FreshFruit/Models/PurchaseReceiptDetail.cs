using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FreshFruit.Models;

public partial class PurchaseReceiptDetail
{
    [Key]
    public int Id { get; set; }

    public int ReceiptId { get; set; }

    public int ProductId { get; set; }

    public int? Quantity { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? ImportPrice { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? Total { get; set; }

    public int? Status { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("PurchaseReceiptDetails")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("ReceiptId")]
    [InverseProperty("PurchaseReceiptDetails")]
    public virtual PurchaseReceipt Receipt { get; set; } = null!;
}
