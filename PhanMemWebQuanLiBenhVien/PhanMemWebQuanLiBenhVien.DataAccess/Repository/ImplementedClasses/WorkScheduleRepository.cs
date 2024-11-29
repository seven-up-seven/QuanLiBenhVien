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
            throw new NotImplementedException();
        }
    }
}
