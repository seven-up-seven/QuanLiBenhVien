using System.ComponentModel.DataAnnotations;

namespace PhanMemWebQuanLiBenhVien.Models
{
    public class Profession
    {
        [Key]
        public int ProfessionId { get; set; }
        [Required(ErrorMessage ="Tên chuyên khoa không được bỏ trống")]
        public string ProfessionName { get; set; }
        //nen them mo ta chi tiet chuyen khoa
        //them danh sach bac si 
    }
}
