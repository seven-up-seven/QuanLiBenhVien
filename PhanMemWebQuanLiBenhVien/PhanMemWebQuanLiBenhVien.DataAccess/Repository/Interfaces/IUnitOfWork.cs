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
        IDoctorRepository DoctorRepository { get; }
        IPatientRepository PatientRepository { get; }
        IMedicalRecordRepository MedicalRecordRepository { get; }
        IMissionRepository MissionRepository { get; }
        INurseRepository NurseRepository { get; }
        IPhongBenhRepository PhongBenhRepository { get; }
        IPhongKhamRepository PhongKhamRepository { get; }
        IProfessionRepository ProfessionRepository { get; }
        IWorkScheduleRepository WorkScheduleRepository { get; }
        IMedicalVisitRepository MedicalVisitRepository { get; }
        IPhongCapCuu PhongCapCuuRepository { get; } 
        IMedicineRepository MedicineRepository { get; }
        public void Save();
		public Task SaveAsync();
	}
}
