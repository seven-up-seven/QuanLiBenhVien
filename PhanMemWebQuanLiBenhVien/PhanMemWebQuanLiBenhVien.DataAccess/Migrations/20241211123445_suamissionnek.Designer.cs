﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PhanMemWebQuanLiBenhVien.DataAccess;

#nullable disable

namespace PhanMemWebQuanLiBenhVien.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241211123445_suamissionnek")]
    partial class suamissionnek
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator().HasValue("IdentityUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("PhanMemWebQuanLiBenhVien.Models.Doctor", b =>
                {
                    b.Property<int>("DoctorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DoctorId"));

                    b.Property<int>("DoctorAge")
                        .HasColumnType("int");

                    b.Property<string>("DoctorCCCD")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<int>("DoctorGender")
                        .HasColumnType("int");

                    b.Property<string>("DoctorImgURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DoctorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HasAccount")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTruongKhoa")
                        .HasColumnType("bit");

                    b.Property<int>("ProfessionId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ViTriLamViec")
                        .HasColumnType("int");

                    b.HasKey("DoctorId");

                    b.HasIndex("ProfessionId");

                    b.ToTable("doctors");
                });

            modelBuilder.Entity("PhanMemWebQuanLiBenhVien.Models.MedicalRecord", b =>
                {
                    b.Property<int>("MedicalRecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MedicalRecordId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BHYT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DoctorId")
                        .HasColumnType("int");

                    b.Property<int?>("NurseId")
                        .HasColumnType("int");

                    b.Property<string>("PatientGender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<string>("PatientName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PhongBenhId")
                        .HasColumnType("int");

                    b.Property<int?>("PhongKhamId")
                        .HasColumnType("int");

                    b.Property<int?>("ProfesisonId")
                        .HasColumnType("int");

                    b.Property<string>("TienSuBenhAn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TinhTrangBenhNhan")
                        .HasColumnType("int");

                    b.Property<int?>("TrangThaiBenhAn")
                        .HasColumnType("int");

                    b.Property<int?>("TrangThaiDieuTri")
                        .HasColumnType("int");

                    b.HasKey("MedicalRecordId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("NurseId");

                    b.HasIndex("PatientId");

                    b.HasIndex("PhongBenhId");

                    b.HasIndex("PhongKhamId");

                    b.HasIndex("ProfesisonId");

                    b.ToTable("medicalRecords");
                });

            modelBuilder.Entity("PhanMemWebQuanLiBenhVien.Models.MedicalVisit", b =>
                {
                    b.Property<int>("VisitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VisitId"));

                    b.Property<string>("ChanDoan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdThuocs")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KetQuaLamSang")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MedicalRecordId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("NgayTaiKham")
                        .HasColumnType("datetime2");

                    b.Property<string>("SoLuongThuocs")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Symptom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TinhTrangBenhNhan")
                        .HasColumnType("int");

                    b.Property<DateTime>("VisitDate")
                        .HasColumnType("datetime2");

                    b.HasKey("VisitId");

                    b.HasIndex("MedicalRecordId");

                    b.ToTable("medicalVisits");
                });

            modelBuilder.Entity("PhanMemWebQuanLiBenhVien.Models.Mission", b =>
                {
                    b.Property<int>("MissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MissionId"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<int>("Lever")
                        .HasColumnType("int");

                    b.Property<int?>("PhongBenhId")
                        .HasColumnType("int");

                    b.Property<int?>("PhongKhamId")
                        .HasColumnType("int");

                    b.Property<int>("RoomType")
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("MissionId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PhongBenhId");

                    b.HasIndex("PhongKhamId");

                    b.ToTable("missions");
                });

            modelBuilder.Entity("PhanMemWebQuanLiBenhVien.Models.Models.Medicine", b =>
                {
                    b.Property<int>("MedicineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MedicineId"));

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Usage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MedicineId");

                    b.ToTable("medicines");
                });

            modelBuilder.Entity("PhanMemWebQuanLiBenhVien.Models.Models.PhongCapCuu", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoomId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isAvailable")
                        .HasColumnType("bit");

                    b.HasKey("RoomId");

                    b.ToTable("phongCapCuus");
                });

            modelBuilder.Entity("PhanMemWebQuanLiBenhVien.Models.Nurse", b =>
                {
                    b.Property<int>("NurseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NurseId"));

                    b.Property<bool>("HasAccount")
                        .HasColumnType("bit");

                    b.Property<int>("NurseAge")
                        .HasColumnType("int");

                    b.Property<string>("NurseCCCD")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<int>("NurseGender")
                        .HasColumnType("int");

                    b.Property<string>("NurseImgURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NurseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NurseId");

                    b.ToTable("nurses");
                });

            modelBuilder.Entity("PhanMemWebQuanLiBenhVien.Models.Patient", b =>
                {
                    b.Property<int>("PatientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PatientId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CCCD")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DoctorId")
                        .HasColumnType("int");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NurseId")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int?>("PhongBenhRoomId")
                        .HasColumnType("int");

                    b.Property<int?>("PhongKhamRoomId")
                        .HasColumnType("int");

                    b.Property<int?>("ProfesisonId")
                        .HasColumnType("int");

                    b.Property<int>("TrangThaiBenhAn")
                        .HasColumnType("int");

                    b.HasKey("PatientId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("NurseId");

                    b.HasIndex("PhongBenhRoomId");

                    b.HasIndex("PhongKhamRoomId");

                    b.HasIndex("ProfesisonId");

                    b.ToTable("patients");
                });

            modelBuilder.Entity("PhanMemWebQuanLiBenhVien.Models.PhongBenh", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoomId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfBeds")
                        .HasColumnType("int");

                    b.Property<int?>("ProfessionId")
                        .HasColumnType("int");

                    b.HasKey("RoomId");

                    b.HasIndex("ProfessionId");

                    b.ToTable("phongBenhs");
                });

            modelBuilder.Entity("PhanMemWebQuanLiBenhVien.Models.PhongKham", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoomId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProfessionId")
                        .HasColumnType("int");

                    b.HasKey("RoomId");

                    b.HasIndex("ProfessionId");

                    b.ToTable("phongKhams");
                });

            modelBuilder.Entity("PhanMemWebQuanLiBenhVien.Models.Profession", b =>
                {
                    b.Property<int>("ProfessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProfessionId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfessionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TruongKhoaId")
                        .HasColumnType("int");

                    b.Property<string>("TruongKhoaName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProfessionId");

                    b.ToTable("professions");
                });

            modelBuilder.Entity("PhanMemWebQuanLiBenhVien.Models.WorkSchedule", b =>
                {
                    b.Property<int>("WorkScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WorkScheduleId"));

                    b.Property<string>("DayOfWeek")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DoctorId1")
                        .HasColumnType("int");

                    b.Property<int?>("DoctorId2")
                        .HasColumnType("int");

                    b.Property<int?>("DoctorId3")
                        .HasColumnType("int");

                    b.Property<int?>("NurseId")
                        .HasColumnType("int");

                    b.Property<int?>("PhongCapCuuId")
                        .HasColumnType("int");

                    b.Property<int?>("PhongKhamId")
                        .HasColumnType("int");

                    b.HasKey("WorkScheduleId");

                    b.HasIndex("DoctorId1");

                    b.HasIndex("DoctorId2");

                    b.HasIndex("DoctorId3");

                    b.HasIndex("NurseId");

                    b.HasIndex("PhongCapCuuId");

                    b.HasIndex("PhongKhamId");

                    b.ToTable("workSchedules");
                });

            modelBuilder.Entity("PhanMemWebQuanLiBenhVien.Models.CustomedUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("CustomedUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PhanMemWebQuanLiBenhVien.Models.Doctor", b =>
                {
                    b.HasOne("PhanMemWebQuanLiBenhVien.Models.Profession", "Profession")
                        .WithMany("DoctorList")
                        .HasForeignKey("ProfessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profession");
                });

            modelBuilder.Entity("PhanMemWebQuanLiBenhVien.Models.MedicalRecord", b =>
                {
                    b.HasOne("PhanMemWebQuanLiBenhVien.Models.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId");

                    b.HasOne("PhanMemWebQuanLiBenhVien.Models.Nurse", "Nurse")
                        .WithMany()
                        .HasForeignKey("NurseId");

                    b.HasOne("PhanMemWebQuanLiBenhVien.Models.Patient", "Patient")
                        .WithMany("MedicalRecords")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhanMemWebQuanLiBenhVien.Models.PhongBenh", "PhongBenh")
                        .WithMany()
                        .HasForeignKey("PhongBenhId");

                    b.HasOne("PhanMemWebQuanLiBenhVien.Models.PhongKham", "PhongKham")
                        .WithMany()
                        .HasForeignKey("PhongKhamId");

                    b.HasOne("PhanMemWebQuanLiBenhVien.Models.Profession", "Profession")
                        .WithMany()
                        .HasForeignKey("ProfesisonId");

                    b.Navigation("Doctor");

                    b.Navigation("Nurse");

                    b.Navigation("Patient");

                    b.Navigation("PhongBenh");

                    b.Navigation("PhongKham");

                    b.Navigation("Profession");
                });

            modelBuilder.Entity("PhanMemWebQuanLiBenhVien.Models.MedicalVisit", b =>
                {
                    b.HasOne("PhanMemWebQuanLiBenhVien.Models.MedicalRecord", "MedicalRecord")
                        .WithMany("Visits")
                        .HasForeignKey("MedicalRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MedicalRecord");
                });

            modelBuilder.Entity("PhanMemWebQuanLiBenhVien.Models.Mission", b =>
                {
                    b.HasOne("PhanMemWebQuanLiBenhVien.Models.Doctor", "Doctor")
                        .WithMany("MissionList")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhanMemWebQuanLiBenhVien.Models.PhongBenh", "PhongBenh")
                        .WithMany()
                        .HasForeignKey("PhongBenhId");

                    b.HasOne("PhanMemWebQuanLiBenhVien.Models.PhongKham", "PhongKham")
                        .WithMany()
                        .HasForeignKey("PhongKhamId");

                    b.Navigation("Doctor");

                    b.Navigation("PhongBenh");

                    b.Navigation("PhongKham");
                });

            modelBuilder.Entity("PhanMemWebQuanLiBenhVien.Models.Patient", b =>
                {
                    b.HasOne("PhanMemWebQuanLiBenhVien.Models.Doctor", null)
                        .WithMany("PatientList")
                        .HasForeignKey("DoctorId");

                    b.HasOne("PhanMemWebQuanLiBenhVien.Models.Nurse", null)
                        .WithMany("PatientList")
                        .HasForeignKey("NurseId");

                    b.HasOne("PhanMemWebQuanLiBenhVien.Models.PhongBenh", null)
                        .WithMany("Patients")
                        .HasForeignKey("PhongBenhRoomId");

                    b.HasOne("PhanMemWebQuanLiBenhVien.Models.PhongKham", null)
                        .WithMany("Patients")
                        .HasForeignKey("PhongKhamRoomId");

                    b.HasOne("PhanMemWebQuanLiBenhVien.Models.Profession", "Profession")
                        .WithMany()
                        .HasForeignKey("ProfesisonId");

                    b.Navigation("Profession");
                });

            modelBuilder.Entity("PhanMemWebQuanLiBenhVien.Models.PhongBenh", b =>
                {
                    b.HasOne("PhanMemWebQuanLiBenhVien.Models.Profession", "Profession")
                        .WithMany("PhongBenhList")
                        .HasForeignKey("ProfessionId");

                    b.Navigation("Profession");
                });

            modelBuilder.Entity("PhanMemWebQuanLiBenhVien.Models.PhongKham", b =>
                {
                    b.HasOne("PhanMemWebQuanLiBenhVien.Models.Profession", "Profession")
                        .WithMany("PhongKhamList")
                        .HasForeignKey("ProfessionId");

                    b.Navigation("Profession");
                });

            modelBuilder.Entity("PhanMemWebQuanLiBenhVien.Models.WorkSchedule", b =>
                {
                    b.HasOne("PhanMemWebQuanLiBenhVien.Models.Doctor", "Doctor1")
                        .WithMany()
                        .HasForeignKey("DoctorId1");

                    b.HasOne("PhanMemWebQuanLiBenhVien.Models.Doctor", "Doctor2")
                        .WithMany()
                        .HasForeignKey("DoctorId2");

                    b.HasOne("PhanMemWebQuanLiBenhVien.Models.Doctor", "Doctor3")
                        .WithMany()
                        .HasForeignKey("DoctorId3");

                    b.HasOne("PhanMemWebQuanLiBenhVien.Models.Nurse", "Nurse")
                        .WithMany()
                        .HasForeignKey("NurseId");

                    b.HasOne("PhanMemWebQuanLiBenhVien.Models.Models.PhongCapCuu", "PhongCapCuu")
                        .WithMany("WorkSchedules")
                        .HasForeignKey("PhongCapCuuId");

                    b.HasOne("PhanMemWebQuanLiBenhVien.Models.PhongKham", "PhongKham")
                        .WithMany("WorkSchedules")
                        .HasForeignKey("PhongKhamId");

                    b.Navigation("Doctor1");

                    b.Navigation("Doctor2");

                    b.Navigation("Doctor3");

                    b.Navigation("Nurse");

                    b.Navigation("PhongCapCuu");

                    b.Navigation("PhongKham");
                });

            modelBuilder.Entity("PhanMemWebQuanLiBenhVien.Models.Doctor", b =>
                {
                    b.Navigation("MissionList");

                    b.Navigation("PatientList");
                });

            modelBuilder.Entity("PhanMemWebQuanLiBenhVien.Models.MedicalRecord", b =>
                {
                    b.Navigation("Visits");
                });

            modelBuilder.Entity("PhanMemWebQuanLiBenhVien.Models.Models.PhongCapCuu", b =>
                {
                    b.Navigation("WorkSchedules");
                });

            modelBuilder.Entity("PhanMemWebQuanLiBenhVien.Models.Nurse", b =>
                {
                    b.Navigation("PatientList");
                });

            modelBuilder.Entity("PhanMemWebQuanLiBenhVien.Models.Patient", b =>
                {
                    b.Navigation("MedicalRecords");
                });

            modelBuilder.Entity("PhanMemWebQuanLiBenhVien.Models.PhongBenh", b =>
                {
                    b.Navigation("Patients");
                });

            modelBuilder.Entity("PhanMemWebQuanLiBenhVien.Models.PhongKham", b =>
                {
                    b.Navigation("Patients");

                    b.Navigation("WorkSchedules");
                });

            modelBuilder.Entity("PhanMemWebQuanLiBenhVien.Models.Profession", b =>
                {
                    b.Navigation("DoctorList");

                    b.Navigation("PhongBenhList");

                    b.Navigation("PhongKhamList");
                });
#pragma warning restore 612, 618
        }
    }
}
