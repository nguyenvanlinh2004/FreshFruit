using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreshFruit.Migrations
{
    /// <inheritdoc />
    public partial class eidtinvoiceproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LongDescription",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "InvoiceDate",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fullname",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "Email", "Fullname", "InvoiceDate", "Phone" },
                values: new object[] { "123 Đường A", "vana@example.com", "Nguyễn Văn A", new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "0912345678" });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Address", "Email", "Fullname", "InvoiceDate", "Phone" },
                values: new object[] { "456 Đường B", "tranb@example.com", "Trần Thị B", new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "0934567890" });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Address", "Email", "Fullname", "InvoiceDate", "Phone" },
                values: new object[] { "789 Đường C", "levc@example.com", "Lê Văn C", new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0987654321" });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Address", "Email", "Fullname", "InvoiceDate", "Phone" },
                values: new object[] { "321 Đường D", "phamd@example.com", "Phạm Văn D", new DateTime(2025, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "0901122334" });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Address", "Email", "Fullname", "InvoiceDate", "Phone" },
                values: new object[] { "654 Đường E", "dange@example.com", "Đặng Thị E", new DateTime(2025, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "0933221144" });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Address", "Email", "Fullname", "InvoiceDate", "Phone" },
                values: new object[] { "987 Đường F", "hoangf@example.com", "Hoàng Văn F", new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "0977766554" });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Address", "Email", "Fullname", "InvoiceDate", "Phone" },
                values: new object[] { "111 Đường G", "lyg@example.com", "Lý Thị G", new DateTime(2025, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "0909090909" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "LongDescription",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "LongDescription",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "LongDescription",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "LongDescription",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "LongDescription",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "LongDescription",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                column: "LongDescription",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
