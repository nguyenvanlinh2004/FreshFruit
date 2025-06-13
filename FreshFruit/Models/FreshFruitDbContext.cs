using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FreshFruit.Models;

public partial class FreshFruitDbContext : DbContext
{
    public FreshFruitDbContext()
    {
    }

    public FreshFruitDbContext(DbContextOptions<FreshFruitDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<BlogImage> BlogImages { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<PurchaseReceipt> PurchaseReceipts { get; set; }

    public virtual DbSet<PurchaseReceiptDetail> PurchaseReceiptDetails { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      => optionsBuilder.UseSqlServer("Name=FreshFruitConnection");
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_account");
        });

        modelBuilder.Entity<Blog>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Author).WithMany(p => p.Blogs).HasConstraintName("FK_Blogs_Accounts_01");
        });

        modelBuilder.Entity<BlogImage>(entity =>
        {
            entity.HasOne(d => d.Blog).WithMany(p => p.BlogImages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BlogImages_Blogs");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasOne(d => d.Rating).WithMany(p => p.Comments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comment_Rating");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasOne(d => d.Member).WithMany(p => p.Invoices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoices_Members_01");
        });

        modelBuilder.Entity<InvoiceDetail>(entity =>
        {
            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceDetails_Invoices_01");

            entity.HasOne(d => d.Product).WithMany(p => p.InvoiceDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceDetails_Products_01");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_member");

            entity.Property(e => e.Email).IsFixedLength();
            entity.Property(e => e.Phone).IsFixedLength();

            entity.HasOne(d => d.Account).WithMany(p => p.Members)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Members_Accounts_01");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_product");

            entity.HasOne(d => d.Categoty).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Categories_01");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductImages_Products");
        });

        modelBuilder.Entity<PurchaseReceipt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_GoodsReceipt");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.CreateByNavigation).WithMany(p => p.PurchaseReceipts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseReceipts_Accounts_01");
        });

        modelBuilder.Entity<PurchaseReceiptDetail>(entity =>
        {
            entity.HasOne(d => d.Product).WithMany(p => p.PurchaseReceiptDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseReceiptDetails_Products_01");

            entity.HasOne(d => d.Receipt).WithMany(p => p.PurchaseReceiptDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseReceiptDetails_Receipts_01");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasOne(d => d.Member).WithMany(p => p.Ratings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rating_Members");

            entity.HasOne(d => d.Product).WithMany(p => p.Ratings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rating_Products");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
