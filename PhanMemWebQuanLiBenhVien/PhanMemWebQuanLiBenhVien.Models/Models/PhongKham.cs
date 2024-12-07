using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhanMemWebQuanLiBenhVien.Models
{
    public class PhongKham
    {
        [Key]
        public int RoomId { get; set; }

        [Required(ErrorMessage = "Tên phòng là bắt buộc !")]
        //So tang nam trong ten phong
        public string Name { get; set; }
        public ICollection<Patient>? Patients { get; set; }

        public int? ProfessionId { get; set; }
        [ForeignKey("ProfessionId")]
        public Profession? Profession { get; set; }
    }
}
