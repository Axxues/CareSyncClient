using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareSync.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditForeignKeyOnPrescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsultationPrescriptions_PatientPersonalInformations_PatientPersonalInformationId",
                table: "ConsultationPrescriptions");

            migrationBuilder.DropIndex(
                name: "IX_ConsultationPrescriptions_PatientPersonalInformationId",
                table: "ConsultationPrescriptions");

            migrationBuilder.RenameColumn(
                name: "PatientPersonalInformationId",
                table: "ConsultationPrescriptions",
                newName: "CosultationDetailId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "CosultationDetailId",
                table: "ConsultationPrescriptions",
                newName: "PatientPersonalInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationPrescriptions_PatientPersonalInformationId",
                table: "ConsultationPrescriptions",
                column: "PatientPersonalInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsultationPrescriptions_PatientPersonalInformations_PatientPersonalInformationId",
                table: "ConsultationPrescriptions",
                column: "PatientPersonalInformationId",
                principalTable: "PatientPersonalInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
