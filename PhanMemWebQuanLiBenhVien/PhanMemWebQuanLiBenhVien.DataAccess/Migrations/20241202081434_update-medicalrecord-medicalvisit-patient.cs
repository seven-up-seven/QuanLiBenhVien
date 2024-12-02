using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhanMemWebQuanLiBenhVien.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updatemedicalrecordmedicalvisitpatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WentToHospitalDay",
                table: "medicalRecords");

            migrationBuilder.RenameColumn(
                name: "Prediction",
                table: "medicalRecords",
                newName: "TienSuBenhAn");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "medicalRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BHYT",
                table: "medicalRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PatientGender",
                table: "medicalRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PatientName",
                table: "medicalRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "MedicalVisit",
                columns: table => new
                {
                    VisitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicalRecordId = table.Column<int>(type: "int", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Symptom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KetQuaLamSang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChanDoan = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalVisit", x => x.VisitId);
                    table.ForeignKey(
                        name: "FK_MedicalVisit_medicalRecords_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "medicalRecords",
                        principalColumn: "MedicalRecordId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalVisit_MedicalRecordId",
                table: "MedicalVisit",
                column: "MedicalRecordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalVisit");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "medicalRecords");

            migrationBuilder.DropColumn(
                name: "BHYT",
                table: "medicalRecords");

            migrationBuilder.DropColumn(
                name: "PatientGender",
                table: "medicalRecords");

            migrationBuilder.DropColumn(
                name: "PatientName",
                table: "medicalRecords");

            migrationBuilder.RenameColumn(
                name: "TienSuBenhAn",
                table: "medicalRecords",
                newName: "Prediction");

            migrationBuilder.AddColumn<DateTime>(
                name: "WentToHospitalDay",
                table: "medicalRecords",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
