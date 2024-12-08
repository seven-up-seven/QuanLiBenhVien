using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhanMemWebQuanLiBenhVien.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class F : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_workSchedules_doctors_DoctorId",
                table: "workSchedules");

            migrationBuilder.DropColumn(
                name: "RoomOfFriday",
                table: "workSchedules");

            migrationBuilder.DropColumn(
                name: "RoomOfMonday",
                table: "workSchedules");

            migrationBuilder.DropColumn(
                name: "RoomOfSaturday",
                table: "workSchedules");

            migrationBuilder.DropColumn(
                name: "RoomOfSunday",
                table: "workSchedules");

            migrationBuilder.DropColumn(
                name: "RoomOfThurday",
                table: "workSchedules");

            migrationBuilder.DropColumn(
                name: "RoomOfTuesday",
                table: "workSchedules");

            migrationBuilder.DropColumn(
                name: "RoomOfWednesday",
                table: "workSchedules");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "workSchedules",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "NurseId",
                table: "workSchedules",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfesisonId",
                table: "patients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfesisonId",
                table: "medicalRecords",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_workSchedules_NurseId",
                table: "workSchedules",
                column: "NurseId");

            migrationBuilder.CreateIndex(
                name: "IX_patients_ProfesisonId",
                table: "patients",
                column: "ProfesisonId");

            migrationBuilder.CreateIndex(
                name: "IX_medicalRecords_ProfesisonId",
                table: "medicalRecords",
                column: "ProfesisonId");

            migrationBuilder.AddForeignKey(
                name: "FK_medicalRecords_professions_ProfesisonId",
                table: "medicalRecords",
                column: "ProfesisonId",
                principalTable: "professions",
                principalColumn: "ProfessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_patients_professions_ProfesisonId",
                table: "patients",
                column: "ProfesisonId",
                principalTable: "professions",
                principalColumn: "ProfessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_workSchedules_doctors_DoctorId",
                table: "workSchedules",
                column: "DoctorId",
                principalTable: "doctors",
                principalColumn: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_workSchedules_nurses_NurseId",
                table: "workSchedules",
                column: "NurseId",
                principalTable: "nurses",
                principalColumn: "NurseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_medicalRecords_professions_ProfesisonId",
                table: "medicalRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_patients_professions_ProfesisonId",
                table: "patients");

            migrationBuilder.DropForeignKey(
                name: "FK_workSchedules_doctors_DoctorId",
                table: "workSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_workSchedules_nurses_NurseId",
                table: "workSchedules");

            migrationBuilder.DropIndex(
                name: "IX_workSchedules_NurseId",
                table: "workSchedules");

            migrationBuilder.DropIndex(
                name: "IX_patients_ProfesisonId",
                table: "patients");

            migrationBuilder.DropIndex(
                name: "IX_medicalRecords_ProfesisonId",
                table: "medicalRecords");

            migrationBuilder.DropColumn(
                name: "NurseId",
                table: "workSchedules");

            migrationBuilder.DropColumn(
                name: "ProfesisonId",
                table: "patients");

            migrationBuilder.DropColumn(
                name: "ProfesisonId",
                table: "medicalRecords");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "workSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoomOfFriday",
                table: "workSchedules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoomOfMonday",
                table: "workSchedules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoomOfSaturday",
                table: "workSchedules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoomOfSunday",
                table: "workSchedules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoomOfThurday",
                table: "workSchedules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoomOfTuesday",
                table: "workSchedules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoomOfWednesday",
                table: "workSchedules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_workSchedules_doctors_DoctorId",
                table: "workSchedules",
                column: "DoctorId",
                principalTable: "doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
