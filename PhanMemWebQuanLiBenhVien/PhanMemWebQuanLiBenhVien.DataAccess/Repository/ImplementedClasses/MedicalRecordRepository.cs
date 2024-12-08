using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemWebQuanLiBenhVien.DataAccess.Repository.ImplementedClasses
{
    public class MedicalRecordRepository : Repository<MedicalRecord>, IMedicalRecordRepository
    {
        private ApplicationDbContext _db;
        public MedicalRecordRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(MedicalRecord medicalRecord)
        {
            var existingRecord = _db.medicalRecords.FirstOrDefault(m => m.MedicalRecordId == medicalRecord.MedicalRecordId);
            if (existingRecord != null)
            {
                /*if (medicalRecord.PatientId != null) existingRecord.PatientId = medicalRecord.PatientId;*/
                if (medicalRecord.PatientName != null) existingRecord.PatientName = medicalRecord.PatientName;
                if (medicalRecord.PatientGender != null) existingRecord.PatientGender = medicalRecord.PatientGender;
                if (medicalRecord.BHYT != null) existingRecord.BHYT = medicalRecord.BHYT;
                if (medicalRecord.Address != null) existingRecord.Address = medicalRecord.Address;
                if (medicalRecord.TienSuBenhAn != null) existingRecord.TienSuBenhAn = medicalRecord.TienSuBenhAn;
                if (medicalRecord.TrangThaiDieuTri != null) existingRecord.TrangThaiDieuTri = medicalRecord.TrangThaiDieuTri;
                if (medicalRecord.TrangThaiBenhAn != null) existingRecord.TrangThaiBenhAn = medicalRecord.TrangThaiBenhAn;
                if (medicalRecord.DoctorId != null) existingRecord.DoctorId = medicalRecord.DoctorId;
                if (medicalRecord.PhongKhamId != null) existingRecord.PhongKhamId = medicalRecord.PhongKhamId;
                if (medicalRecord.PhongBenhId != null) existingRecord.PhongBenhId = medicalRecord.PhongBenhId;
                if (medicalRecord.NurseId != null) existingRecord.NurseId = medicalRecord.NurseId;
                if (medicalRecord.TinhTrangBenhNhan != null) existingRecord.TinhTrangBenhNhan = medicalRecord.TinhTrangBenhNhan;
                existingRecord.ProfesisonId = medicalRecord.ProfesisonId;
                _db.SaveChanges();
            }
        }
    }
}
