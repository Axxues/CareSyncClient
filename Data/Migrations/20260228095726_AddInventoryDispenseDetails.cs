using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareSync.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddInventoryDispenseDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InventoryDispenseDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientPersonalInformationId = table.Column<int>(type: "int", nullable: false),
                    InventoryItemDetailId = table.Column<int>(type: "int", nullable: false),
                    InitialQuQuantQuantityityantity = table.Column<int>(type: "int", nullable: false),
                    DateDispensed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Instructions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryDispenseDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryDispenseDetails_InventoryItemDetails_InventoryItemDetailId",
                        column: x => x.InventoryItemDetailId,
                        principalTable: "InventoryItemDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryDispenseDetails_PatientPersonalInformations_PatientPersonalInformationId",
                        column: x => x.PatientPersonalInformationId,
                        principalTable: "PatientPersonalInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryDispenseDetails_InventoryItemDetailId",
                table: "InventoryDispenseDetails",
                column: "InventoryItemDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryDispenseDetails_PatientPersonalInformationId",
                table: "InventoryDispenseDetails",
                column: "PatientPersonalInformationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryDispenseDetails");
        }
    }
}
