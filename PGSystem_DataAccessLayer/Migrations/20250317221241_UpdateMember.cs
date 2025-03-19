using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PGSystem_DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderCode",
                table: "Members",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UID",
                keyValue: 1,
                column: "UpdateAt",
                value: new DateTime(2025, 3, 18, 5, 12, 40, 961, DateTimeKind.Local).AddTicks(7635));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderCode",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Members");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UID",
                keyValue: 1,
                column: "UpdateAt",
                value: new DateTime(2025, 3, 18, 3, 52, 53, 571, DateTimeKind.Local).AddTicks(9509));
        }
    }
}
