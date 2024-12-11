using System.ComponentModel.DataAnnotations;
using PhanMemWebQuanLiBenhVien.Models;

namespace PhanMemWebQuanLiBenhVien.VadilationAttributed
{
    public class EndTimeGreaterThanTimeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (Mission)validationContext.ObjectInstance;
            if (model.EndTime < model.Time)
            {
                return new ValidationResult("Ngày kết thúc phải lớn hơn ngày bắt đầu!");
            }
            return ValidationResult.Success;
        }
    }
}
