using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareSync.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditDispenseFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryDispenseDetails_InventoryItemDetails_InventoryItemDetailId",
                table: "InventoryDispenseDetails");

            migrationBuilder.AddColumn<int>(
                name: "InventoryStockDetailId",
                table: "InventoryDispenseDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryDispenseDetails_InventoryStockDetails_InventoryItemDetailId",
                table: "InventoryDispenseDetails",
                column: "InventoryItemDetailId",
                principalTable: "InventoryStockDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryDispenseDetails_InventoryStockDetails_InventoryItemDetailId",
                table: "InventoryDispenseDetails");

            migrationBuilder.DropColumn(
                name: "InventoryStockDetailId",
                table: "InventoryDispenseDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryDispenseDetails_InventoryItemDetails_InventoryItemDetailId",
                table: "InventoryDispenseDetails",
                column: "InventoryItemDetailId",
                principalTable: "InventoryItemDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
