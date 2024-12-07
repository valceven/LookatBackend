using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LookatBackend.Migrations
{
    /// <inheritdoc />
    public partial class addedbarangaypassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Barangays",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Barangays");
        }
    }
}
