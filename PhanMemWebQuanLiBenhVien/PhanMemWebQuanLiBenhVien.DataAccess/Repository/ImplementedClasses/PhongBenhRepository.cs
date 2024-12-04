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

            var existingPhongBenh = _db.phongBenhs.FirstOrDefault(pb => pb.RoomId == phongBenh.RoomId);

            if (existingPhongBenh != null)
            {
                existingPhongBenh.Name = phongBenh.Name;
                existingPhongBenh.NumberOfBeds = phongBenh.NumberOfBeds;

            }
        }
    }
}


        
   

