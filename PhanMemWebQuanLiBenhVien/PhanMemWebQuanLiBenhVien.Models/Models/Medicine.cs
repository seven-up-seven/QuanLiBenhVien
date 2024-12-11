using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemWebQuanLiBenhVien.Models.Models
{
    public class Medicine
    {
        [Key]
        public int MedicineId { get; set; }

        public string Name{ get; set; }
        public string Usage { get; set; }
        public string Unit { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
