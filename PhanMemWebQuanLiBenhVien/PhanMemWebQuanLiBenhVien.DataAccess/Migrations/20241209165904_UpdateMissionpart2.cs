using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhanMemWebQuanLiBenhVien.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMissionpart2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_missions_doctors_DoctorId",
                table: "missions");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "missions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_missions_doctors_DoctorId",
                table: "missions",
                column: "DoctorId",
                principalTable: "doctors",
                principalColumn: "DoctorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_missions_doctors_DoctorId",
                table: "missions");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "missions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_missions_doctors_DoctorId",
                table: "missions",
                column: "DoctorId",
                principalTable: "doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
