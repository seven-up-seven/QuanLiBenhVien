using System.ComponentModel.DataAnnotations;

namespace PhanMemWebQuanLiBenhVien.Models
{
    public class Profession
    {
        [Key]
        public int ProfessionId { get; set; }
        [Required(ErrorMessage ="Tên chuyên khoa không được bỏ trống")]
        public string ProfessionName { get; set; }
        public string Description {  get; set; }
        public int? TruongKhoaId { get; set; } = null;
        public List<Doctor>? DoctorList { get; set; }
    }
}
