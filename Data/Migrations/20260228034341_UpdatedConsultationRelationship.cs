using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareSync.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedConsultationRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsultationPrescriptions_ConsultationDetails_ConsultationDetailId",
                table: "ConsultationPrescriptions");

            migrationBuilder.DropIndex(
                name: "IX_ConsultationPrescriptions_ConsultationDetailId",
                table: "ConsultationPrescriptions");

            migrationBuilder.DropColumn(
                name: "ConsultationDetailId",
                table: "ConsultationPrescriptions");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationPrescriptions_CosultationDetailId",
                table: "ConsultationPrescriptions",
                column: "CosultationDetailId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ConsultationPrescriptions_ConsultationDetails_CosultationDetailId",
                table: "ConsultationPrescriptions",
                column: "CosultationDetailId",
                principalTable: "ConsultationDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsultationPrescriptions_ConsultationDetails_CosultationDetailId",
                table: "ConsultationPrescriptions");

            migrationBuilder.DropIndex(
                name: "IX_ConsultationPrescriptions_CosultationDetailId",
                table: "ConsultationPrescriptions");

            migrationBuilder.AddColumn<int>(
                name: "ConsultationDetailId",
                table: "ConsultationPrescriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationPrescriptions_ConsultationDetailId",
                table: "ConsultationPrescriptions",
                column: "ConsultationDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsultationPrescriptions_ConsultationDetails_ConsultationDetailId",
                table: "ConsultationPrescriptions",
                column: "ConsultationDetailId",
                principalTable: "ConsultationDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
