using Microsoft.EntityFrameworkCore.Migrations;

namespace Komunalka.DAL.Migrations
{
    public partial class Simplify2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceType",
                table: "ServiceProvider",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceType",
                table: "ServiceProvider");
        }
    }
}
