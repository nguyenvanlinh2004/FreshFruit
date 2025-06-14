﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FreshFruit.Migrations
{
    /// <inheritdoc />
    public partial class seeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategotyId",
                table: "Products",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategotyId",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.AlterColumn<double>(
                name: "Quantity",
                table: "Products",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "ID", "Email", "Password", "Role", "Status" },
                values: new object[,]
                {
                    { 1, "admin1@freshfruit.vn", "Admin@123", 1, 1 },
                    { 2, "admin2@freshfruit.vn", "User@123", 1, 1 },
                    { 3, "user2@freshfruit.vn", "User@123", 0, 1 },
                    { 4, "user3@freshfruit.vn", "User@123", 0, 1 },
                    { 5, "staff1@freshfruit.vn", "Staff@123", 0, 1 },
                    { 6, "staff2@freshfruit.vn", "Staff@123", 0, 1 },
                    { 7, "staff3@freshfruit.vn", "Staff@123", 0, 1 }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "Status" },
                values: new object[,]
                {
                    { 1, "Fruits", 1 },
                    { 2, "Vegetables", 1 },
                    { 3, "Dairy", 1 },
                    { 4, "Bakery", 1 },
                    { 5, "Meat", 1 },
                    { 6, "Beverages", 1 },
                    { 7, "Snacks", 1 }
                });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "AuthorId", "Contents", "CreatedAt", "Image", "Slug", "Status", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Trái cây giúp tăng cường hệ miễn dịch và cung cấp nhiều vitamin.", new DateOnly(2025, 1, 15), "blog1.jpg", "loi-ich-an-trai-cay", 1, "Lợi ích của việc ăn trái cây mỗi ngày" },
                    { 2, 2, "Hướng dẫn chọn trái cây tươi, không bị tẩm hóa chất.", new DateOnly(2025, 2, 5), "blog2.jpg", "cach-chon-trai-cay-sach", 1, "Cách chọn trái cây sạch và an toàn" },
                    { 3, 3, "Chuối, táo, dưa hấu... giúp no lâu và ít calo.", new DateOnly(2024, 3, 10), "blog3.jpg", "trai-cay-giam-can", 1, "Top 5 loại trái cây giúp giảm cân" },
                    { 4, 4, "Trái cây hữu cơ không dùng thuốc trừ sâu hóa học.", new DateOnly(2025, 4, 12), "blog4.jpg", "trai-cay-huu-co", 1, "Sự khác biệt giữa trái cây hữu cơ và thông thường" },
                    { 5, 5, "Nước ép giàu dưỡng chất nhưng nên uống đúng cách.", new DateOnly(2025, 5, 20), "blog5.jpg", "nuoc-ep-trai-cay", 1, "Nước ép trái cây: lợi ích và lưu ý khi sử dụng" },
                    { 6, 6, "Cam chứa nhiều vitamin C giúp đẹp da và tăng sức đề kháng.", new DateOnly(2025, 6, 1), "blog6.jpg", "vitamin-c-trong-cam", 1, "Tác dụng của vitamin C trong cam" },
                    { 7, 7, "Ăn trái cây vào buổi sáng giúp hấp thu tốt hơn.", new DateOnly(2025, 6, 10), "blog7.jpg", "an-trai-cay-dung-luc", 1, "Ăn trái cây đúng thời điểm để hấp thu tối đa" }
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "id", "AccountId", "address", "dob", "email", "fullname", "phone", "Status" },
                values: new object[,]
                {
                    { 1, 1, "123 Lê Lợi, Q1, TP.HCM", new DateOnly(1990, 1, 1), "a.nguyen@example.com", "Nguyễn Văn A", "0912345678", 1 },
                    { 2, 2, "45 Pasteur, Q3, TP.HCM", new DateOnly(1992, 5, 12), "b.tran@example.com", "Trần Thị B", "0934567890", 1 },
                    { 3, 3, "99 Trần Hưng Đạo, Q5, TP.HCM", new DateOnly(1985, 8, 23), "c.le@example.com", "Lê Văn C", "0901122334", 1 },
                    { 4, 4, "10 Nguyễn Du, Q1, TP.HCM", new DateOnly(1995, 12, 5), "d.pham@example.com", "Phạm Thị D", "0987654321", 1 },
                    { 5, 5, "75 Điện Biên Phủ, Q10, TP.HCM", new DateOnly(1993, 3, 15), "e.dang@example.com", "Đặng Văn E", "0911223344", 1 },
                    { 6, 6, "234 Lý Thường Kiệt, Q11, TP.HCM", new DateOnly(1988, 9, 9), "f.vo@example.com", "Võ Thị F", "0977554433", 1 },
                    { 7, 7, "88 Nguyễn Văn Cừ, Q5, TP.HCM", new DateOnly(1991, 7, 7), "g.hoang@example.com", "Hoàng Văn G", "0966888999", 1 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Image", "Name", "Price", "Quantity", "ShipmentId", "Slug", "Status" },
                values: new object[,]
                {
                    { 1, 1, "Táo nhập khẩu Mỹ, giòn ngọt", "tao.jpg", "Táo Mỹ", 45000.0, 100.0, "SHIP001", "tao-my", 1 },
                    { 2, 2, "Cam sành miền Tây, nhiều nước", "cam.jpg", "Cam Sành", 30000.0, 150.0, "SHIP002", "cam-sanh", 1 },
                    { 3, 3, "Chuối Laba Đà Lạt chín cây", "chuoi.jpg", "Chuối Laba", 20000.0, 120.0, "SHIP003", "chuoi-laba", 1 },
                    { 4, 4, "Nho tươi không hạt", "nho.jpg", "Nho Ninh Thuận", 60000.0, 80.0, "SHIP004", "nho-ninh-thuan", 1 },
                    { 5, 5, "Dưa hấu đỏ ruột, ngọt mát", "duahau.jpg", "Dưa Hấu", 15000.0, 200.0, "SHIP005", "dua-hau", 1 },
                    { 6, 6, "Ổi lê giòn, ít hạt", "oi.jpg", "Ổi Lê", 25000.0, 90.0, "SHIP006", "oi-le", 1 },
                    { 7, 7, "Mít Thái thơm, ngọt", "mit.jpg", "Mít Thái", 40000.0, 70.0, "SHIP007", "mit-thai", 1 }
                });

            migrationBuilder.InsertData(
                table: "PurchaseReceipts",
                columns: new[] { "Id", "CreateBy", "ReceiptDate", "ShipmentId", "Status", "Supplier", "Total" },
                values: new object[,]
                {
                    { 1, 1, new DateOnly(2024, 1, 5), "SHIP001", 1, "Công ty A", 1000000.0 },
                    { 2, 2, new DateOnly(2024, 1, 10), "SHIP002", 1, "Công ty B", 1500000.0 },
                    { 3, 3, new DateOnly(2024, 2, 1), "SHIP003", 1, "Công ty C", 850000.0 },
                    { 4, 4, new DateOnly(2024, 2, 15), "SHIP004", 1, "Công ty D", 1300000.0 },
                    { 5, 5, new DateOnly(2024, 3, 3), "SHIP005", 1, "Công ty E", 950000.0 },
                    { 6, 6, new DateOnly(2024, 3, 20), "SHIP006", 1, "Công ty F", 1100000.0 },
                    { 7, 7, new DateOnly(2024, 4, 2), "SHIP007", 1, "Công ty G", 1250000.0 }
                });

            migrationBuilder.InsertData(
                table: "BlogImages",
                columns: new[] { "Id", "BlogId", "ImageUrl", "Status" },
                values: new object[,]
                {
                    { 1, 1, "blogimage1.jpg", 1 },
                    { 2, 2, "blogimage2.jpg", 1 },
                    { 3, 3, "blogimage3.jpg", 1 },
                    { 4, 4, "blogimage4.jpg", 1 },
                    { 5, 5, "blogimage5.jpg", 1 },
                    { 6, 6, "blogimage6.jpg", 1 },
                    { 7, 7, "blogimage7.jpg", 1 }
                });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "Id", "InvoiceDate", "InvoicesCode", "MemberId", "PaymentMethod", "Status", "Total" },
                values: new object[,]
                {
                    { 1, new DateOnly(2025, 1, 10), "INV0001", 1, "Tiền mặt", 1, 250000m },
                    { 2, new DateOnly(2025, 1, 15), "INV0002", 2, "Chuyển khoản", 1, 350000m },
                    { 3, new DateOnly(2025, 2, 1), "INV0003", 3, "Chuyển khoản", 1, 180000m },
                    { 4, new DateOnly(2025, 2, 18), "INV0004", 4, "Tiền mặt", 1, 410000m },
                    { 5, new DateOnly(2025, 3, 2), "INV0005", 5, "Chuyển khoản", 1, 295000m },
                    { 6, new DateOnly(2025, 3, 20), "INV0006", 6, "Chuyển khoản", 1, 150000m },
                    { 7, new DateOnly(2025, 4, 5), "INV0007", 7, "Tiền mặt", 1, 320000m }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "ImageUrl", "ProductId", "Status" },
                values: new object[,]
                {
                    { 1, "tao-1.jpg", 1, 1 },
                    { 2, "cam-1.jpg", 2, 1 },
                    { 3, "chuoi-1.jpg", 3, 1 },
                    { 4, "nho-1.jpg", 4, 1 },
                    { 5, "duahau-1.jpg", 5, 1 },
                    { 6, "oi-1.jpg", 6, 1 },
                    { 7, "mit-1.jpg", 7, 1 }
                });

            migrationBuilder.InsertData(
                table: "PurchaseReceiptDetails",
                columns: new[] { "Id", "ExpiryDate", "ImportPrice", "ProductId", "Quantity", "ReceiptId", "Status", "Total" },
                values: new object[,]
                {
                    { 1, new DateOnly(2024, 12, 1), 20000m, 1, 50, 1, 1, 1000000m },
                    { 2, new DateOnly(2024, 11, 20), 25000m, 2, 60, 2, 1, 1500000m },
                    { 3, new DateOnly(2024, 11, 10), 21250m, 3, 40, 3, 1, 850000m },
                    { 4, new DateOnly(2024, 12, 5), 20000m, 4, 65, 4, 1, 1300000m },
                    { 5, new DateOnly(2024, 10, 25), 13571m, 5, 70, 5, 1, 950000m },
                    { 6, new DateOnly(2024, 12, 15), 20000m, 6, 55, 6, 1, 1100000m },
                    { 7, new DateOnly(2024, 12, 31), 25000m, 7, 50, 7, 1, 1250000m }
                });

            migrationBuilder.InsertData(
                table: "Rating",
                columns: new[] { "Id", "CreatedAt", "MemberId", "ProductId", "Rating", "Status" },
                values: new object[,]
                {
                    { 1, new DateOnly(2024, 6, 1), 1, 1, 5, 1 },
                    { 2, new DateOnly(2024, 6, 2), 2, 2, 4, 1 },
                    { 3, new DateOnly(2024, 6, 3), 3, 3, 3, 1 },
                    { 4, new DateOnly(2024, 6, 4), 4, 4, 5, 1 },
                    { 5, new DateOnly(2024, 6, 5), 5, 5, 4, 1 },
                    { 6, new DateOnly(2024, 6, 6), 6, 6, 2, 1 },
                    { 7, new DateOnly(2024, 6, 7), 7, 7, 5, 1 }
                });

            migrationBuilder.InsertData(
                table: "Comment",
                columns: new[] { "Id", "CommentText", "CreatedAt", "RatingId", "Status" },
                values: new object[,]
                {
                    { 1, "Sản phẩm rất tốt, sẽ ủng hộ tiếp!", new DateOnly(2025, 1, 10), 1, 1 },
                    { 2, "Chất lượng ổn, giá hợp lý.", new DateOnly(2025, 1, 15), 2, 1 },
                    { 3, "Không tươi như mong đợi.", new DateOnly(2025, 2, 1), 3, 1 },
                    { 4, "Dịch vụ giao hàng nhanh chóng.", new DateOnly(2025, 2, 5), 4, 1 },
                    { 5, "Sẽ giới thiệu cho bạn bè.", new DateOnly(2025, 3, 1), 5, 1 },
                    { 6, "Trái cây ngon và ngọt.", new DateOnly(2025, 3, 12), 6, 1 },
                    { 7, "Không hài lòng vì đóng gói sơ sài.", new DateOnly(2025, 3, 20), 7, 0 }
                });

            migrationBuilder.InsertData(
                table: "InvoiceDetails",
                columns: new[] { "ID", "InvoiceId", "ProductId", "Quantity", "Status", "Total", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, 1, 2.000m, 1, 100000m, 50000m },
                    { 2, 2, 2, 1.000m, 1, 35000m, 35000m },
                    { 3, 3, 3, 3.500m, 1, 140000m, 40000m },
                    { 4, 4, 4, 1.200m, 1, 72000m, 60000m },
                    { 5, 5, 5, 0.800m, 1, 36000m, 45000m },
                    { 6, 6, 6, 2.000m, 1, 110000m, 55000m },
                    { 7, 7, 7, 1.500m, 1, 72000m, 48000m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BlogImages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BlogImages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BlogImages",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BlogImages",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BlogImages",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "BlogImages",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "BlogImages",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "InvoiceDetails",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "InvoiceDetails",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "InvoiceDetails",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "InvoiceDetails",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "InvoiceDetails",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "InvoiceDetails",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "InvoiceDetails",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "PurchaseReceiptDetails",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PurchaseReceiptDetails",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PurchaseReceiptDetails",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PurchaseReceiptDetails",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PurchaseReceiptDetails",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PurchaseReceiptDetails",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "PurchaseReceiptDetails",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "PurchaseReceipts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PurchaseReceipts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PurchaseReceipts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PurchaseReceipts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PurchaseReceipts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PurchaseReceipts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "PurchaseReceipts",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Rating",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rating",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rating",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rating",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Rating",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Rating",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Rating",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Products",
                newName: "CategotyId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                newName: "IX_Products_CategotyId");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);
        }
    }
}
