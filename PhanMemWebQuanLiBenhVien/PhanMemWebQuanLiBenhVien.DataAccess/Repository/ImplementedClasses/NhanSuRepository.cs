using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemWebQuanLiBenhVien.DataAccess.Repository.ImplementedClasses
{
    public class NhanSuRepository : Repository<NhanSu>, INhanSuRepository
    {
        private ApplicationDbContext _db;
        public NhanSuRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(NhanSu nhansu)
        {
            var oldobj = _db.nhansus.FirstOrDefault(u=>u.NhanSuId == nhansu.NhanSuId);
            if (oldobj!=null)
            {
                if (nhansu.NhanSuName!=null) oldobj.NhanSuName=nhansu.NhanSuName;
                if (nhansu.NhanSuAge!=null) oldobj.NhanSuAge=nhansu.NhanSuAge;
                if (nhansu.NhanSuGender!=null) oldobj.NhanSuGender=nhansu.NhanSuGender;
                if (nhansu.Address!=null) oldobj.Address=nhansu.Address;
                if (nhansu.UserName != null) oldobj.UserName = nhansu.UserName;
                if (!string.IsNullOrEmpty(nhansu.ImgUrl)) oldobj.ImgUrl=nhansu.ImgUrl;
                oldobj.HasAccount = nhansu.HasAccount;
                oldobj.Role = nhansu.Role;
                _db.Update(oldobj);
                _db.SaveChanges();
            }
        }
    }
}
