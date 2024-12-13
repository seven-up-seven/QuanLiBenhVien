
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using static PhanMemWebQuanLiBenhVien.Ultilities.Utilities;

namespace PhanMemWebQuanLiBenhVien.Models
{
    public class Nurse
    {
        [Key]
        public int NurseId { get; set; }
		[MaxLength(12, ErrorMessage = "CCCD có 12 số!")]
        [Required(ErrorMessage = "Vui lòng nhập CCCD!")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Chỉ được nhập số!")]
		public string NurseCCCD { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập họ tên!")]
        public string NurseName { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn giới tính!")]
        public EGender NurseGender { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tuổi!")]
        [Range(24, int.MaxValue, ErrorMessage = "Độ tuổi phải lớn hơn hoặc bằng 24")]
		public int NurseAge { get; set; }
		public string? NurseImgURL { get; set; }
		public bool HasAccount { get; set; } = false;
		public string? Username { get; set; } = null;

		//Foreign Keys
		public ICollection<Patient>? PatientList { get; set; } 
    }
}
