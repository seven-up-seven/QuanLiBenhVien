
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PhanMemWebQuanLiBenhVien.Models
{
    public class Nurse
    {
        [Key]
        public int NurseId { get; set; }

        public ICollection<Patient> PatientList { get; set; }
    }
}
