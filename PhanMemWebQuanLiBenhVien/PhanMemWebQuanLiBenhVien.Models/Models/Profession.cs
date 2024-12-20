﻿using System.ComponentModel.DataAnnotations;

namespace PhanMemWebQuanLiBenhVien.Models
{
    public class Profession
    {
        [Key]
        public int ProfessionId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên chuyên khoa!")]
        public string ProfessionName { get; set; }
        public string Description {  get; set; }
        public int? TruongKhoaId { get; set; } = null;
        public string? TruongKhoaName { get; set; } = null;
        public List<Doctor>? DoctorList { get; set; }

        public List<PhongBenh>? PhongBenhList { get; set; }

        public List<PhongKham>? PhongKhamList { get; set; }
    }
}
