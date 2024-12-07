using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemWebQuanLiBenhVien.DataAccess.Repository.ImplementedClasses
{
    public class PhongCapCuuRepository : Repository<PhongCapCuu>, IPhongCapCuu
    {
        ApplicationDbContext _db;
        public PhongCapCuuRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(PhongCapCuu phongCapCuu)
        {
            var objFromDb = _db.phongCapCuus.FirstOrDefault(p => p.RoomId == phongCapCuu.RoomId);
            if (objFromDb != null)
            {
                objFromDb.Name = phongCapCuu.Name;
                objFromDb.isAvailable = phongCapCuu.isAvailable;
                _db.SaveChanges();
            }
        }
    }
}
