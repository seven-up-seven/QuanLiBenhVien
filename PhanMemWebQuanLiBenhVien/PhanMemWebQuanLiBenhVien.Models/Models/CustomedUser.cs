using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static PhanMemWebQuanLiBenhVien.Ultilities.Utilities;

namespace PhanMemWebQuanLiBenhVien.Models
{
    public class CustomedUser: IdentityUser
    {
        [MaxLength(12, ErrorMessage = "CCCD có 12 số !")]
        [Required(ErrorMessage = "Chưa nhập CCCD !")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Chỉ được nhập số !")]
        public string CCCD { get; set; }
        [Required(ErrorMessage = "Chưa nhập họ và tên !")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Giới tính là bắt buộc.")]
        public EGender Gender { get; set; }
        [Required(ErrorMessage = "Chưa nhập tuổi !")]
        [Range(24, int.MaxValue, ErrorMessage = "Độ tuổi phải lớn hơn hoặc bằng 24")]
        public int Age { get; set; }
    }
}
