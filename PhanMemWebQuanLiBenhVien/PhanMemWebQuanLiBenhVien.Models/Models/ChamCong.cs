using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemWebQuanLiBenhVien.Models.Models
{
    public class ChamCong
    {
        [Key]
        public int Id { get; set; }

        public int? DoctorId { get; set; }

        public int? NurseId { get; set; }

        public int? NhanSuId { get; set; }
         
        public DateTime Time { get; set; } = DateTime.Now;

    }
}
