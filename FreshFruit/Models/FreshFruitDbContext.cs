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
    public virtual DbSet<Voucher> Vouchers { get; set; }
    public virtual DbSet<WishList> WishLists { get; set; }

	public virtual DbSet<CompanyInfo> CompanyInfos { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      => optionsBuilder.UseSqlServer("Name=FreshFruitConnection");
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasData(
				  new Account { Id = 1, Email = "admin1@freshfruit.vn", Password = "Admin@123", Role = 1, Status = 1 },
	              new Account { Id = 2, Email = "admin2@freshfruit.vn", Password = "User@123", Role = 1, Status = 1 },
	              new Account { Id = 3, Email = "user2@freshfruit.vn", Password = "User@123", Role = 0, Status = 1 },
	              new Account { Id = 4, Email = "user3@freshfruit.vn", Password = "User@123", Role = 0, Status = 1 },
	              new Account { Id = 5, Email = "staff1@freshfruit.vn", Password = "Staff@123", Role = 0, Status = 1 },
	              new Account { Id = 6, Email = "staff2@freshfruit.vn", Password = "Staff@123", Role = 0, Status = 1 },
	              new Account { Id = 7, Email = "staff3@freshfruit.vn", Password = "Staff@123", Role = 0, Status = 1 }
				);
			entity.HasKey(e => e.Id).HasName("PK_account");
        });

        modelBuilder.Entity<Blog>(entity =>
        {
			modelBuilder.Entity<Category>(entity =>
			{
				entity.HasData(
					new Category { Id = 1, Name = "Trái cây nhiệt đới", Status = 1 },
					new Category { Id = 2, Name = "Trái cây ôn đới", Status = 1 },
					new Category { Id = 3, Name = "Trái cây đặc", Status = 1 }
				);
			});

			entity.HasData(
                 new Blog { Id = 1, Title = "Lợi ích của việc ăn trái cây mỗi ngày", Slug = "loi-ich-an-trai-cay", Contents = "Trái cây giúp tăng cường hệ miễn dịch và cung cấp nhiều vitamin.", Image = "blog1.jpg", AuthorId = 1, CreatedAt = new DateOnly(2025, 1, 15), Status = 1 },
                 new Blog { Id = 2, Title = "Cách chọn trái cây sạch và an toàn", Slug = "cach-chon-trai-cay-sach", Contents = "Hướng dẫn chọn trái cây tươi, không bị tẩm hóa chất.", Image = "blog2.jpg", AuthorId = 2, CreatedAt = new DateOnly(2025, 2, 5), Status = 1 },
                 new Blog { Id = 3, Title = "Top 5 loại trái cây giúp giảm cân", Slug = "trai-cay-giam-can", Contents = "Chuối, táo, dưa hấu... giúp no lâu và ít calo.", Image = "blog3.jpg", AuthorId = 3, CreatedAt = new DateOnly(2024, 3, 10), Status = 1 },
                 new Blog { Id = 4, Title = "Sự khác biệt giữa trái cây hữu cơ và thông thường", Slug = "trai-cay-huu-co", Contents = "Trái cây hữu cơ không dùng thuốc trừ sâu hóa học.", Image = "blog4.jpg", AuthorId = 4, CreatedAt = new DateOnly(2025, 4, 12), Status = 1 },
                 new Blog { Id = 5, Title = "Nước ép trái cây: lợi ích và lưu ý khi sử dụng", Slug = "nuoc-ep-trai-cay", Contents = "Nước ép giàu dưỡng chất nhưng nên uống đúng cách.", Image = "blog5.jpg", AuthorId = 5, CreatedAt = new DateOnly(2025, 5, 20), Status = 1 },
                 new Blog { Id = 6, Title = "Tác dụng của vitamin C trong cam", Slug = "vitamin-c-trong-cam", Contents = "Cam chứa nhiều vitamin C giúp đẹp da và tăng sức đề kháng.", Image = "blog6.jpg", AuthorId = 6, CreatedAt = new DateOnly(2025, 6, 1), Status = 1 },
                 new Blog { Id = 7, Title = "Ăn trái cây đúng thời điểm để hấp thu tối đa", Slug = "an-trai-cay-dung-luc", Contents = "Ăn trái cây vào buổi sáng giúp hấp thu tốt hơn.", Image = "blog7.jpg", AuthorId = 7, CreatedAt = new DateOnly(2025, 6, 10), Status = 1 }
                );
			entity.Property(e => e.Id).ValueGeneratedOnAdd();


			entity.HasOne(d => d.Author).WithMany(p => p.Blogs).HasConstraintName("FK_Blogs_Accounts_01");
        });

        modelBuilder.Entity<BlogImage>(entity =>
        {
            entity.HasData(
			    new BlogImage { Id = 1, BlogId = 1, ImageUrl = "blogimage1.jpg", Status = 1 },
	            new BlogImage { Id = 2, BlogId = 2, ImageUrl = "blogimage2.jpg", Status = 1 },
	            new BlogImage { Id = 3, BlogId = 3, ImageUrl = "blogimage3.jpg", Status = 1 },
	            new BlogImage { Id = 4, BlogId = 4, ImageUrl = "blogimage4.jpg", Status = 1 },
	            new BlogImage { Id = 5, BlogId = 5, ImageUrl = "blogimage5.jpg", Status = 1 },
	            new BlogImage { Id = 6, BlogId = 6, ImageUrl = "blogimage6.jpg", Status = 1 },
	            new BlogImage { Id = 7, BlogId = 7, ImageUrl = "blogimage7.jpg", Status = 1 }
			);
            entity.HasOne(d => d.Blog).WithMany(p => p.BlogImages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BlogImages_Blogs");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasData(
                new Comment { Id = 1, ProductId = 1, MemberId = 1, Contents = "Sản phẩm rất tốt, sẽ ủng hộ tiếp!", CreatedAt = new DateTime(2025, 1, 10), Status = 1 },
                new Comment { Id = 2, ProductId = 2, MemberId = 2, Contents = "Chất lượng ổn, giá hợp lý.", CreatedAt = new DateTime(2025, 1, 15), Status = 1 },
                new Comment { Id = 3, ProductId = 3, MemberId = 3, Contents = "Không tươi như mong đợi.", CreatedAt = new DateTime(2025, 2, 1), Status = 1 },
                new Comment { Id = 4, ProductId = 4, MemberId = 4, Contents = "Dịch vụ giao hàng nhanh chóng.", CreatedAt = new DateTime(2025, 2, 5), Status = 1 },
                new Comment { Id = 5, ProductId = 5, MemberId = 5, Contents = "Sẽ giới thiệu cho bạn bè.", CreatedAt = new DateTime(2025, 3, 1), Status = 1 },
                new Comment { Id = 6, ProductId = 6, MemberId = 6, Contents = "Trái cây ngon và ngọt.", CreatedAt = new DateTime(2025, 3, 12), Status = 1 },
                new Comment { Id = 7, ProductId = 7, MemberId = 7, Contents = "Không hài lòng vì đóng gói sơ sài.", CreatedAt = new DateTime(2025, 3, 20), Status = 0 }
            );

            entity.HasOne(d => d.Member).WithMany(p => p.Comments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comment_Members");

            entity.HasOne(d => d.Product).WithMany(p => p.Comments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comment_Products");
        });


        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasData(
                new Invoice { Id = 1, InvoicesCode = "INV0001", MemberId = 1, InvoiceDate = new DateTime(2025, 1, 15), Fullname = "Nguyễn Văn A", Email = "vana@example.com", Phone = "0912345678", Address = "123 Đường A", Total = 250000, Status = 1, PaymentMethod = "Tiền mặt" },
                new Invoice { Id = 3, InvoicesCode = "INV0003", MemberId = 3, InvoiceDate = new DateTime(2025, 2, 1), Fullname = "Lê Văn C", Email = "levc@example.com", Phone = "0987654321", Address = "789 Đường C", Total = 180000, Status = 1, PaymentMethod = "Chuyển khoản" },
                new Invoice { Id = 4, InvoicesCode = "INV0004", MemberId = 4, InvoiceDate = new DateTime(2025, 2, 18), Fullname = "Phạm Văn D", Email = "phamd@example.com", Phone = "0901122334", Address = "321 Đường D", Total = 410000, Status = 1, PaymentMethod = "Tiền mặt" },
                new Invoice { Id = 2, InvoicesCode = "INV0002", MemberId = 2, InvoiceDate = new DateTime(2025, 1, 20), Fullname = "Trần Thị B", Email = "tranb@example.com", Phone = "0934567890", Address = "456 Đường B", Total = 350000, Status = 1, PaymentMethod = "Chuyển khoản" },
                new Invoice { Id = 5, InvoicesCode = "INV0005", MemberId = 5, InvoiceDate = new DateTime(2025, 3, 2), Fullname = "Đặng Thị E", Email = "dange@example.com", Phone = "0933221144", Address = "654 Đường E", Total = 295000, Status = 1, PaymentMethod = "Chuyển khoản" },
                new Invoice { Id = 6, InvoicesCode = "INV0006", MemberId = 6, InvoiceDate = new DateTime(2025, 3, 20), Fullname = "Hoàng Văn F", Email = "hoangf@example.com", Phone = "0977766554", Address = "987 Đường F", Total = 150000, Status = 1, PaymentMethod = "Chuyển khoản" },
                new Invoice { Id = 7, InvoicesCode = "INV0007", MemberId = 7, InvoiceDate = new DateTime(2025, 4, 5), Fullname = "Lý Thị G", Email = "lyg@example.com", Phone = "0909090909", Address = "111 Đường G", Total = 320000, Status = 1, PaymentMethod = "Tiền mặt" }
                );
            entity.HasOne(d => d.Member).WithMany(p => p.Invoices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoices_Members_01");
        });

        modelBuilder.Entity<InvoiceDetail>(entity =>
        {
            entity.HasData(
				new InvoiceDetail { Id = 1, InvoiceId = 1, ProductId = 1, Quantity = 2.000m, UnitPrice = 50000, Total = 100000, Status = 1 },
	            new InvoiceDetail { Id = 2, InvoiceId = 2, ProductId = 2, Quantity = 1.000m, UnitPrice = 35000, Total = 35000, Status = 1 },
	            new InvoiceDetail { Id = 3, InvoiceId = 3, ProductId = 3, Quantity = 3.500m, UnitPrice = 40000, Total = 140000, Status = 1 },
	            new InvoiceDetail { Id = 4, InvoiceId = 4, ProductId = 4, Quantity = 1.200m, UnitPrice = 60000, Total = 72000, Status = 1 },
	            new InvoiceDetail { Id = 5, InvoiceId = 5, ProductId = 5, Quantity = 0.800m, UnitPrice = 45000, Total = 36000, Status = 1 },
	            new InvoiceDetail { Id = 6, InvoiceId = 6, ProductId = 6, Quantity = 2.000m, UnitPrice = 55000, Total = 110000, Status = 1 },
	            new InvoiceDetail { Id = 7, InvoiceId = 7, ProductId = 7, Quantity = 1.500m, UnitPrice = 48000, Total = 72000, Status = 1 }
				);
            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceDetails_Invoices_01");

            entity.HasOne(d => d.Product).WithMany(p => p.InvoiceDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceDetails_Products_01");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasData(
				new Member { Id = 1, Fullname = "Nguyễn Văn A", Phone = "0912345678", Address = "123 Lê Lợi, Q1, TP.HCM", Email = "a.nguyen@example.com", Dob = new DateOnly(1990, 1, 1), AccountId = 1, Status = 1 },
	            new Member { Id = 2, Fullname = "Trần Thị B", Phone = "0934567890", Address = "45 Pasteur, Q3, TP.HCM", Email = "b.tran@example.com", Dob = new DateOnly(1992, 5, 12), AccountId = 2, Status = 1 },
	            new Member { Id = 3, Fullname = "Lê Văn C", Phone = "0901122334", Address = "99 Trần Hưng Đạo, Q5, TP.HCM", Email = "c.le@example.com", Dob = new DateOnly(1985, 8, 23), AccountId = 3, Status = 1 },
	            new Member { Id = 4, Fullname = "Phạm Thị D", Phone = "0987654321", Address = "10 Nguyễn Du, Q1, TP.HCM", Email = "d.pham@example.com", Dob = new DateOnly(1995, 12, 5), AccountId = 4, Status = 1 },
	            new Member { Id = 5, Fullname = "Đặng Văn E", Phone = "0911223344", Address = "75 Điện Biên Phủ, Q10, TP.HCM", Email = "e.dang@example.com", Dob = new DateOnly(1993, 3, 15), AccountId = 5, Status = 1 },
	            new Member { Id = 6, Fullname = "Võ Thị F", Phone = "0977554433", Address = "234 Lý Thường Kiệt, Q11, TP.HCM", Email = "f.vo@example.com", Dob = new DateOnly(1988, 9, 9), AccountId = 6, Status = 1 },
	            new Member { Id = 7, Fullname = "Hoàng Văn G", Phone = "0966888999", Address = "88 Nguyễn Văn Cừ, Q5, TP.HCM", Email = "g.hoang@example.com", Dob = new DateOnly(1991, 7, 7), AccountId = 7, Status = 1 }
				);
            entity.HasKey(e => e.Id).HasName("PK_member");

            entity.Property(e => e.Email).IsFixedLength();
            entity.Property(e => e.Phone).IsFixedLength();

            entity.HasOne(d => d.Account).WithMany(p => p.Members)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Members_Accounts_01");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasData(
				 new Product { Id = 1, Name = "Táo Mỹ", CategoryId = 1, Price = 45000, Description = "Táo nhập khẩu Mỹ, giòn ngọt", Image = "tao.jpg", Status = 1, Quantity = 0, ShipmentId = "SHIP001", Slug = "tao-my" },
	new Product { Id = 2, Name = "Cam Sành", CategoryId = 2, Price = 30000, Description = "Cam sành miền Tây, nhiều nước", Image = "cam.jpg", Status = 1, Quantity = 0, ShipmentId = "SHIP002", Slug = "cam-sanh" },
	new Product { Id = 3, Name = "Chuối Laba", CategoryId = 3, Price = 20000, Description = "Chuối Laba Đà Lạt chín cây", Image = "chuoi.jpg", Status = 1, Quantity = 0, ShipmentId = "SHIP003", Slug = "chuoi-laba" },
	new Product { Id = 4, Name = "Nho Ninh Thuận", CategoryId = 1, Price = 60000, Description = "Nho tươi không hạt", Image = "nho.jpg", Status = 1, Quantity = 0, ShipmentId = "SHIP004", Slug = "nho-ninh-thuan" },
	new Product { Id = 5, Name = "Dưa Hấu", CategoryId = 2, Price = 15000, Description = "Dưa hấu đỏ ruột, ngọt mát", Image = "duahau.jpg", Status = 1, Quantity = 0, ShipmentId = "SHIP005", Slug = "dua-hau" },
	new Product { Id = 6, Name = "Ổi Lê", CategoryId = 3, Price = 25000, Description = "Ổi lê giòn, ít hạt", Image = "oi.jpg", Status = 1, Quantity = 0, ShipmentId = "SHIP006", Slug = "oi-le" },
	new Product { Id = 7, Name = "Mít Thái", CategoryId = 1, Price = 40000, Description = "Mít Thái thơm, ngọt", Image = "mit.jpg", Status = 1, Quantity = 0, ShipmentId = "SHIP007", Slug = "mit-thai" }
				);
			entity.HasKey(e => e.Id).HasName("PK_product");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Categories_01");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasData(
				new ProductImage { Id = 1, ProductId = 1, ImageUrl = "tao-1.jpg", Status = 1 },
	            new ProductImage { Id = 2, ProductId = 2, ImageUrl = "cam-1.jpg", Status = 1 },
	            new ProductImage { Id = 3, ProductId = 3, ImageUrl = "chuoi-1.jpg", Status = 1 },
	            new ProductImage { Id = 4, ProductId = 4, ImageUrl = "nho-1.jpg", Status = 1 },
	            new ProductImage { Id = 5, ProductId = 5, ImageUrl = "duahau-1.jpg", Status = 1 },
	            new ProductImage { Id = 6, ProductId = 6, ImageUrl = "oi-1.jpg", Status = 1 },
	            new ProductImage { Id = 7, ProductId = 7, ImageUrl = "mit-1.jpg", Status = 1 }
				);
			entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductImages_Products");
        });

        modelBuilder.Entity<PurchaseReceipt>(entity =>
        {
            entity.HasData(
				new PurchaseReceipt { Id = 1, ShipmentId = "SHIP001", ReceiptDate = new DateOnly(2024, 1, 5), Total = 1000000, Supplier = "Công ty A", CreateBy = 1, Status = 1 },
	            new PurchaseReceipt { Id = 2, ShipmentId = "SHIP002", ReceiptDate = new DateOnly(2024, 1, 10), Total = 1500000, Supplier = "Công ty B", CreateBy = 2, Status = 1 },
	            new PurchaseReceipt { Id = 3, ShipmentId = "SHIP003", ReceiptDate = new DateOnly(2024, 2, 1), Total = 850000, Supplier = "Công ty C", CreateBy = 3, Status = 1 },
	            new PurchaseReceipt { Id = 4, ShipmentId = "SHIP004", ReceiptDate = new DateOnly(2024, 2, 15), Total = 1300000, Supplier = "Công ty D", CreateBy = 4, Status = 1 },
	            new PurchaseReceipt { Id = 5, ShipmentId = "SHIP005", ReceiptDate = new DateOnly(2024, 3, 3), Total = 950000, Supplier = "Công ty E", CreateBy = 5, Status = 1 },
	            new PurchaseReceipt { Id = 6, ShipmentId = "SHIP006", ReceiptDate = new DateOnly(2024, 3, 20), Total = 1100000, Supplier = "Công ty F", CreateBy = 6, Status = 1 },
	            new PurchaseReceipt { Id = 7, ShipmentId = "SHIP007", ReceiptDate = new DateOnly(2024, 4, 2), Total = 1250000, Supplier = "Công ty G", CreateBy = 7, Status = 1 }
				);
			entity.HasKey(e => e.Id).HasName("PK_GoodsReceipt");


            entity.HasOne(d => d.CreateByNavigation).WithMany(p => p.PurchaseReceipts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseReceipts_Accounts_01");
        });

        modelBuilder.Entity<PurchaseReceiptDetail>(entity =>
        {
            entity.HasData(
				 new PurchaseReceiptDetail { Id = 1, ReceiptId = 1, ProductId = 1, Quantity = 50, ImportPrice = 20000, ExpiryDate = new DateOnly(2024, 12, 1), Total = 1000000, Status = 1 },
	             new PurchaseReceiptDetail { Id = 2, ReceiptId = 2, ProductId = 2, Quantity = 60, ImportPrice = 25000, ExpiryDate = new DateOnly(2024, 11, 20), Total = 1500000, Status = 1 },
	             new PurchaseReceiptDetail { Id = 3, ReceiptId = 3, ProductId = 3, Quantity = 40, ImportPrice = 21250, ExpiryDate = new DateOnly(2024, 11, 10), Total = 850000, Status = 1 },
	             new PurchaseReceiptDetail { Id = 4, ReceiptId = 4, ProductId = 4, Quantity = 65, ImportPrice = 20000, ExpiryDate = new DateOnly(2024, 12, 5), Total = 1300000, Status = 1 },
	             new PurchaseReceiptDetail { Id = 5, ReceiptId = 5, ProductId = 5, Quantity = 70, ImportPrice = 13571, ExpiryDate = new DateOnly(2024, 10, 25), Total = 950000, Status = 1 },
               	 new PurchaseReceiptDetail { Id = 6, ReceiptId = 6, ProductId = 6, Quantity = 55, ImportPrice = 20000, ExpiryDate = new DateOnly(2024, 12, 15), Total = 1100000, Status = 1 },
             	 new PurchaseReceiptDetail { Id = 7, ReceiptId = 7, ProductId = 7, Quantity = 50, ImportPrice = 25000, ExpiryDate = new DateOnly(2024, 12, 31), Total = 1250000, Status = 1 }
				);
			entity.HasOne(d => d.Product).WithMany(p => p.PurchaseReceiptDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseReceiptDetails_Products_01");

            entity.HasOne(d => d.Receipt).WithMany(p => p.PurchaseReceiptDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PurchaseReceiptDetails_Receipts_01");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasData(
                new Rating { Id = 1, ProductId = 1, MemberId = 1, RatingValue = 5, CreatedAt = new DateTime(2024, 6, 1), Status = 1 },
                new Rating { Id = 2, ProductId = 2, MemberId = 2, RatingValue = 4, CreatedAt = new DateTime(2024, 6, 2), Status = 1 },
                new Rating { Id = 3, ProductId = 3, MemberId = 3, RatingValue = 3, CreatedAt = new DateTime(2024, 6, 3), Status = 1 },
                new Rating { Id = 4, ProductId = 4, MemberId = 4, RatingValue = 5, CreatedAt = new DateTime(2024, 6, 4), Status = 1 },
                new Rating { Id = 5, ProductId = 5, MemberId = 5, RatingValue = 4, CreatedAt = new DateTime(2024, 6, 5), Status = 1 },
                new Rating { Id = 6, ProductId = 6, MemberId = 6, RatingValue = 2, CreatedAt = new DateTime(2024, 6, 6), Status = 1 },
                new Rating { Id = 7, ProductId = 7, MemberId = 7, RatingValue = 5, CreatedAt = new DateTime(2024, 6, 7), Status = 1 }
            );

            entity.HasOne(d => d.Member).WithMany(p => p.Ratings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rating_Members");

            entity.HasOne(d => d.Product).WithMany(p => p.Ratings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rating_Products");
        });

		modelBuilder.Entity<CompanyInfo>(entity =>
		{
			entity.HasData(
				new CompanyInfo
				{
					Id = 1,
					CompanyName = "Công ty TNHH Trái Cây Tươi",
					Address = "123 Đường Hoa Quả, Quận 1, TP.HCM",
					Phone = "0123456789",
					Email = "info@traicaytuoi.vn",
					Website = "https://traicaytuoi.vn",
					LogoUrl = "/images/company-logo.png"
				}
			);
		});
		OnModelCreatingPartial(modelBuilder);

		modelBuilder.Entity<Voucher>().HasData(
	   new Voucher
	   {
		   Id = 1,
		   Name = "Giảm giá 10k",
		   Code = "DISCOUNT10",
		   DiscountAmount = 10000,
		   StartDate = new DateTime(2025, 7, 1),
		   EndDate = new DateTime(2025, 8, 31),
		   Status = 1
	   },
	   new Voucher
	   {
		   Id = 2,
		   Name = "Giảm giá 20k",
		   Code = "DISCOUNT20",
		   DiscountAmount = 50000,
		   StartDate = new DateTime(2025, 7, 10),
		   EndDate = new DateTime(2025, 12, 31),
		   Status = 1
	   },
	   new Voucher
	   {
		   Id = 3,
		   Name = "Giảm giá 50k",
		   Code = "DISCOUNT50",
		   DiscountAmount = 200000,
		   StartDate = new DateTime(2025, 11, 25),
		   EndDate = new DateTime(2025, 11, 30),
		   Status = 1
	   }
	 );
	}

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
