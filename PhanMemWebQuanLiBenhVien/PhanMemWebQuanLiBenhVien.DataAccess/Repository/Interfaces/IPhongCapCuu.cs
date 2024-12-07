using PhanMemWebQuanLiBenhVien.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces
{
    public interface IPhongCapCuu : IRepository<PhongCapCuu>
    {
        public void Update(PhongCapCuu phongCapCuu);
    }
}
