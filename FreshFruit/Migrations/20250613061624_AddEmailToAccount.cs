using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreshFruit.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailToAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slug = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Contents = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    AuthorId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blogs_Accounts_01",
                        column: x => x.AuthorId,
                        principalTable: "Accounts",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fullname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    phone = table.Column<string>(type: "nchar(15)", fixedLength: true, maxLength: 15, nullable: true),
                    address = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    email = table.Column<string>(type: "nchar(150)", fixedLength: true, maxLength: 150, nullable: true),
                    dob = table.Column<DateOnly>(type: "date", nullable: true),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_member", x => x.id);
                    table.ForeignKey(
                        name: "FK_Members_Accounts_01",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseReceipts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ShipmentId = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ReceiptDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Total = table.Column<double>(type: "float", nullable: true),
                    Supplier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsReceipt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseReceipts_Accounts_01",
                        column: x => x.CreateBy,
                        principalTable: "Accounts",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategotyId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    ShipmentId = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Slug = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_01",
                        column: x => x.CategotyId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BlogImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogImages_Blogs",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoicesCode = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    InvoiceDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Members_01",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseReceiptDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceiptId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    ImportPrice = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    ExpiryDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseReceiptDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseReceiptDetails_Products_01",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseReceiptDetails_Receipts_01",
                        column: x => x.ReceiptId,
                        principalTable: "PurchaseReceipts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rating_Members",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Rating_Products",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InvoiceDetails",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InvoiceDetails_Invoices_01",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InvoiceDetails_Products_01",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RatingId = table.Column<int>(type: "int", nullable: false),
                    CommentText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Rating",
                        column: x => x.RatingId,
                        principalTable: "Rating",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogImages_BlogId",
                table: "BlogImages",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_AuthorId",
                table: "Blogs",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_RatingId",
                table: "Comment",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_InvoiceId",
                table: "InvoiceDetails",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_ProductId",
                table: "InvoiceDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_MemberId",
                table: "Invoices",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_AccountId",
                table: "Members",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategotyId",
                table: "Products",
                column: "CategotyId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceiptDetails_ProductId",
                table: "PurchaseReceiptDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceiptDetails_ReceiptId",
                table: "PurchaseReceiptDetails",
                column: "ReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceipts_CreateBy",
                table: "PurchaseReceipts",
                column: "CreateBy");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_MemberId",
                table: "Rating",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_ProductId",
                table: "Rating",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogImages");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "InvoiceDetails");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "PurchaseReceiptDetails");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "PurchaseReceipts");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
