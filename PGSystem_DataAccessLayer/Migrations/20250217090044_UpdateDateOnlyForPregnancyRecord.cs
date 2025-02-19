using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PGSystem_DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDateOnlyForPregnancyRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UID",
                keyValue: 1,
                columns: new[] { "CreateAt", "UpdateAt" },
                values: new object[] { new DateTime(2025, 2, 17, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 17, 16, 0, 43, 664, DateTimeKind.Local).AddTicks(4831) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UID",
                keyValue: 1,
                columns: new[] { "CreateAt", "UpdateAt" },
                values: new object[] { new DateTime(2025, 2, 10, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 10, 21, 57, 35, 259, DateTimeKind.Local).AddTicks(2109) });
        }
    }
}
