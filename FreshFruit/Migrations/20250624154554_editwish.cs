using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreshFruit.Migrations
{
    /// <inheritdoc />
    public partial class editwish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_Accounts_AccountId",
                table: "WishLists");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "WishLists");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "WishLists",
                newName: "AccountID");

            migrationBuilder.RenameIndex(
                name: "IX_WishLists_AccountId",
                table: "WishLists",
                newName: "IX_WishLists_AccountID");

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_Accounts_AccountID",
                table: "WishLists",
                column: "AccountID",
                principalTable: "Accounts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_Accounts_AccountID",
                table: "WishLists");

            migrationBuilder.RenameColumn(
                name: "AccountID",
                table: "WishLists",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_WishLists_AccountID",
                table: "WishLists",
                newName: "IX_WishLists_AccountId");

            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "WishLists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_Accounts_AccountId",
                table: "WishLists",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
