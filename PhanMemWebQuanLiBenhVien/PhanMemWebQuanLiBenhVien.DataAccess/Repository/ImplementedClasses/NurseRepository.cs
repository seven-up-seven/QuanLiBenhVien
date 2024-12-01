using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemWebQuanLiBenhVien.DataAccess.Repository.ImplementedClasses
{
    public class NurseRepository : Repository<Nurse>, INurseRepository
    {
        private ApplicationDbContext _db;
        public NurseRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Nurse nurse)
        {
            var oldobj = _db.nurses.FirstOrDefault(u => u.NurseId == nurse.NurseId);
            if (oldobj != null)
            {
                oldobj.NurseName = nurse.NurseName;
                oldobj.NurseGender = nurse.NurseGender;
                oldobj.NurseAge = nurse.NurseAge;
                oldobj.NurseCCCD = nurse.NurseCCCD;
                oldobj.HasAccount = nurse.HasAccount;
                oldobj.Username = nurse.Username;
                if (nurse.NurseImgURL != null) oldobj.NurseImgURL = nurse.NurseImgURL;
                _db.nurses.Update(oldobj);
            }
        }
    }
}
