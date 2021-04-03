using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Database.Migrations
{
    public partial class StockOnHold : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderStocks_Orders_OrderId",
                table: "OrderStocks");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderStocks_Stock_StockId",
                table: "OrderStocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderStocks",
                table: "OrderStocks");

            migrationBuilder.RenameTable(
                name: "OrderStocks",
                newName: "OrderStock");

            migrationBuilder.RenameIndex(
                name: "IX_OrderStocks_OrderId",
                table: "OrderStock",
                newName: "IX_OrderStock_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderStock",
                table: "OrderStock",
                columns: new[] { "StockId", "OrderId" });

            migrationBuilder.CreateTable(
                name: "StocksOnHolds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockId = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StocksOnHolds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StocksOnHolds_Stock_StockId",
                        column: x => x.StockId,
                        principalTable: "Stock",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StocksOnHolds_StockId",
                table: "StocksOnHolds",
                column: "StockId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderStock_Orders_OrderId",
                table: "OrderStock",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderStock_Stock_StockId",
                table: "OrderStock",
                column: "StockId",
                principalTable: "Stock",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderStock_Orders_OrderId",
                table: "OrderStock");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderStock_Stock_StockId",
                table: "OrderStock");

            migrationBuilder.DropTable(
                name: "StocksOnHolds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderStock",
                table: "OrderStock");

            migrationBuilder.RenameTable(
                name: "OrderStock",
                newName: "OrderStocks");

            migrationBuilder.RenameIndex(
                name: "IX_OrderStock_OrderId",
                table: "OrderStocks",
                newName: "IX_OrderStocks_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderStocks",
                table: "OrderStocks",
                columns: new[] { "StockId", "OrderId" });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderStocks_Orders_OrderId",
                table: "OrderStocks",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderStocks_Stock_StockId",
                table: "OrderStocks",
                column: "StockId",
                principalTable: "Stock",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
