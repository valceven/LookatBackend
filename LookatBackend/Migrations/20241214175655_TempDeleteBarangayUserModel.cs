using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LookatBackend.Migrations
{
    /// <inheritdoc />
    public partial class TempDeleteBarangayUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Barangays_BarangayId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_BarangayId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BarangayId",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BarangayId",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_BarangayId",
                table: "Users",
                column: "BarangayId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Barangays_BarangayId",
                table: "Users",
                column: "BarangayId",
                principalTable: "Barangays",
                principalColumn: "BarangayId");
        }
    }
}
