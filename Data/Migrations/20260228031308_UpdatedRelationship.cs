using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareSync.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ConsultationPrescriptions_PatientPersonalInformationId",
                table: "ConsultationPrescriptions");

            migrationBuilder.DropIndex(
                name: "IX_ConsultationDetails_PatientPersonalInformationId",
                table: "ConsultationDetails");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationPrescriptions_PatientPersonalInformationId",
                table: "ConsultationPrescriptions",
                column: "PatientPersonalInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationDetails_PatientPersonalInformationId",
                table: "ConsultationDetails",
                column: "PatientPersonalInformationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ConsultationPrescriptions_PatientPersonalInformationId",
                table: "ConsultationPrescriptions");

            migrationBuilder.DropIndex(
                name: "IX_ConsultationDetails_PatientPersonalInformationId",
                table: "ConsultationDetails");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationPrescriptions_PatientPersonalInformationId",
                table: "ConsultationPrescriptions",
                column: "PatientPersonalInformationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationDetails_PatientPersonalInformationId",
                table: "ConsultationDetails",
                column: "PatientPersonalInformationId",
                unique: true);
        }
    }
}
