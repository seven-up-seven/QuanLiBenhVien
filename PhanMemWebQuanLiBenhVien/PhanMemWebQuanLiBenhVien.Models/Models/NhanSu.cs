using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PhanMemWebQuanLiBenhVien.Ultilities.Utilities;

namespace PhanMemWebQuanLiBenhVien.Models.Models
{
    public class NhanSu
    {
        [Key]
        public int NhanSuId { get; set; }
        [Required]
        public string? NhanSuName { get; set; }
        [Range(18, int.MaxValue)]
        [Required]
        public int? NhanSuAge { get; set; }
        [Required]
        public EGender? NhanSuGender { get; set; }
        [Required]
        public string? Address { get; set; }
        [ValidateNever]
        public string? ImgUrl { get; set; }
        [ValidateNever]
        public bool HasAccount { get; set; } = false;
        [ValidateNever]
        public string? UserName { get; set; }
        [ValidateNever]
        public string? Role { get; set; } 
    }
}
