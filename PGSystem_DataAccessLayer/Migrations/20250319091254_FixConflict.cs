using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PGSystem_DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class FixConflict : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UID",
                keyValue: 1,
                columns: new[] { "CreateAt", "UpdateAt" },
                values: new object[] { new DateTime(2025, 3, 19, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 3, 19, 16, 12, 53, 807, DateTimeKind.Local).AddTicks(9107) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UID",
                keyValue: 1,
                columns: new[] { "CreateAt", "UpdateAt" },
                values: new object[] { new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 3, 18, 5, 12, 40, 961, DateTimeKind.Local).AddTicks(7635) });
        }
    }
}
