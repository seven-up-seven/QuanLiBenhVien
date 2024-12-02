using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemWebQuanLiBenhVien.DataAccess.Repository.ImplementedClasses
{
	public class MedicalVisitRepository : Repository<MedicalVisit>, IMedicalVisitRepository
	{
		private ApplicationDbContext _db;
		public MedicalVisitRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public void Update(MedicalVisit medicalVisit)
		{
			throw new NotImplementedException();
		}
	}
}
