using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static PhanMemWebQuanLiBenhVien.Ultilities.Utilities;

namespace PhanMemWebQuanLiBenhVien.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập CCCD!")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Chỉ được nhập số!")]
        [MaxLength(12, ErrorMessage = "CCCD có 12 số!")]
        public string CCCD { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập họ tên!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn giới tính!")]
        public EGender Gender { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ngày tháng năm sinh!")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ!")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập SĐT!")]
        [MaxLength(10, ErrorMessage = "SĐT có 10 số!")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Chỉ được nhập số!")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập BHYT!")]

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
