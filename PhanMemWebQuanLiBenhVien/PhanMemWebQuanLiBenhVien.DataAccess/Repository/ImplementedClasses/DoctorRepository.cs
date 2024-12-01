using Microsoft.EntityFrameworkCore;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemWebQuanLiBenhVien.DataAccess.Repository.ImplementedClasses
{
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        private ApplicationDbContext _db;
        private DbSet<Doctor> _dbSet;
        public DoctorRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Doctor doctor)
        {
            var oldobj = _db.doctors.FirstOrDefault(u => u.DoctorId == doctor.DoctorId);
            if (oldobj != null)
            {
                oldobj.DoctorName = doctor.DoctorName;
                oldobj.DoctorGender = doctor.DoctorGender;
                oldobj.DoctorAge = doctor.DoctorAge;
                oldobj.DoctorCCCD = doctor.DoctorCCCD;
                oldobj.HasAccount = doctor.HasAccount;
                oldobj.Username = doctor.Username;
                oldobj.ProfessionId = doctor.ProfessionId;
                if (doctor.DoctorImgURL != null) oldobj.DoctorImgURL = doctor.DoctorImgURL;
                _db.doctors.Update(oldobj);
            }
        }
    }
}
