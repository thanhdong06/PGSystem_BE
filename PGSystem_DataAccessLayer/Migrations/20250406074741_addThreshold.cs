using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PGSystem_DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class addThreshold : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Thresholds",
                columns: table => new
                {
                    ThresholdsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeasurementType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MinValue = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    MaxValue = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    WarningMessage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thresholds", x => x.ThresholdsId);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UID",
                keyValue: 1,
                column: "UpdateAt",
                value: new DateTime(2025, 4, 6, 14, 47, 40, 663, DateTimeKind.Local).AddTicks(6963));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Thresholds");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UID",
                keyValue: 1,
                column: "UpdateAt",
                value: new DateTime(2025, 4, 6, 13, 12, 53, 62, DateTimeKind.Local).AddTicks(3658));
        }
    }
}
