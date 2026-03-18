using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareSync.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PatientPersonalInformationId",
                table: "PatientHealthInformations");

            migrationBuilder.DropColumn(
                name: "PatientPersonalInformationId",
                table: "PatientEmergencyContacts");

            migrationBuilder.RenameColumn(
                name: "PatientPersonalInformationId",
                table: "PatientPersonalInformations",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PatientHealthInformationId",
                table: "PatientHealthInformations",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PatientEmergencyContactId",
                table: "PatientEmergencyContacts",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "EmergencyContactId",
                table: "PatientPersonalInformations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "HealthInformationId",
                table: "PatientPersonalInformations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "KnownAllergies",
                table: "PatientHealthInformations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PatientPersonalInformations",
                newName: "PatientPersonalInformationId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PatientHealthInformations",
                newName: "PatientHealthInformationId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PatientEmergencyContacts",
                newName: "PatientEmergencyContactId");

            migrationBuilder.AlterColumn<string>(
                name: "KnownAllergies",
                table: "PatientHealthInformations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
        }
    }
}
