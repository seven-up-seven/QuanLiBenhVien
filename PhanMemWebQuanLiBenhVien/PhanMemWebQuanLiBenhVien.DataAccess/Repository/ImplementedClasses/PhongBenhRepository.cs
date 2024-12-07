using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemWebQuanLiBenhVien.DataAccess.Repository.ImplementedClasses
{
    public class PhongBenhRepository : Repository<PhongBenh>, IPhongBenhRepository
    {
        ApplicationDbContext _db;
        public PhongBenhRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(PhongBenh phongBenh)
        {
            var objFromDb = _db.phongBenhs.FirstOrDefault(p => p.RoomId == phongBenh.RoomId);
            if (objFromDb != null)
            {
                objFromDb.Name = phongBenh.Name;
                objFromDb.ProfessionId = phongBenh.ProfessionId;    
                _db.SaveChanges();
            }
        }
    }
}
