using Microsoft.Identity.Client;
using PhanMemWebQuanLiBenhVien.Models.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhanMemWebQuanLiBenhVien.Models
{
    public class WorkSchedule
    {
        [Key]
        public int WorkScheduleId { get; set; }

        public string? DayOfWeek { get; set; }


        //Foreign key
        public int? DoctorId1 { get; set; }
        [ForeignKey("DoctorId1")]
        public Doctor? Doctor1 { get; set; }
        public int? DoctorId2 { get; set; }
        [ForeignKey("DoctorId2")]
        public Doctor? Doctor2 { get; set; }
        public int? DoctorId3 { get; set; }
        [ForeignKey("DoctorId3")]
        public Doctor? Doctor3 { get; set; }






        public int? NurseId { get; set; }
        [ForeignKey("NurseId")]
        public Nurse? Nurse { get; set; }
        public int? PhongKhamId { get; set; }
        public PhongKham? PhongKham { get; set; }
        public int? PhongCapCuuId { get; set; }
        public PhongCapCuu? PhongCapCuu { get; set; }
    }
}