using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhanMemWebQuanLiBenhVien.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class A : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    UserRole = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "nurses",
                columns: table => new
                {
                    NurseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NurseCCCD = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    NurseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NurseGender = table.Column<int>(type: "int", nullable: false),
                    NurseAge = table.Column<int>(type: "int", nullable: false),
                    NurseImgURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasAccount = table.Column<bool>(type: "bit", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nurses", x => x.NurseId);
                });

            migrationBuilder.CreateTable(
                name: "phongBenhs",
                columns: table => new
                {
                    RoomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfBeds = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phongBenhs", x => x.RoomId);
                });

            migrationBuilder.CreateTable(
                name: "phongKhams",
                columns: table => new
                {
                    RoomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phongKhams", x => x.RoomId);
                });

            migrationBuilder.CreateTable(
                name: "professions",
                columns: table => new
                {
                    ProfessionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfessionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TruongKhoaId = table.Column<int>(type: "int", nullable: true),
                    TruongKhoaName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_professions", x => x.ProfessionId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "doctors",
                columns: table => new
                {
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorCCCD = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    DoctorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoctorGender = table.Column<int>(type: "int", nullable: false),
                    DoctorAge = table.Column<int>(type: "int", nullable: false),
                    DoctorImgURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasAccount = table.Column<bool>(type: "bit", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsTruongKhoa = table.Column<bool>(type: "bit", nullable: false),
                    ProfessionId = table.Column<int>(type: "int", nullable: false),
                    ViTriLamViec = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_doctors", x => x.DoctorId);
                    table.ForeignKey(
                        name: "FK_doctors_professions_ProfessionId",
                        column: x => x.ProfessionId,
                        principalTable: "professions",
                        principalColumn: "ProfessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "missions",
                columns: table => new
                {
                    MissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_missions", x => x.MissionId);
                    table.ForeignKey(
                        name: "FK_missions_doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "doctors",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "patients",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CCCD = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TrangThaiDieuTri = table.Column<int>(type: "int", nullable: false),
                    TrangThaiBenhAn = table.Column<int>(type: "int", nullable: true),
                    DoctorId = table.Column<int>(type: "int", nullable: true),
                    NurseId = table.Column<int>(type: "int", nullable: true),
                    PhongBenhRoomId = table.Column<int>(type: "int", nullable: true),
                    PhongKhamRoomId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patients", x => x.PatientId);
                    table.ForeignKey(
                        name: "FK_patients_doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "doctors",
                        principalColumn: "DoctorId");
                    table.ForeignKey(
                        name: "FK_patients_nurses_NurseId",
                        column: x => x.NurseId,
                        principalTable: "nurses",
                        principalColumn: "NurseId");
                    table.ForeignKey(
                        name: "FK_patients_phongBenhs_PhongBenhRoomId",
                        column: x => x.PhongBenhRoomId,
                        principalTable: "phongBenhs",
                        principalColumn: "RoomId");
                    table.ForeignKey(
                        name: "FK_patients_phongKhams_PhongKhamRoomId",
                        column: x => x.PhongKhamRoomId,
                        principalTable: "phongKhams",
                        principalColumn: "RoomId");
                });

            migrationBuilder.CreateTable(
                name: "workSchedules",
                columns: table => new
                {
                    WorkScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomOfMonday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomOfTuesday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomOfWednesday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomOfThurday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomOfFriday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomOfSaturday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomOfSunday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workSchedules", x => x.WorkScheduleId);
                    table.ForeignKey(
                        name: "FK_workSchedules_doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "doctors",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "medicalRecords",
                columns: table => new
                {
                    MedicalRecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientGender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BHYT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TienSuBenhAn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: true),
                    PhongKhamId = table.Column<int>(type: "int", nullable: true),
                    PhongBenhId = table.Column<int>(type: "int", nullable: true),
                    NurseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medicalRecords", x => x.MedicalRecordId);
                    table.ForeignKey(
                        name: "FK_medicalRecords_doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "doctors",
                        principalColumn: "DoctorId");
                    table.ForeignKey(
                        name: "FK_medicalRecords_nurses_NurseId",
                        column: x => x.NurseId,
                        principalTable: "nurses",
                        principalColumn: "NurseId");
                    table.ForeignKey(
                        name: "FK_medicalRecords_patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_medicalRecords_phongBenhs_PhongBenhId",
                        column: x => x.PhongBenhId,
                        principalTable: "phongBenhs",
                        principalColumn: "RoomId");
                    table.ForeignKey(
                        name: "FK_medicalRecords_phongKhams_PhongKhamId",
                        column: x => x.PhongKhamId,
                        principalTable: "phongKhams",
                        principalColumn: "RoomId");
                });

            migrationBuilder.CreateTable(
                name: "medicalVisits",
                columns: table => new
                {
                    VisitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicalRecordId = table.Column<int>(type: "int", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Symptom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KetQuaLamSang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChanDoan = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medicalVisits", x => x.VisitId);
                    table.ForeignKey(
                        name: "FK_medicalVisits_medicalRecords_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "medicalRecords",
                        principalColumn: "MedicalRecordId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_doctors_ProfessionId",
                table: "doctors",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_medicalRecords_DoctorId",
                table: "medicalRecords",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_medicalRecords_NurseId",
                table: "medicalRecords",
                column: "NurseId");

            migrationBuilder.CreateIndex(
                name: "IX_medicalRecords_PatientId",
                table: "medicalRecords",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_medicalRecords_PhongBenhId",
                table: "medicalRecords",
                column: "PhongBenhId");

            migrationBuilder.CreateIndex(
                name: "IX_medicalRecords_PhongKhamId",
                table: "medicalRecords",
                column: "PhongKhamId");

            migrationBuilder.CreateIndex(
                name: "IX_medicalVisits_MedicalRecordId",
                table: "medicalVisits",
                column: "MedicalRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_missions_DoctorId",
                table: "missions",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_patients_DoctorId",
                table: "patients",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_patients_NurseId",
                table: "patients",
                column: "NurseId");

            migrationBuilder.CreateIndex(
                name: "IX_patients_PhongBenhRoomId",
                table: "patients",
                column: "PhongBenhRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_patients_PhongKhamRoomId",
                table: "patients",
                column: "PhongKhamRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_workSchedules_DoctorId",
                table: "workSchedules",
                column: "DoctorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "medicalVisits");

            migrationBuilder.DropTable(
                name: "missions");

            migrationBuilder.DropTable(
                name: "workSchedules");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "medicalRecords");

            migrationBuilder.DropTable(
                name: "patients");

            migrationBuilder.DropTable(
                name: "doctors");

            migrationBuilder.DropTable(
                name: "nurses");

            migrationBuilder.DropTable(
                name: "phongBenhs");

            migrationBuilder.DropTable(
                name: "phongKhams");

            migrationBuilder.DropTable(
                name: "professions");
        }
    }
}
