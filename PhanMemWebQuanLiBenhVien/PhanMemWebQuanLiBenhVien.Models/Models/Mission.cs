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

        public ELever Lever { get; set; }

        public DateTime EndTime { get; set; }
        public string Content { get; set; }
        public EPhong RoomType { get; set; }

        public bool IsCompleted { get; set; } = false;



        //Foreign Keys
        public int DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; }

        public int? PhongKhamId { get; set; }

        [ForeignKey("PhongKhamId")]
        public PhongKham? PhongKham { get; set; }
        public int? PhongBenhId { get; set; }

        [ForeignKey("PhongBenhId")]
        public PhongBenh? PhongBenh { get; set; }
    }
}
