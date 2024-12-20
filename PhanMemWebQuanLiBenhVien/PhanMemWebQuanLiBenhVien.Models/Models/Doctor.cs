﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static PhanMemWebQuanLiBenhVien.Ultilities.Utilities;


namespace PhanMemWebQuanLiBenhVien.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }
		[MaxLength(12, ErrorMessage = "CCCD có 12 số !")]
        [MinLength(12, ErrorMessage ="CCCD có 12 số!")]
        [Required(ErrorMessage = "Vui lòng nhập CCCD!")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Chỉ được nhập số !")]
		public string DoctorCCCD { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập họ tên!")]
        public string DoctorName { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn giới tính!")]
        public EGender DoctorGender { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tuổi!")]
        [Range(24, int.MaxValue, ErrorMessage = "Độ tuổi phải lớn hơn hoặc bằng 24")]
		public int DoctorAge { get; set; }
		public string? DoctorImgURL { get; set; }
        public bool HasAccount { get; set; } = false;
        public string? Username { get; set; } = null;
        public bool IsTruongKhoa { get; set; } = false;
        //Foreign Keys
        public int ProfessionId { get; set; }
        [ForeignKey("ProfessionId")]
        public Profession? Profession { get; set; }

        public EViTriLamViec? ViTriLamViec { get; set; }

        public ICollection<Patient>? PatientList { get; set; }
        public ICollection<Mission>? MissionList { get; set; }
    }
}
