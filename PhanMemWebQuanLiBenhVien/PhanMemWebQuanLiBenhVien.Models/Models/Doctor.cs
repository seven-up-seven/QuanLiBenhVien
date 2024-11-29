using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PhanMemWebQuanLiBenhVien.Models
{
    public class Doctor
    {
        //Goi cac thuoc tinh con lai thong qua IdentityUser
        [Key]
        public int DoctorId { get; set; }
        //Foreign Keys
        public int ProfessionId { get; set; }
        [ForeignKey("ProfessionId")]
        public Profession Profession { get; set; }
        public ICollection<Patient> PatientList { get; set; }
        public int WorkScheduleId { get; set; }
        [ForeignKey("WorkScheduleId")]
        public WorkSchedule WorkSchedule { get; set; }
    }
}
