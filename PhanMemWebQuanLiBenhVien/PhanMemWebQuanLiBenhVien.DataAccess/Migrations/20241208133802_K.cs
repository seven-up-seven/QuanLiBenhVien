using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhanMemWebQuanLiBenhVien.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class K : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_workSchedules_doctors_DoctorId",
                table: "workSchedules");

            migrationBuilder.DropColumn(
                name: "ShiftType",
                table: "workSchedules");

            migrationBuilder.RenameColumn(
                name: "TimeSlot",
                table: "workSchedules",
                newName: "DayOfWeek");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "workSchedules",
                newName: "DoctorId3");

            migrationBuilder.RenameIndex(
                name: "IX_workSchedules_DoctorId",
                table: "workSchedules",
                newName: "IX_workSchedules_DoctorId3");

            migrationBuilder.AddColumn<int>(
                name: "DoctorId1",
                table: "workSchedules",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DoctorId2",
                table: "workSchedules",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_workSchedules_DoctorId1",
                table: "workSchedules",
                column: "DoctorId1");

            migrationBuilder.CreateIndex(
                name: "IX_workSchedules_DoctorId2",
                table: "workSchedules",
                column: "DoctorId2");

            migrationBuilder.AddForeignKey(
                name: "FK_workSchedules_doctors_DoctorId1",
                table: "workSchedules",
                column: "DoctorId1",
                principalTable: "doctors",
                principalColumn: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_workSchedules_doctors_DoctorId2",
                table: "workSchedules",
                column: "DoctorId2",
                principalTable: "doctors",
                principalColumn: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_workSchedules_doctors_DoctorId3",
                table: "workSchedules",
                column: "DoctorId3",
                principalTable: "doctors",
                principalColumn: "DoctorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_workSchedules_doctors_DoctorId1",
                table: "workSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_workSchedules_doctors_DoctorId2",
                table: "workSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_workSchedules_doctors_DoctorId3",
                table: "workSchedules");

            migrationBuilder.DropIndex(
                name: "IX_workSchedules_DoctorId1",
                table: "workSchedules");

            migrationBuilder.DropIndex(
                name: "IX_workSchedules_DoctorId2",
                table: "workSchedules");

            migrationBuilder.DropColumn(
                name: "DoctorId1",
                table: "workSchedules");

            migrationBuilder.DropColumn(
                name: "DoctorId2",
                table: "workSchedules");

            migrationBuilder.RenameColumn(
                name: "DoctorId3",
                table: "workSchedules",
                newName: "DoctorId");

            migrationBuilder.RenameColumn(
                name: "DayOfWeek",
                table: "workSchedules",
                newName: "TimeSlot");

            migrationBuilder.RenameIndex(
                name: "IX_workSchedules_DoctorId3",
                table: "workSchedules",
                newName: "IX_workSchedules_DoctorId");

            migrationBuilder.AddColumn<string>(
                name: "ShiftType",
                table: "workSchedules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_workSchedules_doctors_DoctorId",
                table: "workSchedules",
                column: "DoctorId",
                principalTable: "doctors",
                principalColumn: "DoctorId");
        }
    }
}
