using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PGSystem_DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AdminData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UID", "CreateAt", "Email", "Password", "Phone", "Role", "UpdateAt" },
                values: new object[] { 1, new DateTime(2025, 1, 17, 0, 0, 0, 0, DateTimeKind.Local), "admin@gmail.com", "12345", "0123456789", "Admin", new DateTime(2025, 1, 17, 2, 52, 22, 654, DateTimeKind.Local).AddTicks(9955) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UID",
                keyValue: 1);
        }
    }
}
