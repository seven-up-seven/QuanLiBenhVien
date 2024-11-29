using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhanMemWebQuanLiBenhVien.Models
{
    public class Mission
    {
        [Key]
        public int MissionId { get; set; }
        [Required]
        public DateTime Time { get; set; }
        public string Content { get; set; }
        //Foreign Keys
        public string DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; }
    }
}
