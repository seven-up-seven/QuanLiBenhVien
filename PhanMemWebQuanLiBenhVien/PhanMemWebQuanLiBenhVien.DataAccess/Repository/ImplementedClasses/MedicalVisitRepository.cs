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
            var objFromDb = _db.medicalVisits.FirstOrDefault(m => m.VisitId == medicalVisit.VisitId);
            if (objFromDb != null)
            {
                objFromDb.MedicalRecordId = medicalVisit.MedicalRecordId;
                objFromDb.VisitDate = medicalVisit.VisitDate;
                objFromDb.Symptom = medicalVisit.Symptom;
                objFromDb.KetQuaLamSang = medicalVisit.KetQuaLamSang;
                objFromDb.ChanDoan = medicalVisit.ChanDoan;
                objFromDb.TinhTrangBenhNhan = medicalVisit.TinhTrangBenhNhan;
                _db.SaveChanges();
            }
        }
	}
}
