using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PhanMemWebQuanLiBenhVien.VadilationAttributed;
using static PhanMemWebQuanLiBenhVien.Ultilities.Utilities;

namespace PhanMemWebQuanLiBenhVien.Models
{
    public class Mission
    {
        [Key]
        public int MissionId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập ngày bắt đầu !")]
        public DateTime Time { get; set; }
        [Required(ErrorMessage ="Vui long nhập mức độ ! ")]
        public Elever Lever { get; set; }
   
        [Required(ErrorMessage = "Vui lòng nhập ngày kết thúc !")]
        [EndTimeGreaterThanTime]
        public DateTime EndTime { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập nội dung !")]

        public string Content { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập loại phòng !")]
        public EPhong RoomType { get; set; }

        public bool IsCompleted { get; set; } = false;



        //Foreign Keys
        [Required(ErrorMessage ="Vui Lòng chọn bác sĩ !")]
        public int DoctorId { get; set; }

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
