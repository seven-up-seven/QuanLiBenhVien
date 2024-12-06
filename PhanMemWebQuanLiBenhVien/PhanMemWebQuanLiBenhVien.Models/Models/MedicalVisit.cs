using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PhanMemWebQuanLiBenhVien.Ultilities.Utilities;

namespace PhanMemWebQuanLiBenhVien.Models
{
	public class MedicalVisit
	{
		[Key]
		public int VisitId { get; set; }

		public int MedicalRecordId { get; set; }
		[ForeignKey("MedicalRecordId")]
		public MedicalRecord? MedicalRecord { get; set; }

		public DateTime VisitDate { get; set; } // Ngày khám
		public string Symptom { get; set; } // Triệu chứng
		public string KetQuaLamSang { get; set; } // Kết quả lâm sàng
		public string ChanDoan { get; set; } // Chẩn đoán

		public ETinhTrangBenhNhan? TinhTrangBenhNhan { get; set; }
    }
}
