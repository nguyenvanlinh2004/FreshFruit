using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreshFruit.Migrations
{
    /// <inheritdoc />
    public partial class updateRatingAndComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Rating",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "CommentText",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "Rating",
                newName: "RatingValue");

            migrationBuilder.RenameColumn(
                name: "RatingId",
                table: "Comment",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_RatingId",
                table: "Comment",
                newName: "IX_Comment_ProductId");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Rating",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "Rating",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Rating",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Comment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Comment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Contents",
                table: "Comment",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "Comment",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Contents", "CreatedAt", "MemberId" },
                values: new object[] { "Sản phẩm rất tốt, sẽ ủng hộ tiếp!", new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Contents", "CreatedAt", "MemberId" },
                values: new object[] { "Chất lượng ổn, giá hợp lý.", new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 });

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Contents", "CreatedAt", "MemberId" },
                values: new object[] { "Không tươi như mong đợi.", new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 });

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Contents", "CreatedAt", "MemberId" },
                values: new object[] { "Dịch vụ giao hàng nhanh chóng.", new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 });

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Contents", "CreatedAt", "MemberId" },
                values: new object[] { "Sẽ giới thiệu cho bạn bè.", new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 });

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Contents", "CreatedAt", "MemberId" },
                values: new object[] { "Trái cây ngon và ngọt.", new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 });

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Contents", "CreatedAt", "MemberId" },
                values: new object[] { "Không hài lòng vì đóng gói sơ sài.", new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 7 });

            migrationBuilder.UpdateData(
                table: "Rating",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Rating",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Rating",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Rating",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Rating",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2024, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Rating",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2024, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Rating",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2024, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Comment_MemberId",
                table: "Comment",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Members",
                table: "Comment",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Products",
                table: "Comment",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Members",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Products",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_MemberId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "Contents",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "RatingValue",
                table: "Rating",
                newName: "Rating");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Comment",
                newName: "RatingId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_ProductId",
                table: "Comment",
                newName: "IX_Comment_RatingId");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Rating",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "Rating",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreatedAt",
                table: "Rating",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Comment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreatedAt",
                table: "Comment",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "CommentText",
                table: "Comment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CommentText", "CreatedAt" },
                values: new object[] { "Sản phẩm rất tốt, sẽ ủng hộ tiếp!", new DateOnly(2025, 1, 10) });

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CommentText", "CreatedAt" },
                values: new object[] { "Chất lượng ổn, giá hợp lý.", new DateOnly(2025, 1, 15) });

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CommentText", "CreatedAt" },
                values: new object[] { "Không tươi như mong đợi.", new DateOnly(2025, 2, 1) });

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CommentText", "CreatedAt" },
                values: new object[] { "Dịch vụ giao hàng nhanh chóng.", new DateOnly(2025, 2, 5) });

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CommentText", "CreatedAt" },
                values: new object[] { "Sẽ giới thiệu cho bạn bè.", new DateOnly(2025, 3, 1) });

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CommentText", "CreatedAt" },
                values: new object[] { "Trái cây ngon và ngọt.", new DateOnly(2025, 3, 12) });

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CommentText", "CreatedAt" },
                values: new object[] { "Không hài lòng vì đóng gói sơ sài.", new DateOnly(2025, 3, 20) });

            migrationBuilder.UpdateData(
                table: "Rating",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateOnly(2024, 6, 1));

            migrationBuilder.UpdateData(
                table: "Rating",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateOnly(2024, 6, 2));

            migrationBuilder.UpdateData(
                table: "Rating",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateOnly(2024, 6, 3));

            migrationBuilder.UpdateData(
                table: "Rating",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateOnly(2024, 6, 4));

            migrationBuilder.UpdateData(
                table: "Rating",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateOnly(2024, 6, 5));

            migrationBuilder.UpdateData(
                table: "Rating",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateOnly(2024, 6, 6));

            migrationBuilder.UpdateData(
                table: "Rating",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateOnly(2024, 6, 7));

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Rating",
                table: "Comment",
                column: "RatingId",
                principalTable: "Rating",
                principalColumn: "Id");
        }
    }
}
