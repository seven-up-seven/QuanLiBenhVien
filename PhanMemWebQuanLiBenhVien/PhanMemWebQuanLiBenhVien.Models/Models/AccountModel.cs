using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhanMemWebQuanLiBenhVien.Models.Models
{
    public class AccountModel
    {
        [Required(ErrorMessage = "Tên đăng nhập không được bỏ trống.")]
        [StringLength(256, MinimumLength = 6, ErrorMessage = "Tên đăng nhập phải có từ 6 đến 256 ký tự.")]
        [EmailAddress(ErrorMessage = "Nhập đúng định dạng của email.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được bỏ trống.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Mật khẩu phải có ít nhất một chữ cái và một chữ số và một kí tự đặc biệt")]
        public string Password { get; set; }
    }
}
