using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhanMemWebQuanLiBenhVien.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class B : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrangThaiDieuTri",
                table: "patients");

            migrationBuilder.AlterColumn<int>(
                name: "TrangThaiBenhAn",
                table: "patients",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrangThaiDieuTri",
                table: "medicalRecords",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrangThaiDieuTri",
                table: "medicalRecords");

            migrationBuilder.AlterColumn<int>(
                name: "TrangThaiBenhAn",
                table: "patients",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TrangThaiDieuTri",
                table: "patients",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
