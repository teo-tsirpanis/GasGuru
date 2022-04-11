using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GasGuru.Database.Migrations
{
    public partial class options : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GasStationOptions",
                schema: "GasGuru",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MonthlyRent = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GasStationOptions", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "GasGuru",
                table: "GasStationOptions",
                columns: new[] { "Id", "MonthlyRent" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), 5000m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GasStationOptions",
                schema: "GasGuru");
        }
    }
}
