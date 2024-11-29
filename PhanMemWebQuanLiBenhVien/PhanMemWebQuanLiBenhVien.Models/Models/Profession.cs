using System.ComponentModel.DataAnnotations;

namespace PhanMemWebQuanLiBenhVien.Models
{
    public class Profession
    {
        [Key]
        public int ProfessionId { get; set; }
        [Required]
        public string ProfessionName { get; set; }
    }
}
