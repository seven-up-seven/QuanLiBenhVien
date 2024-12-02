using Microsoft.Identity.Client;
using PhanMemWebQuanLiBenhVien.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhanMemWebQuanLiBenhVien.Models
{
	public class MedicalRecord
	{
		[Key]
		public int MedicalRecordId { get; set; }

		public int PatientId { get; set; }
		[ForeignKey("PatientId")]
		public Patient? Patient { get; set; }

		// Thông tin cố định
		public string PatientName { get; set; }
		public string PatientGender { get; set; }
		public string BHYT { get; set; }
		public string Address { get; set; }

		// Tiền sử (thường bổ sung thông tin, không ghi đè)
		public string TienSuBenhAn { get; set; }

		// Liên kết với các lần khám
		public ICollection<MedicalVisit>? Visits { get; set; } 
	}

}
