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
                objFromDb.ProfessionName = profession.ProfessionName;
                objFromDb.Description = profession.Description;
                _db.SaveChanges();
            }
        }
    }
}
