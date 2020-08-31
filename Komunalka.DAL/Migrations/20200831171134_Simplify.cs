using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Komunalka.DAL.Migrations
{
    public partial class Simplify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceProvider",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceProvider", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Payment__Custome__37A5467C",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PayingComponent",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentId = table.Column<int>(nullable: false),
                    ServiceProviderId = table.Column<int>(nullable: false),
                    Account = table.Column<string>(maxLength: 50, nullable: false),
                    CounterIndicationsCurrent = table.Column<int>(nullable: false),
                    CurrentIndicationsPrevious = table.Column<int>(nullable: false),
                    CounterIndicationsDifference = table.Column<int>(nullable: false),
                    RateCommon = table.Column<double>(nullable: false),
                    RateDiscount = table.Column<double>(nullable: true),
                    DiscountIndicationsAmount = table.Column<int>(nullable: true),
                    Summa = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayingComponent", x => x.Id);
                    table.ForeignKey(
                        name: "FK__PayingCom__Payme__5CD6CB2B",
                        column: x => x.PaymentId,
                        principalTable: "Payment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__PayingCom__Servi__5DCAEF64",
                        column: x => x.ServiceProviderId,
                        principalTable: "ServiceProvider",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PayingComponent_PaymentId",
                table: "PayingComponent",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_PayingComponent_ServiceProviderId",
                table: "PayingComponent",
                column: "ServiceProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_CustomerId",
                table: "Payment",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PayingComponent");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "ServiceProvider");

            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
