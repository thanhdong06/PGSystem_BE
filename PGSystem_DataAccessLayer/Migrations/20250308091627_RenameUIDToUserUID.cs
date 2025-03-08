using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PGSystem_DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class RenameUIDToUserUID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Users_UserUID",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_UserUID",
                table: "Members");

            migrationBuilder.AlterColumn<int>(
                name: "UserUID",
                table: "Members",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UID",
                keyValue: 1,
                columns: new[] { "CreateAt", "UpdateAt" },
                values: new object[] { new DateTime(2025, 3, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 3, 8, 16, 16, 26, 617, DateTimeKind.Local).AddTicks(3815) });

            migrationBuilder.CreateIndex(
                name: "IX_Members_UserUID",
                table: "Members",
                column: "UserUID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Users_UserUID",
                table: "Members",
                column: "UserUID",
                principalTable: "Users",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Users_UserUID",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_UserUID",
                table: "Members");

            migrationBuilder.AlterColumn<int>(
                name: "UserUID",
                table: "Members",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UID",
                keyValue: 1,
                columns: new[] { "CreateAt", "UpdateAt" },
                values: new object[] { new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 3, 5, 16, 31, 28, 292, DateTimeKind.Local).AddTicks(6420) });

            migrationBuilder.CreateIndex(
                name: "IX_Members_UserUID",
                table: "Members",
                column: "UserUID");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Users_UserUID",
                table: "Members",
                column: "UserUID",
                principalTable: "Users",
                principalColumn: "UID");
        }
    }
}
