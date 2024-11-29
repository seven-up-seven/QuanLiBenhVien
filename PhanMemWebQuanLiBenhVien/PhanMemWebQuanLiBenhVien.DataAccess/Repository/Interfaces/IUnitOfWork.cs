using PhanMemWebQuanLiBenhVien.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        ApplicationDbContext db_ { get; }
        
        public void Save();
		public Task SaveAsync();
	}
}
