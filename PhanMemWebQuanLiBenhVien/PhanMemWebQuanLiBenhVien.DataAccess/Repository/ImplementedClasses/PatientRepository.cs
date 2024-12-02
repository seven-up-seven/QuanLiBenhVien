using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemWebQuanLiBenhVien.DataAccess.Repository.ImplementedClasses
{
    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        private ApplicationDbContext _db;
        public PatientRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Patient patient)
        {
            Patient oldobj=_db.patients.FirstOrDefault(u=>u.PatientId==patient.PatientId);
            if (patient.Name!=null) oldobj.Name=patient.Name;
            if (patient.CCCD!=null) oldobj.CCCD=patient.CCCD;
            if (patient.Gender!=null) oldobj.Gender=patient.Gender;
            if (patient.Age!=null) oldobj.Age=patient.Age;
            if (!string.IsNullOrEmpty(patient.Address)) oldobj.Address=patient.Address;
            if (!string.IsNullOrEmpty(patient.PhoneNumber)) oldobj.PhoneNumber=patient.PhoneNumber;
            if (patient.TrangThaiDieuTri!=null) oldobj.TrangThaiDieuTri=patient.TrangThaiDieuTri;
            if (patient.DoctorId != null) oldobj.DoctorId=patient.DoctorId;
            if (patient.NurseId != null) oldobj.NurseId=patient.NurseId;
            if (patient.PhongBenhId != null) oldobj.PhongBenhId=patient.PhongBenhId;
            if (patient.PhongKhamId != null) oldobj.PhongKhamId=patient.PhongKhamId;
            if (patient.TrangThaiDieuTri == Ultilities.Utilities.ETrangThaiDieuTri.xuatvien)
            {
                if (oldobj.PhongKhamId != null) oldobj.PhongKhamId = null;
                if (oldobj.PhongBenhId != null) oldobj.PhongBenhId = null;
                if (oldobj.DoctorId != null) oldobj.DoctorId = null;
                if (oldobj.NurseId != null) oldobj.NurseId = null;
            }
            if(patient.TrangThaiDieuTri == Ultilities.Utilities.ETrangThaiDieuTri.nhapvien)
			{
				if (oldobj.PhongKhamId != null) oldobj.PhongKhamId = null;
				if (oldobj.PhongBenhId != null) oldobj.PhongBenhId = null;
			}
			_db.Update(oldobj);
        }
    }
}
