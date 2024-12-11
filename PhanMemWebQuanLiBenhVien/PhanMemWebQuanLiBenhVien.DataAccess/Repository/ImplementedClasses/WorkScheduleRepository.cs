using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemWebQuanLiBenhVien.DataAccess.Repository.ImplementedClasses
{
    public class WorkScheduleRepository : Repository<WorkSchedule>, IWorkScheduleRepository
    {
        ApplicationDbContext _db;
        public WorkScheduleRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(WorkSchedule workSchedule)
        {
            var oldobj = _db.workSchedules.FirstOrDefault(u => u.WorkScheduleId == workSchedule.WorkScheduleId);
            if (workSchedule.DoctorId1 != null)
            {
                oldobj.DoctorId1 = workSchedule.DoctorId1; 
            }
            if (workSchedule.DoctorId2 != null)
            {
                oldobj.DoctorId2 = workSchedule.DoctorId2;
            }
            if (workSchedule.DoctorId3 != null)
            {
                oldobj.DoctorId3 = workSchedule.DoctorId3;
            }
            _db.Update(oldobj);
            _db.SaveChanges();
        }
    }
}
