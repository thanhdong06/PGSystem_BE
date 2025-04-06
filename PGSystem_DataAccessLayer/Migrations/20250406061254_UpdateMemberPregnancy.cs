using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PGSystem_DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMemberPregnancy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PregnancyRecords_MemberMemberID",
                table: "PregnancyRecords");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UID",
                keyValue: 1,
                column: "UpdateAt",
                value: new DateTime(2025, 4, 6, 13, 12, 53, 62, DateTimeKind.Local).AddTicks(3658));

            migrationBuilder.CreateIndex(
                name: "IX_PregnancyRecords_MemberMemberID",
                table: "PregnancyRecords",
                column: "MemberMemberID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PregnancyRecords_MemberMemberID",
                table: "PregnancyRecords");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UID",
                keyValue: 1,
                column: "UpdateAt",
                value: new DateTime(2025, 4, 6, 12, 7, 16, 735, DateTimeKind.Local).AddTicks(6957));

            migrationBuilder.CreateIndex(
                name: "IX_PregnancyRecords_MemberMemberID",
                table: "PregnancyRecords",
                column: "MemberMemberID",
                unique: true);
        }
    }
}
