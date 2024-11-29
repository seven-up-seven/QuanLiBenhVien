using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhanMemWebQuanLiBenhVien.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class changeWorkScheduleDoctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_doctors_workSchedules_WorkScheduleId",
                table: "doctors");

            migrationBuilder.DropIndex(
                name: "IX_doctors_WorkScheduleId",
                table: "doctors");

            migrationBuilder.DropColumn(
                name: "WorkScheduleId",
                table: "doctors");

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "workSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_workSchedules_DoctorId",
                table: "workSchedules",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_workSchedules_doctors_DoctorId",
                table: "workSchedules",
                column: "DoctorId",
                principalTable: "doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_workSchedules_doctors_DoctorId",
                table: "workSchedules");

            migrationBuilder.DropIndex(
                name: "IX_workSchedules_DoctorId",
                table: "workSchedules");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "workSchedules");

            migrationBuilder.AddColumn<int>(
                name: "WorkScheduleId",
                table: "doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_doctors_WorkScheduleId",
                table: "doctors",
                column: "WorkScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_doctors_workSchedules_WorkScheduleId",
                table: "doctors",
                column: "WorkScheduleId",
                principalTable: "workSchedules",
                principalColumn: "WorkScheduleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
