using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static PhanMemWebQuanLiBenhVien.Ultilities.Utilities;

namespace PhanMemWebQuanLiBenhVien.Models
{
    public class Mission
    {
        [Key]
        public int MissionId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập ngày bắt đầu.")]
        public DateTime Time { get; set; }
        [Required(ErrorMessage ="Vui long nhập mức đọ. ")]
        public Elever Lever { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập ngày kết thúc !")]
        public DateTime EndTime { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập nội dung !")]
        public string Content { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập ngày loại phòng !")]
        public EPhong RoomType { get; set; }

        public bool IsCompleted { get; set; } = false;



        //Foreign Keys
        public int? DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public Doctor? Doctor { get; set; }

        public int? PhongKhamId { get; set; }

        [ForeignKey("PhongKhamId")]
        public PhongKham? PhongKham { get; set; }
        public int? PhongBenhId { get; set; }

        [ForeignKey("PhongBenhId")]
        public PhongBenh? PhongBenh { get; set; }
    }
}
