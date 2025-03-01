using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PGSystem_DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBlogAndComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Reminders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UID",
                keyValue: 1,
                columns: new[] { "CreateAt", "UpdateAt" },
                values: new object[] { new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 3, 1, 15, 9, 8, 39, DateTimeKind.Local).AddTicks(2582) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Reminders");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UID",
                keyValue: 1,
                columns: new[] { "CreateAt", "UpdateAt" },
                values: new object[] { new DateTime(2025, 2, 19, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 19, 16, 3, 13, 93, DateTimeKind.Local).AddTicks(8166) });
        }
    }
}
