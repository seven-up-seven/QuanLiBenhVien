using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            throw new NotImplementedException();
        }
    }
}
