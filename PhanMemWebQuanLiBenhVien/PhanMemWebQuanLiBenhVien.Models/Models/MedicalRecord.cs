using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhanMemWebQuanLiBenhVien.Models
{
    public class MedicalRecord
    {
        [Key]
        public int MedicalRecordId { get; set; }
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
        public string Prediction { get; set; }
        public DateTime WentToHospitalDay { get; set; }
    }
}
