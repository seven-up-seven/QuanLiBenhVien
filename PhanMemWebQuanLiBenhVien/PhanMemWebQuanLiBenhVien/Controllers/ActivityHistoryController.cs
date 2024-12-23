using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;

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

        public async Task<IActionResult> Index()
        {
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
            return View(listactivity.ToList());
        }
    }
}
