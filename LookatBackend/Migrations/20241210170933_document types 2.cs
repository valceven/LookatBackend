using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LookatBackend.Migrations
{
    /// <inheritdoc />
    public partial class documenttypes2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentTypes_Barangays_BarangayId",
                table: "DocumentTypes");

            migrationBuilder.AlterColumn<string>(
                name: "BarangayId",
                table: "DocumentTypes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentTypes_Barangays_BarangayId",
                table: "DocumentTypes",
                column: "BarangayId",
                principalTable: "Barangays",
                principalColumn: "BarangayId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentTypes_Barangays_BarangayId",
                table: "DocumentTypes");

            migrationBuilder.AlterColumn<string>(
                name: "BarangayId",
                table: "DocumentTypes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentTypes_Barangays_BarangayId",
                table: "DocumentTypes",
                column: "BarangayId",
                principalTable: "Barangays",
                principalColumn: "BarangayId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
