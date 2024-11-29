using PhanMemWebQuanLiBenhVien.DataAccess;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemWebQuanLiBenhVien.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        ApplicationDbContext _db;
        public ApplicationDbContext db_ { get { return _db; } }
        
        public UnitOfWork(ApplicationDbContext db)
        {
            this._db = db;
            
        }

        public void Save()
        {
            _db.SaveChanges(); 
        }
		public async Task SaveAsync()
		{
			await _db.SaveChangesAsync();
		}
	}
}
