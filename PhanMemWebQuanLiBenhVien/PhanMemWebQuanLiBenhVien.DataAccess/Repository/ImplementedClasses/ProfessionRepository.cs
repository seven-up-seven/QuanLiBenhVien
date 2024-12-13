using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemWebQuanLiBenhVien.DataAccess.Repository.ImplementedClasses
{
    public class ProfessionRepository : Repository<Profession>, IProfessionRepository
    {
        ApplicationDbContext _db;
        public ProfessionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Profession profession)
        {
            var objFromDb = _db.professions.FirstOrDefault(p => p.ProfessionId == profession.ProfessionId);
            if (objFromDb != null)
            {
                if (profession.ProfessionName != null) objFromDb.ProfessionName = profession.ProfessionName;
                if (profession.Description != null) objFromDb.Description = profession.Description;
                 objFromDb.TruongKhoaId = profession.TruongKhoaId;
                 objFromDb.TruongKhoaName = profession.TruongKhoaName;
                _db.SaveChanges();
            }
        }
    }
}
