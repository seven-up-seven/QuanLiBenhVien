using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhanMemWebQuanLiBenhVien.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class D : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TinhTrangBenhNhan",
                table: "medicalVisits",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TinhTrangBenhNhan",
                table: "medicalRecords",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TinhTrangBenhNhan",
                table: "medicalVisits");

            migrationBuilder.DropColumn(
                name: "TinhTrangBenhNhan",
                table: "medicalRecords");
        }
    }
}
