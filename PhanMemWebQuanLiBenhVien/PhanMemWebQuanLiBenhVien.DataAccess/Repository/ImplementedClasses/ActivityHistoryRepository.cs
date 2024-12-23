using Microsoft.EntityFrameworkCore;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemWebQuanLiBenhVien.DataAccess.Repository.ImplementedClasses
{
    public class ActivityHistoryRepository : Repository<ActivityHistory>, IActivityHistory
    {
        private ApplicationDbContext _db;
        private DbSet<ActivityHistory> _dbset;
        public ActivityHistoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
            _dbset=db.Set<ActivityHistory>();
        }
        public void Update(ActivityHistory obj)
        {
            _dbset.Update(obj);
            _db.SaveChanges();
        }
    }
}
