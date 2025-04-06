using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PGSystem_DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class updateFetus_FetusMeasurement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fetuses",
                columns: table => new
                {
                    FetusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nickname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PregnancyRecordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fetuses", x => x.FetusId);
                    table.ForeignKey(
                        name: "FK_Fetuses_PregnancyRecords_PregnancyRecordId",
                        column: x => x.PregnancyRecordId,
                        principalTable: "PregnancyRecords",
                        principalColumn: "PID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FetusMeasurements",
                columns: table => new
                {
                    MeasurementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateMeasured = table.Column<DateOnly>(type: "date", nullable: false),
                    Length = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    HeadCircumference = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    WeightEstimate = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FetusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FetusMeasurements", x => x.MeasurementId);
                    table.ForeignKey(
                        name: "FK_FetusMeasurements_Fetuses_FetusId",
                        column: x => x.FetusId,
                        principalTable: "Fetuses",
                        principalColumn: "FetusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UID",
                keyValue: 1,
                column: "UpdateAt",
                value: new DateTime(2025, 4, 6, 12, 7, 16, 735, DateTimeKind.Local).AddTicks(6957));

            migrationBuilder.CreateIndex(
                name: "IX_Fetuses_PregnancyRecordId",
                table: "Fetuses",
                column: "PregnancyRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_FetusMeasurements_FetusId",
                table: "FetusMeasurements",
                column: "FetusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FetusMeasurements");

            migrationBuilder.DropTable(
                name: "Fetuses");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UID",
                keyValue: 1,
                column: "UpdateAt",
                value: new DateTime(2025, 4, 6, 11, 58, 50, 395, DateTimeKind.Local).AddTicks(9546));
        }
    }
}
