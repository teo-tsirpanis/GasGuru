using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GasGuru.Database.Migrations
{
    public partial class soft_delete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "GasGuru",
                table: "Items",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "GasGuru",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "GasGuru",
                table: "Customers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "GasGuru",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "GasGuru",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "GasGuru",
                table: "Customers");
        }
    }
}
