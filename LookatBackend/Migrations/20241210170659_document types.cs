using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LookatBackend.Migrations
{
    /// <inheritdoc />
    public partial class documenttypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BarangayId",
                table: "DocumentTypes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTypes_BarangayId",
                table: "DocumentTypes",
                column: "BarangayId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentTypes_Barangays_BarangayId",
                table: "DocumentTypes",
                column: "BarangayId",
                principalTable: "Barangays",
                principalColumn: "BarangayId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentTypes_Barangays_BarangayId",
                table: "DocumentTypes");

            migrationBuilder.DropIndex(
                name: "IX_DocumentTypes_BarangayId",
                table: "DocumentTypes");

            migrationBuilder.DropColumn(
                name: "BarangayId",
                table: "DocumentTypes");
        }
    }
}
