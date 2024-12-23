using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhanMemWebQuanLiBenhVien.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class S : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "activityHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(type: "int", nullable: true),
                    MemberName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ObjectId = table.Column<int>(type: "int", nullable: true),
                    Activity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberRole = table.Column<int>(type: "int", nullable: false),
                    ActivityTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDetails = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_activityHistories", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "activityHistories");
        }
    }
}
