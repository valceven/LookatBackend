using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LookatBackend.Migrations
{
    /// <inheritdoc />
    public partial class updatedrequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Barangays_BarangayId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_DocumentTypes_DocumentId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_BarangayId",
                table: "Requests");

            migrationBuilder.AlterColumn<string>(
                name: "BarangayId",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_DocumentTypes_DocumentId",
                table: "Requests",
                column: "DocumentId",
                principalTable: "DocumentTypes",
                principalColumn: "DocumentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_DocumentTypes_DocumentId",
                table: "Requests");

            migrationBuilder.AlterColumn<string>(
                name: "BarangayId",
                table: "Requests",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_BarangayId",
                table: "Requests",
                column: "BarangayId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Barangays_BarangayId",
                table: "Requests",
                column: "BarangayId",
                principalTable: "Barangays",
                principalColumn: "BarangayId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_DocumentTypes_DocumentId",
                table: "Requests",
                column: "DocumentId",
                principalTable: "DocumentTypes",
                principalColumn: "DocumentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
