using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhanMemWebQuanLiBenhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemWebQuanLiBenhVien.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        
        public DbSet<CustomedUser> customedUsers { get; set; }
        public DbSet<Doctor> doctors { get; set; }
        public DbSet<MedicalRecord> medicalRecords { get; set; }
        public DbSet<Mission> missions { get; set; }
        public DbSet<Patient> patients { get; set; }
        public DbSet<Nurse> nurses { get; set; }
        public DbSet<PhongBenh> phongBenhs { get; set; }
        public DbSet<PhongKham> phongKhams { get; set; }
        public DbSet<Profession> professions { get; set; }
        public DbSet<WorkSchedule> workSchedules { get; set; }
    }
}
