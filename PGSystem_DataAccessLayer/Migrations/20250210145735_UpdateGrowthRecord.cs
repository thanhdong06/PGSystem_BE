using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PGSystem_DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGrowthRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GrowthRecords_PregnancyRecords_PID",
                table: "GrowthRecords");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UID",
                keyValue: 1,
                columns: new[] { "CreateAt", "UpdateAt" },
                values: new object[] { new DateTime(2025, 2, 10, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 10, 21, 57, 35, 259, DateTimeKind.Local).AddTicks(2109) });

            migrationBuilder.AddForeignKey(
                name: "FK_GrowthRecords_PregnancyRecords_PID",
                table: "GrowthRecords",
                column: "PID",
                principalTable: "PregnancyRecords",
                principalColumn: "PID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GrowthRecords_PregnancyRecords_PID",
                table: "GrowthRecords");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UID",
                keyValue: 1,
                columns: new[] { "CreateAt", "UpdateAt" },
                values: new object[] { new DateTime(2025, 2, 7, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 2, 7, 22, 39, 30, 247, DateTimeKind.Local).AddTicks(8659) });

            migrationBuilder.AddForeignKey(
                name: "FK_GrowthRecords_PregnancyRecords_PID",
                table: "GrowthRecords",
                column: "PID",
                principalTable: "PregnancyRecords",
                principalColumn: "PID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
