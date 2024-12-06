using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhanMemWebQuanLiBenhVien.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class C : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrangThaiBenhAn",
                table: "medicalRecords",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrangThaiBenhAn",
                table: "medicalRecords");
        }
    }
}
