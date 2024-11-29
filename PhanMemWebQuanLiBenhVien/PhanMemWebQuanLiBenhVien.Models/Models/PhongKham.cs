using System.ComponentModel.DataAnnotations;

namespace PhanMemWebQuanLiBenhVien.Models
{
    public class PhongKham
    {
        [Key]
        public int RoomId { get; set; }
        [Required(ErrorMessage = "Tên phòng là bắt buộc !")]
        //So tang nam trong ten phong
        public string Name { get; set; }
        public ICollection<Patient> Patients { get; set; }
    }
}
