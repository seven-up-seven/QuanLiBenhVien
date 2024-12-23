using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhanMemWebQuanLiBenhVien.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class asdasasaasas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TenBacSi",
                table: "medicalVisits",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenBacSi",
                table: "medicalVisits");
        }
    }
}
