using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhanMemWebQuanLiBenhVien.Models
{
    public class WorkSchedule
    {
        [Key]
        public int WorkScheduleId { get; set; }


        //Foreign key
        public int? DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public Doctor? Doctor { get; set; }

        public int? NurseId { get; set; }
        [ForeignKey("NurseId")]
        public Nurse? Nurse { get; set; }
    }
    public class Shift
    {
        public int Index { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }

        public void SetTime()
        {
            if(Index == 1)
            {
                StartTime = 7;
                EndTime = 13; 
            }
            if(Index == 2)
            {
                StartTime = 13;
                EndTime = 19;
            }
            if (Index == 3)
            {
                StartTime = 19;
                EndTime = 24;
            }
            if(Index == 4) // TRỰC ĐÊM 
            {
                StartTime = 0;
                EndTime = 7;
            }
        }
    }
}
