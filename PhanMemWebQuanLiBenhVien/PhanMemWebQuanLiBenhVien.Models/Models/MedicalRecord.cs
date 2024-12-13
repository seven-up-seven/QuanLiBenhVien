using Microsoft.Identity.Client;
using PhanMemWebQuanLiBenhVien.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static PhanMemWebQuanLiBenhVien.Ultilities.Utilities;


namespace PhanMemWebQuanLiBenhVien.Models
{
	public class MedicalRecord
	{
		[Key]
		public int MedicalRecordId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập ID bệnh nhân!")]
        public int PatientId { get; set; }
		[ForeignKey("PatientId")]
		public Patient? Patient { get; set; }

		// Thông tin cố định
		[Required(ErrorMessage ="Vui lòng nhập tên bệnh nhân!")]
		public string PatientName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập giới tính bệnh nhân!")]
        public string PatientGender { get; set; }
		public string BHYT { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ bệnh nhân!")]
        public string Address { get; set; }

        // Tiền sử (thường bổ sung thông tin, không ghi đè)
        [Required(ErrorMessage = "Vui lòng nhập tiền sử bệnh nhân!")]
        public string TienSuBenhAn { get; set; }

		public ETrangThaiDieuTri? TrangThaiDieuTri { get; set; }
		public ETrangThaiBenhAn? TrangThaiBenhAn { get; set; }

		public ETinhTrangBenhNhan? TinhTrangBenhNhan { get; set; }

		public int? ProfesisonId { get; set; }
        [ForeignKey("ProfesisonId")]
        public Profession? Profession { get; set; }

        // Liên kết với các lần khám
        public ICollection<MedicalVisit>? Visits { get; set; }

		public int? DoctorId { get; set; }
		[ForeignKey("DoctorId")]
		public Doctor? Doctor { get; set; }
		public int? PhongKhamId { get; set; }
		[ForeignKey("PhongKhamId")]
		public PhongKham? PhongKham { get; set; }
		public int? PhongBenhId { get; set; }
		[ForeignKey("PhongBenhId")]
		public PhongBenh? PhongBenh { get; set; }

		public int? NurseId { get; set; }
		[ForeignKey("NurseId")]
		public Nurse? Nurse { get; set; }

	}

}
