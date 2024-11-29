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
            throw new NotImplementedException();
        }
    }
}
