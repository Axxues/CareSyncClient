using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareSync.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddConsultationPrescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Weight",
                table: "ConsultationDetails",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "ConsultationPrescriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientPersonalInformationId = table.Column<int>(type: "int", nullable: false),
                    Complaint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Medications = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultationPrescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsultationPrescriptions_PatientPersonalInformations_PatientPersonalInformationId",
                        column: x => x.PatientPersonalInformationId,
                        principalTable: "PatientPersonalInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationPrescriptions_PatientPersonalInformationId",
                table: "ConsultationPrescriptions",
                column: "PatientPersonalInformationId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsultationPrescriptions");

            migrationBuilder.AlterColumn<int>(
                name: "Weight",
                table: "ConsultationDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
