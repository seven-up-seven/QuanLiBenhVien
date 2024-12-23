using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;
using PhanMemWebQuanLiBenhVien.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemWebQuanLiBenhVien.DataAccess.Repository.ImplementedClasses
{
    public class ChamCongRepository : Repository<ChamCong>, IChamCongRepository
    {
        private readonly ApplicationDbContext _db;
        public ChamCongRepository(ApplicationDbContext db) : base(db)
        {
            db = _db; 
        }

        public void Update(ChamCong chamCong)
        {
            var cc = _db.chamCongs.FirstOrDefault(m => m.Id == chamCong.Id);
            
            _db.chamCongs.Update(cc);
        }
    }
}
