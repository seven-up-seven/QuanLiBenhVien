using PhanMemWebQuanLiBenhVien.DataAccess;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.ImplementedClasses;
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
        public IDoctorRepository DoctorRepository { get; }
        public IPatientRepository PatientRepository { get; }
        public IMedicalRecordRepository MedicalRecordRepository { get; }
        public IMissionRepository MissionRepository { get; }
        public INurseRepository NurseRepository { get; }
        public IPhongBenhRepository PhongBenhRepository { get; }
        public IPhongKhamRepository PhongKhamRepository { get; }
        public IProfessionRepository ProfessionRepository { get; }
        public IWorkScheduleRepository WorkScheduleRepository { get; }
        public IMedicalVisitRepository MedicalVisitRepository { get; }
        public IPhongCapCuu PhongCapCuuRepository { get; }
        public IMedicineRepository MedicineRepository { get; }
        public ApplicationDbContext db_ { get { return _db; } }
        public INhanSuRepository NhanSuRepository { get; }
        
        public UnitOfWork(ApplicationDbContext db)                 
        {
            this._db = db;
            DoctorRepository = new DoctorRepository(_db);
            PatientRepository = new PatientRepository(_db);
            MedicalRecordRepository = new MedicalRecordRepository(_db);
            MissionRepository = new MissionRepository(_db);
            NurseRepository = new NurseRepository(_db);
            PhongBenhRepository = new PhongBenhRepository(_db);
            PhongKhamRepository = new PhongKhamRepository(_db);
            ProfessionRepository = new ProfessionRepository(_db);
            WorkScheduleRepository = new WorkScheduleRepository(_db);
            MedicalVisitRepository = new MedicalVisitRepository(_db);
            PhongCapCuuRepository = new PhongCapCuuRepository(_db);
            MedicineRepository = new MedicineRepository(_db);
            NhanSuRepository = new NhanSuRepository(_db);
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
