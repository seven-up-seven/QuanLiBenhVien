using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;
using static PhanMemWebQuanLiBenhVien.Ultilities.Utilities;

namespace PhanMemWebQuanLiBenhVien.Controllers
{
    public class ActivityHistoryController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private UserManager<IdentityUser> _userManager;
        public ActivityHistoryController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int? ExecutorID=null)
        {
            ViewBag.ListRole = new SelectList(new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Quản lí nhân sự",
                    Value = ERole.quanlinhansu.ToString(),
                },
                new SelectListItem
                {
                    Text = "Quản lí vật tư",
                    Value = ERole.quanlivattu.ToString(),
                },
                new SelectListItem
                {
                    Text = "Quản lí bệnh nhân",
                    Value = ERole.quanlibenhnhan.ToString(),
                },
                new SelectListItem
                {
                    Text="Bác sĩ",
                    Value=ERole.doctor.ToString(),
                },
                new SelectListItem
                {
                    Text="Y tá",
                    Value=ERole.nurse.ToString(),
                },
                new SelectListItem
                {
                    Text="Admin",
                    Value=ERole.admin.ToString(),
                }
            }, "Value", "Text");
            var listactivity=_unitOfWork.ActivityHistoryRepository.GetAll();
            if (User.IsInRole("Doctor"))
            {
                var user = await _userManager.GetUserAsync(User);
                var true_user = (CustomedUser)user;
                listactivity = listactivity.Where(u => u.MemberRole == Ultilities.Utilities.ERole.doctor && u.MemberId==true_user.UserId);
            }
            if (User.IsInRole("Nurse"))
            {
                var user = await _userManager.GetUserAsync(User);
                var true_user = (CustomedUser)user;
                listactivity = listactivity.Where(u => u.MemberRole == Ultilities.Utilities.ERole.nurse && u.MemberId == true_user.UserId);
            }
            if (User.IsInRole("QuanLiVatTu"))
            {
                var user = await _userManager.GetUserAsync(User);
                var true_user = (CustomedUser)user;
                listactivity = listactivity.Where(u => u.MemberRole == Ultilities.Utilities.ERole.quanlivattu && u.MemberId == true_user.UserId);
            }
            if (User.IsInRole("QuanLiBenhNhan"))
            {
                var user = await _userManager.GetUserAsync(User);
                var true_user = (CustomedUser)user;
                listactivity = listactivity.Where(u => u.MemberRole == Ultilities.Utilities.ERole.quanlibenhnhan && u.MemberId == true_user.UserId);
            }
            if (ExecutorID!=null)
            {
                listactivity=listactivity.Where(u=>u.MemberId==ExecutorID);
            }
            return View(listactivity.OrderByDescending(u=>u.ActivityTime).ToList());
        }
        [HttpPost]
        public async Task<IActionResult> Index(string SearchRole, int SearchExecutorID)
        {
            ViewBag.ListRole = new SelectList(new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Quản lí nhân sự",
                    Value = ERole.quanlinhansu.ToString(),
                },
                new SelectListItem
                {
                    Text = "Quản lí vật tư",
                    Value = ERole.quanlivattu.ToString(),
                },
                new SelectListItem
                {
                    Text = "Quản lí bệnh nhân",
                    Value = ERole.quanlibenhnhan.ToString(),
                },
                new SelectListItem
                {
                    Text="Bác sĩ",
                    Value=ERole.doctor.ToString(),
                },
                new SelectListItem
                {
                    Text="Y tá",
                    Value=ERole.nurse.ToString(),
                },
                new SelectListItem
                {
                    Text="Admin",
                    Value=ERole.admin.ToString(),
                }
            }, "Value", "Text");
            var listactivity = _unitOfWork.ActivityHistoryRepository.GetAll();
            /*if (User.IsInRole("Doctor"))
            {
                var user = await _userManager.GetUserAsync(User);
                var true_user = (CustomedUser)user;
                listactivity = listactivity.Where(u => u.MemberRole == Ultilities.Utilities.ERole.doctor && u.MemberId == true_user.UserId);
            }
            if (User.IsInRole("Nurse"))
            {
                var user = await _userManager.GetUserAsync(User);
                var true_user = (CustomedUser)user;
                listactivity = listactivity.Where(u => u.MemberRole == Ultilities.Utilities.ERole.nurse && u.MemberId == true_user.UserId);
            }
            if (User.IsInRole("QuanLiVatTu"))
            {
                var user = await _userManager.GetUserAsync(User);
                var true_user = (CustomedUser)user;
                listactivity = listactivity.Where(u => u.MemberRole == Ultilities.Utilities.ERole.quanlivattu && u.MemberId == true_user.UserId);
            }
            if (User.IsInRole("QuanLiBenhNhan"))
            {
                var user = await _userManager.GetUserAsync(User);
                var true_user = (CustomedUser)user;
                listactivity = listactivity.Where(u => u.MemberRole == Ultilities.Utilities.ERole.quanlibenhnhan && u.MemberId == true_user.UserId);
            }*/
            if (SearchExecutorID!=-1)
            {
                listactivity = listactivity.Where(u => u.MemberId == SearchExecutorID);
            }
            if (SearchRole!="NoFilter")
            {
                listactivity = listactivity.Where(u => u.MemberRole.ToString() == SearchRole);
            }
            return View(listactivity.OrderByDescending(u => u.ActivityTime).ToList());
        }
    }
}
