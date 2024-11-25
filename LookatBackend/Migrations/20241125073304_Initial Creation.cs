using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LookatBackend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Barangays",
                columns: table => new
                {
                    BarangayId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BarangayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Purok = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    BarangayLoc = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CityMunicipality = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Barangays", x => x.BarangayId);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    DocumentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentName = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.DocumentId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhysicalIdNumber = table.Column<string>(type: "nvarchar(25)", nullable: false),
                    Purok = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    BarangayLoc = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CityMunicipality = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    BarangayId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Barangays_BarangayId",
                        column: x => x.BarangayId,
                        principalTable: "Barangays",
                        principalColumn: "BarangayId");
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    RequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestType = table.Column<int>(type: "int", nullable: false),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_Requests_DocumentTypes_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "DocumentTypes",
                        principalColumn: "DocumentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requests_DocumentId",
                table: "Requests",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_BarangayId",
                table: "Users",
                column: "BarangayId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropTable(
                name: "Barangays");
        }
    }
}
