using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhanMemWebQuanLiBenhVien.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "missions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Lever",
                table: "missions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PhongBenhId",
                table: "missions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PhongKhamId",
                table: "missions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoomType",
                table: "missions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_missions_PhongBenhId",
                table: "missions",
                column: "PhongBenhId");

            migrationBuilder.CreateIndex(
                name: "IX_missions_PhongKhamId",
                table: "missions",
                column: "PhongKhamId");

            migrationBuilder.AddForeignKey(
                name: "FK_missions_phongBenhs_PhongBenhId",
                table: "missions",
                column: "PhongBenhId",
                principalTable: "phongBenhs",
                principalColumn: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_missions_phongKhams_PhongKhamId",
                table: "missions",
                column: "PhongKhamId",
                principalTable: "phongKhams",
                principalColumn: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_missions_phongBenhs_PhongBenhId",
                table: "missions");

            migrationBuilder.DropForeignKey(
                name: "FK_missions_phongKhams_PhongKhamId",
                table: "missions");

            migrationBuilder.DropIndex(
                name: "IX_missions_PhongBenhId",
                table: "missions");

            migrationBuilder.DropIndex(
                name: "IX_missions_PhongKhamId",
                table: "missions");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "missions");

            migrationBuilder.DropColumn(
                name: "Lever",
                table: "missions");

            migrationBuilder.DropColumn(
                name: "PhongBenhId",
                table: "missions");

            migrationBuilder.DropColumn(
                name: "PhongKhamId",
                table: "missions");

            migrationBuilder.DropColumn(
                name: "RoomType",
                table: "missions");
        }
    }
}
