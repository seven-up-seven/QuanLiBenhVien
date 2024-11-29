using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using static PhanMemWebQuanLiBenhVien.Ultilities.Utilities;

namespace PhanMemWebQuanLiBenhVien.Models
{
    public class CustomedUser: IdentityUser
    {
        [ValidateNever]
        public int UserId { get; set; }
        [ValidateNever]
        public ERole UserRole { get; set; }
    }
}
