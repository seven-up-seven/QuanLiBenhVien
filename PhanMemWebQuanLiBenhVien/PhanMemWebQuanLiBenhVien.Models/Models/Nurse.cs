
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PhanMemWebQuanLiBenhVien.Models
{
    public class Nurse
    {
        //cac thuoc tinh khac goi thong qua IdentityUser
        [Key]
        public int NurseId { get; set; }
    }
}
