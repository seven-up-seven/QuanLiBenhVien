using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static PhanMemWebQuanLiBenhVien.Ultilities.Utilities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemWebQuanLiBenhVien.Models.Models
{
    public class ActivityHistory
    {
        [Key]
        public int Id { get; set; }
        [ValidateNever]
        public int? MemberId { get; set; }
        [ValidateNever]
        public string? MemberName { get; set; }
        [ValidateNever]
        public int? ObjectId { get; set; }
        [ValidateNever]
        public string? Activity {  get; set; }
        [ValidateNever]
        public ERole MemberRole { get; set; }
        [ValidateNever]
        public DateTime ActivityTime { get; set; }
        [ValidateNever]
        public string? UpdateDetails { get; set; } = null;
    }
}
