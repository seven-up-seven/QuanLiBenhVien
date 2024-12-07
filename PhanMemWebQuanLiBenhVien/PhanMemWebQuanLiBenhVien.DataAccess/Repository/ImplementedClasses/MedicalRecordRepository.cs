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
                existingRecord.PatientId = medicalRecord.PatientId;
                existingRecord.PatientName = medicalRecord.PatientName;
                existingRecord.PatientGender = medicalRecord.PatientGender;
                existingRecord.BHYT = medicalRecord.BHYT;
                existingRecord.Address = medicalRecord.Address;
                existingRecord.TienSuBenhAn = medicalRecord.TienSuBenhAn;
                existingRecord.TrangThaiDieuTri = medicalRecord.TrangThaiDieuTri;
                existingRecord.TrangThaiBenhAn = medicalRecord.TrangThaiBenhAn;
                existingRecord.DoctorId = medicalRecord.DoctorId;
                existingRecord.PhongKhamId = medicalRecord.PhongKhamId;
                existingRecord.PhongBenhId = medicalRecord.PhongBenhId;
                existingRecord.NurseId = medicalRecord.NurseId;
                existingRecord.TinhTrangBenhNhan = medicalRecord.TinhTrangBenhNhan; 
                _db.SaveChanges();
            }
        }
    }
}
