using PhanMemWebQuanLiBenhVien.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces
{
    public interface IChamCongRepository : IRepository<ChamCong>
    {
        public void Update(ChamCong chamCong);
    }
}
