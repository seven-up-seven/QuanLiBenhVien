using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemWebQuanLiBenhVien.DataAccess.Repository.ImplementedClasses
{
    public class PhongKhamRepository : Repository<PhongKham>, IPhongKhamRepository
    {
        ApplicationDbContext _db;
        public PhongKhamRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(PhongKham phongKham)
        {
            throw new NotImplementedException();
        }
    }
}
