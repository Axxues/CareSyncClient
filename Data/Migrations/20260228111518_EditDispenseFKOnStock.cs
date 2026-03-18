using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareSync.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditDispenseFKOnStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryDispenseDetails_InventoryStockDetails_InventoryItemDetailId",
                table: "InventoryDispenseDetails");

            migrationBuilder.DropIndex(
                name: "IX_InventoryDispenseDetails_InventoryItemDetailId",
                table: "InventoryDispenseDetails");

            migrationBuilder.DropColumn(
                name: "InventoryItemDetailId",
                table: "InventoryDispenseDetails");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryDispenseDetails_InventoryStockDetailId",
                table: "InventoryDispenseDetails",
                column: "InventoryStockDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryDispenseDetails_InventoryStockDetails_InventoryStockDetailId",
                table: "InventoryDispenseDetails",
                column: "InventoryStockDetailId",
                principalTable: "InventoryStockDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryDispenseDetails_InventoryStockDetails_InventoryStockDetailId",
                table: "InventoryDispenseDetails");

            migrationBuilder.DropIndex(
                name: "IX_InventoryDispenseDetails_InventoryStockDetailId",
                table: "InventoryDispenseDetails");

            migrationBuilder.AddColumn<int>(
                name: "InventoryItemDetailId",
                table: "InventoryDispenseDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryDispenseDetails_InventoryItemDetailId",
                table: "InventoryDispenseDetails",
                column: "InventoryItemDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryDispenseDetails_InventoryStockDetails_InventoryItemDetailId",
                table: "InventoryDispenseDetails",
                column: "InventoryItemDetailId",
                principalTable: "InventoryStockDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
