using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PGSystem_DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMembershipPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UID",
                keyValue: 1,
                columns: new[] { "CreateAt", "UpdateAt" },
                values: new object[] { new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 3, 18, 3, 52, 53, 571, DateTimeKind.Local).AddTicks(9509) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UID",
                keyValue: 1,
                columns: new[] { "CreateAt", "UpdateAt" },
                values: new object[] { new DateTime(2025, 3, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 3, 8, 16, 16, 26, 617, DateTimeKind.Local).AddTicks(3815) });
        }
    }
}
