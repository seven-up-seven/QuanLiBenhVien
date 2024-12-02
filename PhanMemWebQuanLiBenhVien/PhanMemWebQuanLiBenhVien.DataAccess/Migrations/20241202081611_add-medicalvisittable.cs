using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhanMemWebQuanLiBenhVien.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addmedicalvisittable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalVisit_medicalRecords_MedicalRecordId",
                table: "MedicalVisit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicalVisit",
                table: "MedicalVisit");

            migrationBuilder.RenameTable(
                name: "MedicalVisit",
                newName: "medicalVisits");

            migrationBuilder.RenameIndex(
                name: "IX_MedicalVisit_MedicalRecordId",
                table: "medicalVisits",
                newName: "IX_medicalVisits_MedicalRecordId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_medicalVisits",
                table: "medicalVisits",
                column: "VisitId");

            migrationBuilder.AddForeignKey(
                name: "FK_medicalVisits_medicalRecords_MedicalRecordId",
                table: "medicalVisits",
                column: "MedicalRecordId",
                principalTable: "medicalRecords",
                principalColumn: "MedicalRecordId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_medicalVisits_medicalRecords_MedicalRecordId",
                table: "medicalVisits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_medicalVisits",
                table: "medicalVisits");

            migrationBuilder.RenameTable(
                name: "medicalVisits",
                newName: "MedicalVisit");

            migrationBuilder.RenameIndex(
                name: "IX_medicalVisits_MedicalRecordId",
                table: "MedicalVisit",
                newName: "IX_MedicalVisit_MedicalRecordId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicalVisit",
                table: "MedicalVisit",
                column: "VisitId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalVisit_medicalRecords_MedicalRecordId",
                table: "MedicalVisit",
                column: "MedicalRecordId",
                principalTable: "medicalRecords",
                principalColumn: "MedicalRecordId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
