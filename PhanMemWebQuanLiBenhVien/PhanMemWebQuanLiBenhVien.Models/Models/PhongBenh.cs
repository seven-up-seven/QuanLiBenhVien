using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhanMemWebQuanLiBenhVien.Models
{
    public class PhongBenh
    {
        [Key]
        public int RoomId { get; set; } 

        [Required(ErrorMessage ="Tên phòng là bắt buộc !")]
        //So tang nam trong ten phong
        public string Name { get; set; }

        [Required(ErrorMessage ="Số giường là bắt buộc !")]
        [Range(1, 10, ErrorMessage ="Một phòng có ít nhất 1 giường và tối đa 10 giường")]
        public int NumberOfBeds { get; set; }

        //foreign key 
        public ICollection<Patient>? Patients { get; set; }

        public int? ProfessionId { get; set; }
        [ForeignKey("ProfessionId")]
        public Profession? Profession { get; set; }
    }
}
