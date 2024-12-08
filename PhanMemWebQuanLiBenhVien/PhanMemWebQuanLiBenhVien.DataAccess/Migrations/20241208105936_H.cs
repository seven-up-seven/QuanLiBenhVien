using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhanMemWebQuanLiBenhVien.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class H : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "workSchedules");

            migrationBuilder.AddColumn<int>(
                name: "PhongCapCuuId",
                table: "workSchedules",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PhongKhamId",
                table: "workSchedules",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_workSchedules_PhongCapCuuId",
                table: "workSchedules",
                column: "PhongCapCuuId");

            migrationBuilder.CreateIndex(
                name: "IX_workSchedules_PhongKhamId",
                table: "workSchedules",
                column: "PhongKhamId");

            migrationBuilder.AddForeignKey(
                name: "FK_workSchedules_phongCapCuus_PhongCapCuuId",
                table: "workSchedules",
                column: "PhongCapCuuId",
                principalTable: "phongCapCuus",
                principalColumn: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_workSchedules_phongKhams_PhongKhamId",
                table: "workSchedules",
                column: "PhongKhamId",
                principalTable: "phongKhams",
                principalColumn: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_workSchedules_phongCapCuus_PhongCapCuuId",
                table: "workSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_workSchedules_phongKhams_PhongKhamId",
                table: "workSchedules");

            migrationBuilder.DropIndex(
                name: "IX_workSchedules_PhongCapCuuId",
                table: "workSchedules");

            migrationBuilder.DropIndex(
                name: "IX_workSchedules_PhongKhamId",
                table: "workSchedules");

            migrationBuilder.DropColumn(
                name: "PhongCapCuuId",
                table: "workSchedules");

            migrationBuilder.DropColumn(
                name: "PhongKhamId",
                table: "workSchedules");

            migrationBuilder.AddColumn<string>(
                name: "DayOfWeek",
                table: "workSchedules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
