﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static PhanMemWebQuanLiBenhVien.Ultilities.Utilities;

namespace PhanMemWebQuanLiBenhVien.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }
        [Required]
        [RegularExpression(@"^\d+$", ErrorMessage = "Chỉ được nhập số !")]
        [MaxLength(12, ErrorMessage = "CCCD có 12 số !")]
        public string CCCD { get; set; }
        [Required(ErrorMessage = "Chưa nhập họ và tên !")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Giới tính là bắt buộc !")]
        public EGender Gender { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage ="Địa chỉ là bắt buộc !")]
        public string Address { get; set; }
        [Required(ErrorMessage = "SĐT là bắt buộc !")]
        [MaxLength(10, ErrorMessage = "SĐT có 10 số !")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Chỉ được nhập số !")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "BHYT là bắt buộc !")]

        public ETrangThaiBenhAn? TrangThaiBenhAn { get; set; }

        public int? ProfesisonId { get; set; }
        [ForeignKey("ProfesisonId")]
        public Profession? Profession { get; set; }
        //Khi benh nhan xuat vien thi cac gia tri sau duoc chuyen sang null

        //Foreign Keys
        //public int? DoctorId { get; set; }
        //[ForeignKey("DoctorId")]
        //public Doctor? Doctor { get; set; }
        //public int? PhongKhamId { get; set; }
        //[ForeignKey("PhongKhamId")]
        //public PhongKham? PhongKham { get; set; }
        //public int? PhongBenhId { get; set; }
        //[ForeignKey("PhongBenhId")]
        //public PhongBenh? PhongBenh { get; set; }

        //public int? NurseId { get; set; }
        //[ForeignKey("NurseId")]
        //public Nurse? Nurse { get; set; }

        public ICollection<MedicalRecord>? MedicalRecords { get; set; }
	}
}
