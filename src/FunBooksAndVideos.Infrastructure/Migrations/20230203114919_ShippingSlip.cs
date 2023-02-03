using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FunBooksAndVideos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ShippingSlip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShippingSlips",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShipTo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingSlips", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShippingSlipItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderedQuantity = table.Column<int>(type: "int", nullable: false),
                    ShippedQuantity = table.Column<int>(type: "int", nullable: false),
                    ShippingSlipId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingSlipItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShippingSlipItem_ShippingSlips_ShippingSlipId",
                        column: x => x.ShippingSlipId,
                        principalTable: "ShippingSlips",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShippingSlipItem_ShippingSlipId",
                table: "ShippingSlipItem",
                column: "ShippingSlipId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShippingSlipItem");

            migrationBuilder.DropTable(
                name: "ShippingSlips");
        }
    }
}
