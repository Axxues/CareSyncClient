using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareSync.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditPatientProfie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BloodType",
                table: "PatientEmergencyContacts");

            migrationBuilder.DropColumn(
                name: "DateofBirth",
                table: "PatientEmergencyContacts");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "PatientEmergencyContacts");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "PatientEmergencyContacts");

            migrationBuilder.DropColumn(
                name: "ResidentialAddress",
                table: "PatientEmergencyContacts");

            migrationBuilder.DropColumn(
                name: "SecondaryPhoneNumber",
                table: "PatientEmergencyContacts");

            migrationBuilder.RenameColumn(
                name: "Patient_PersonalInformationId",
                table: "PatientPersonalInformations",
                newName: "PatientPersonalInformationId");

            migrationBuilder.RenameColumn(
                name: "Patient_PersonalInformationId",
                table: "PatientHealthInformations",
                newName: "PatientPersonalInformationId");

            migrationBuilder.RenameColumn(
                name: "Patient_HealthInformationId",
                table: "PatientHealthInformations",
                newName: "PatientHealthInformationId");

            migrationBuilder.RenameColumn(
                name: "Patient_PersonalInformationId",
                table: "PatientEmergencyContacts",
                newName: "PatientPersonalInformationId");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "PatientEmergencyContacts",
                newName: "ContactPersonName");

            migrationBuilder.RenameColumn(
                name: "ContactNumber",
                table: "PatientEmergencyContacts",
                newName: "EmergencyNumber");

            migrationBuilder.RenameColumn(
                name: "Patient_EmergencyContactId",
                table: "PatientEmergencyContacts",
                newName: "PatientEmergencyContactId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PatientPersonalInformationId",
                table: "PatientPersonalInformations",
                newName: "Patient_PersonalInformationId");

            migrationBuilder.RenameColumn(
                name: "PatientPersonalInformationId",
                table: "PatientHealthInformations",
                newName: "Patient_PersonalInformationId");

            migrationBuilder.RenameColumn(
                name: "PatientHealthInformationId",
                table: "PatientHealthInformations",
                newName: "Patient_HealthInformationId");

            migrationBuilder.RenameColumn(
                name: "PatientPersonalInformationId",
                table: "PatientEmergencyContacts",
                newName: "Patient_PersonalInformationId");

            migrationBuilder.RenameColumn(
                name: "EmergencyNumber",
                table: "PatientEmergencyContacts",
                newName: "ContactNumber");

            migrationBuilder.RenameColumn(
                name: "ContactPersonName",
                table: "PatientEmergencyContacts",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "PatientEmergencyContactId",
                table: "PatientEmergencyContacts",
                newName: "Patient_EmergencyContactId");

            migrationBuilder.AddColumn<string>(
                name: "BloodType",
                table: "PatientEmergencyContacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DateofBirth",
                table: "PatientEmergencyContacts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "PatientEmergencyContacts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "PatientEmergencyContacts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ResidentialAddress",
                table: "PatientEmergencyContacts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecondaryPhoneNumber",
                table: "PatientEmergencyContacts",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }
    }
}
