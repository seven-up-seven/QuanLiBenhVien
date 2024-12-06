using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhanMemWebQuanLiBenhVien.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class E : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfessionId",
                table: "phongKhams",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfessionId",
                table: "phongBenhs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "phongCapCuus",
                columns: table => new
                {
                    RoomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phongCapCuus", x => x.RoomId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_phongKhams_ProfessionId",
                table: "phongKhams",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_phongBenhs_ProfessionId",
                table: "phongBenhs",
                column: "ProfessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_phongBenhs_professions_ProfessionId",
                table: "phongBenhs",
                column: "ProfessionId",
                principalTable: "professions",
                principalColumn: "ProfessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_phongKhams_professions_ProfessionId",
                table: "phongKhams",
                column: "ProfessionId",
                principalTable: "professions",
                principalColumn: "ProfessionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_phongBenhs_professions_ProfessionId",
                table: "phongBenhs");

            migrationBuilder.DropForeignKey(
                name: "FK_phongKhams_professions_ProfessionId",
                table: "phongKhams");

            migrationBuilder.DropTable(
                name: "phongCapCuus");

            migrationBuilder.DropIndex(
                name: "IX_phongKhams_ProfessionId",
                table: "phongKhams");

            migrationBuilder.DropIndex(
                name: "IX_phongBenhs_ProfessionId",
                table: "phongBenhs");

            migrationBuilder.DropColumn(
                name: "ProfessionId",
                table: "phongKhams");

            migrationBuilder.DropColumn(
                name: "ProfessionId",
                table: "phongBenhs");
        }
    }
}
