using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareSync.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditPKDatatypeOnHealthInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. EF ALREADY GENERATED THESE
            migrationBuilder.DropForeignKey(
                name: "FK_PatientPersonalInformations_PatientHealthInformations_HealthInformationId",
                table: "PatientPersonalInformations");

            migrationBuilder.DropIndex(
                name: "IX_PatientPersonalInformations_HealthInformationId",
                table: "PatientPersonalInformations");

            migrationBuilder.DropColumn(
                name: "HealthInformationId",
                table: "PatientPersonalInformations");

            // 2. DROP THE PRIMARY KEY SHIELD
            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientHealthInformations",
                table: "PatientHealthInformations");

            // 3. NUKE THE OLD STRING COLUMN
            migrationBuilder.DropColumn(
                name: "Id",
                table: "PatientHealthInformations");

            // 4. CREATE THE BRAND NEW INT IDENTITY COLUMN
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PatientHealthInformations",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            // 5. PUT THE PRIMARY KEY BACK ON THE NEW COLUMN
            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientHealthInformations",
                table: "PatientHealthInformations",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HealthInformationId",
                table: "PatientPersonalInformations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "PatientHealthInformations",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

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
    }
}
