using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PGSystem_DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UID",
                keyValue: 1,
                columns: new[] { "CreateAt", "UpdateAt" },
                values: new object[] { new DateTime(2025, 2, 7, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 7, 22, 39, 30, 247, DateTimeKind.Local).AddTicks(8659) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UID",
                keyValue: 1,
                columns: new[] { "CreateAt", "UpdateAt" },
                values: new object[] { new DateTime(2025, 1, 17, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 1, 17, 2, 52, 22, 654, DateTimeKind.Local).AddTicks(9955) });
        }
    }
}
