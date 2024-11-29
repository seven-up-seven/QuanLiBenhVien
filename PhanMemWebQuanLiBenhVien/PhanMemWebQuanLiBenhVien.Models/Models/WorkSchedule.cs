using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhanMemWebQuanLiBenhVien.Models
{
    public class WorkSchedule
    {
        [Key]
        public int WorkScheduleId { get; set; }

        public string? RoomOfMonday { get; set; }
        public string? RoomOfTuesday { get; set; }
        public string? RoomOfWednesday { get; set; }
        public string? RoomOfThurday { get; set; }
        public string? RoomOfFriday { get; set; }
        public string? RoomOfSaturday { get; set; }
        public string? RoomOfSunday { get; set; }
    }
}
