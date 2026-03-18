using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareSync.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPKOnPatientPersonalInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HealthInformationId",
                table: "PatientPersonalInformations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PatientPersonalInformations_HealthInformationId",
                table: "PatientPersonalInformations",
                column: "HealthInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientPersonalInformations_PatientHealthInformations_HealthInformationId",
                table: "PatientPersonalInformations",
                column: "HealthInformationId",
                principalTable: "PatientHealthInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientPersonalInformations_PatientHealthInformations_HealthInformationId",
                table: "PatientPersonalInformations");

            migrationBuilder.DropIndex(
                name: "IX_PatientPersonalInformations_HealthInformationId",
                table: "PatientPersonalInformations");

            migrationBuilder.DropColumn(
                name: "HealthInformationId",
                table: "PatientPersonalInformations");
        }
    }
}
