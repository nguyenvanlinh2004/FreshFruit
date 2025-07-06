﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FreshFruit.Models;

public partial class Product
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }

    public int CategoryId { get; set; }

    public double? Price { get; set; }

    public string? Description { get; set; }
    public string? LongDescription { get; set; }

    [Unicode(false)]
    public string? Image { get; set; }

    public int? Status { get; set; }

	public double? Quantity { get; set; }

	[StringLength(50)]
    [Unicode(false)]
    public string? ShipmentId { get; set; }

    [Unicode(false)]
    public string? Slug { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Products")]
    public virtual Category Category { get; set; } = null!;

    [InverseProperty("Product")]
    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    [InverseProperty("Product")]
    public virtual ICollection<PurchaseReceiptDetail> PurchaseReceiptDetails { get; set; } = new List<PurchaseReceiptDetail>();

    [InverseProperty("Product")]
    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    [InverseProperty("Product")]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public virtual ICollection<WishList> WishLists { get; set; } = new List<WishList>();
}
