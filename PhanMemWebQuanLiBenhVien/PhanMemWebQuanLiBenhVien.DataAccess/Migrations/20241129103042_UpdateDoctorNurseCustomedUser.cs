using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhanMemWebQuanLiBenhVien.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDoctorNurseCustomedUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CCCD",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ImgURL",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "AspNetUsers",
                newName: "UserRole");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "AspNetUsers",
                newName: "UserId");

            migrationBuilder.AddColumn<int>(
                name: "NurseAge",
                table: "nurses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NurseCCCD",
                table: "nurses",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NurseGender",
                table: "nurses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NurseImgURL",
                table: "nurses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NurseName",
                table: "nurses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DoctorAge",
                table: "doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DoctorCCCD",
                table: "doctors",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DoctorGender",
                table: "doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DoctorImgURL",
                table: "doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoctorName",
                table: "doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NurseAge",
                table: "nurses");

            migrationBuilder.DropColumn(
                name: "NurseCCCD",
                table: "nurses");

            migrationBuilder.DropColumn(
                name: "NurseGender",
                table: "nurses");

            migrationBuilder.DropColumn(
                name: "NurseImgURL",
                table: "nurses");

            migrationBuilder.DropColumn(
                name: "NurseName",
                table: "nurses");

            migrationBuilder.DropColumn(
                name: "DoctorAge",
                table: "doctors");

            migrationBuilder.DropColumn(
                name: "DoctorCCCD",
                table: "doctors");

            migrationBuilder.DropColumn(
                name: "DoctorGender",
                table: "doctors");

            migrationBuilder.DropColumn(
                name: "DoctorImgURL",
                table: "doctors");

            migrationBuilder.DropColumn(
                name: "DoctorName",
                table: "doctors");

            migrationBuilder.RenameColumn(
                name: "UserRole",
                table: "AspNetUsers",
                newName: "Gender");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "AspNetUsers",
                newName: "Age");

            migrationBuilder.AddColumn<string>(
                name: "CCCD",
                table: "AspNetUsers",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgURL",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
