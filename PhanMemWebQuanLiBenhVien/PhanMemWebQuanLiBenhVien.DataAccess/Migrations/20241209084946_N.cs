using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhanMemWebQuanLiBenhVien.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class N : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "NgayTaiKham",
                table: "medicalVisits",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdThuocs",
                table: "medicalVisits",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoLuongThuocs",
                table: "medicalVisits",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NgayTaiKham",
                table: "medicalVisits");

            migrationBuilder.DropColumn(
                name: "IdThuocs",
                table: "medicalVisits");

            migrationBuilder.DropColumn(
                name: "SoLuongThuocs",
                table: "medicalVisits");
        }
    }
}
