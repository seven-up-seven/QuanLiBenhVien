using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhanMemWebQuanLiBenhVien.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDoctorAndProfession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TruongKhoaId",
                table: "professions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsTruongKhoa",
                table: "doctors",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TruongKhoaId",
                table: "professions");

            migrationBuilder.DropColumn(
                name: "IsTruongKhoa",
                table: "doctors");
        }
    }
}
