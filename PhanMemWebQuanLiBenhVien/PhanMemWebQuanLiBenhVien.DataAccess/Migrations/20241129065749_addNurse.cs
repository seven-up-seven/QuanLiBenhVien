using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhanMemWebQuanLiBenhVien.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addNurse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NurseId",
                table: "patients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_patients_NurseId",
                table: "patients",
                column: "NurseId");

            migrationBuilder.AddForeignKey(
                name: "FK_patients_nurses_NurseId",
                table: "patients",
                column: "NurseId",
                principalTable: "nurses",
                principalColumn: "NurseId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_patients_nurses_NurseId",
                table: "patients");

            migrationBuilder.DropIndex(
                name: "IX_patients_NurseId",
                table: "patients");

            migrationBuilder.DropColumn(
                name: "NurseId",
                table: "patients");
        }
    }
}
