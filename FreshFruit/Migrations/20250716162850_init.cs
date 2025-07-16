using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FreshFruit.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			// 🔁 Đổi CategoryId trước để tránh lỗi FK
			migrationBuilder.UpdateData(
				table: "Products",
				keyColumn: "Id",
				keyValue: 4,
				column: "CategoryId",
				value: 1);

			migrationBuilder.UpdateData(
				table: "Products",
				keyColumn: "Id",
				keyValue: 5,
				column: "CategoryId",
				value: 2);

			migrationBuilder.UpdateData(
				table: "Products",
				keyColumn: "Id",
				keyValue: 6,
				column: "CategoryId",
				value: 3);

			migrationBuilder.UpdateData(
				table: "Products",
				keyColumn: "Id",
				keyValue: 7,
				column: "CategoryId",
				value: 1);

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

            migrationBuilder.InsertData(
                table: "Vouchers",
                columns: new[] { "Id", "Code", "DiscountAmount", "EndDate", "Name", "StartDate", "Status" },
                values: new object[,]
                {
                    { 1, "DISCOUNT10", 10000m, new DateTime(2025, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Giảm giá 10k", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, "DISCOUNT20", 50000m, new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Giảm giá 20k", new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, "DISCOUNT50", 200000m, new DateTime(2025, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Giảm giá 50k", new DateTime(2025, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "LongDescription",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Fullname",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Invoices");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "InvoiceDate",
                table: "Invoices",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Fruits");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Vegetables");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Dairy");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "Status" },
                values: new object[,]
                {
                    { 4, "Bakery", 1 },
                    { 5, "Meat", 1 },
                    { 6, "Beverages", 1 },
                    { 7, "Snacks", 1 }
                });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                column: "InvoiceDate",
                value: new DateOnly(2025, 1, 10));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                column: "InvoiceDate",
                value: new DateOnly(2025, 1, 15));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 3,
                column: "InvoiceDate",
                value: new DateOnly(2025, 2, 1));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 4,
                column: "InvoiceDate",
                value: new DateOnly(2025, 2, 18));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 5,
                column: "InvoiceDate",
                value: new DateOnly(2025, 3, 2));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 6,
                column: "InvoiceDate",
                value: new DateOnly(2025, 3, 20));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 7,
                column: "InvoiceDate",
                value: new DateOnly(2025, 4, 5));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Quantity",
                value: 100.0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "Quantity",
                value: 150.0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "Quantity",
                value: 120.0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CategoryId", "Quantity" },
                values: new object[] { 4, 80.0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CategoryId", "Quantity" },
                values: new object[] { 5, 200.0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CategoryId", "Quantity" },
                values: new object[] { 6, 90.0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CategoryId", "Quantity" },
                values: new object[] { 7, 70.0 });
        }
    }
}
