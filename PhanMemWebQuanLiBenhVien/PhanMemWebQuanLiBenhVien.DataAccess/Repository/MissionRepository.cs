using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemWebQuanLiBenhVien.DataAccess.Repository
{
    public class MissionRepository: Repository<Mission>, IMissionRepository
    {
        private ApplicationDbContext _db;
        public MissionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Mission mission)
        {
            throw new NotImplementedException();
        }
    }
}
