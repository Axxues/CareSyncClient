using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareSync.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditForeignKeyInventoryItemDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItemDetails_ConsultationPrescriptions_ConsultationPrescriptionId",
                table: "InventoryItemDetails");

            migrationBuilder.DropIndex(
                name: "IX_InventoryStockDetails_InventoryItemDetailId",
                table: "InventoryStockDetails");

            migrationBuilder.DropIndex(
                name: "IX_InventoryItemDetails_ConsultationPrescriptionId",
                table: "InventoryItemDetails");

            migrationBuilder.DropColumn(
                name: "ConsultationPrescriptionId",
                table: "InventoryItemDetails");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryStockDetails_InventoryItemDetailId",
                table: "InventoryStockDetails",
                column: "InventoryItemDetailId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InventoryStockDetails_InventoryItemDetailId",
                table: "InventoryStockDetails");

            migrationBuilder.AddColumn<int>(
                name: "ConsultationPrescriptionId",
                table: "InventoryItemDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryStockDetails_InventoryItemDetailId",
                table: "InventoryStockDetails",
                column: "InventoryItemDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItemDetails_ConsultationPrescriptionId",
                table: "InventoryItemDetails",
                column: "ConsultationPrescriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItemDetails_ConsultationPrescriptions_ConsultationPrescriptionId",
                table: "InventoryItemDetails",
                column: "ConsultationPrescriptionId",
                principalTable: "ConsultationPrescriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
