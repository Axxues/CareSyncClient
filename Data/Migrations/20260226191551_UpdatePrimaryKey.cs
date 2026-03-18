using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareSync.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientPersonalInformations_PatientEmergencyContacts_EmergencyContactId",
                table: "PatientPersonalInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientPersonalInformations_PatientHealthInformations_HealthInformationId",
                table: "PatientPersonalInformations");

            migrationBuilder.DropIndex(
                name: "IX_PatientPersonalInformations_EmergencyContactId",
                table: "PatientPersonalInformations");

            migrationBuilder.DropIndex(
                name: "IX_PatientPersonalInformations_HealthInformationId",
                table: "PatientPersonalInformations");

            migrationBuilder.DropColumn(
                name: "EmergencyContactId",
                table: "PatientPersonalInformations");

            migrationBuilder.DropColumn(
                name: "HealthInformationId",
                table: "PatientPersonalInformations");

            migrationBuilder.AddColumn<int>(
                name: "PatientPersonalInformationId",
                table: "PatientHealthInformations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PatientPersonalInformationId",
                table: "PatientEmergencyContacts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PatientHealthInformations_PatientPersonalInformationId",
                table: "PatientHealthInformations",
                column: "PatientPersonalInformationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientEmergencyContacts_PatientPersonalInformationId",
                table: "PatientEmergencyContacts",
                column: "PatientPersonalInformationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientEmergencyContacts_PatientPersonalInformations_PatientPersonalInformationId",
                table: "PatientEmergencyContacts",
                column: "PatientPersonalInformationId",
                principalTable: "PatientPersonalInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientHealthInformations_PatientPersonalInformations_PatientPersonalInformationId",
                table: "PatientHealthInformations",
                column: "PatientPersonalInformationId",
                principalTable: "PatientPersonalInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientEmergencyContacts_PatientPersonalInformations_PatientPersonalInformationId",
                table: "PatientEmergencyContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientHealthInformations_PatientPersonalInformations_PatientPersonalInformationId",
                table: "PatientHealthInformations");

            migrationBuilder.DropIndex(
                name: "IX_PatientHealthInformations_PatientPersonalInformationId",
                table: "PatientHealthInformations");

            migrationBuilder.DropIndex(
                name: "IX_PatientEmergencyContacts_PatientPersonalInformationId",
                table: "PatientEmergencyContacts");

            migrationBuilder.DropColumn(
                name: "PatientPersonalInformationId",
                table: "PatientHealthInformations");

            migrationBuilder.DropColumn(
                name: "PatientPersonalInformationId",
                table: "PatientEmergencyContacts");

            migrationBuilder.AddColumn<int>(
                name: "EmergencyContactId",
                table: "PatientPersonalInformations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HealthInformationId",
                table: "PatientPersonalInformations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PatientPersonalInformations_EmergencyContactId",
                table: "PatientPersonalInformations",
                column: "EmergencyContactId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientPersonalInformations_HealthInformationId",
                table: "PatientPersonalInformations",
                column: "HealthInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientPersonalInformations_PatientEmergencyContacts_EmergencyContactId",
                table: "PatientPersonalInformations",
                column: "EmergencyContactId",
                principalTable: "PatientEmergencyContacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientPersonalInformations_PatientHealthInformations_HealthInformationId",
                table: "PatientPersonalInformations",
                column: "HealthInformationId",
                principalTable: "PatientHealthInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
