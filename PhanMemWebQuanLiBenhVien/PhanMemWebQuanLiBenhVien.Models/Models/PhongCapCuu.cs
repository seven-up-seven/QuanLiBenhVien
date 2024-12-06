using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemWebQuanLiBenhVien.Models.Models
{
    public class PhongCapCuu
    {
        [Key]
        public int RoomId { get; set; }

        [Required(ErrorMessage = "Tên phòng là bắt buộc !")]
        //So tang nam trong ten phong
        public string Name { get; set; }

        public bool isAvailable { get; set; } = true; 

    }
}
