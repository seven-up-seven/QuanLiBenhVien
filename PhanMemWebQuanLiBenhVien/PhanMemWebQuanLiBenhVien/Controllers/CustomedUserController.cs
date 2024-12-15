using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata;
using PhanMemWebQuanLiBenhVien.DataAccess;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;
using PhanMemWebQuanLiBenhVien.Models.Models;
using static PhanMemWebQuanLiBenhVien.Ultilities.Utilities;

namespace PhanMemWebQuanLiBenhVien.Controllers
{
	public class CustomedUserController : Controller
	{
		private IUnitOfWork _unitofwork;
		private UserManager<IdentityUser> _usermanager;
		private ApplicationDbContext _db;
		public CustomedUserController(IUnitOfWork unitofwork, UserManager<IdentityUser> userManager, ApplicationDbContext db)
		{
			_unitofwork = unitofwork;
			_usermanager = userManager;
			_db= db;
		}
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult DoctorIndex()
		{
			var doctorlist = _unitofwork.DoctorRepository.GetAll();
            ViewBag.TrangThaiTaiKhoan = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = "Chưa có tài khoản",
                        Value = "NoAccount"
                    },
                    new SelectListItem
                    {
                        Text = "Đã có tài khoản",
                        Value = "HasAccount"
                    }
                },
                "Value",
                "Text"
            );
            return View(doctorlist);
		}
        [HttpPost]
        public IActionResult DoctorIndex(string SearchName, string TrangThaiTaiKhoan)
        {
            var doctorlist = _unitofwork.DoctorRepository.GetAll();
            if (!string.IsNullOrEmpty(SearchName)) doctorlist = doctorlist.Where(u => u.DoctorName.ToLower().Contains(SearchName.ToLower()));
            if (TrangThaiTaiKhoan!="NoFilter")
            {
                if (TrangThaiTaiKhoan == "NoAccount") doctorlist = doctorlist.Where(u => u.HasAccount == false);
                else if (TrangThaiTaiKhoan == "HasAccount") doctorlist = doctorlist.Where(u => u.HasAccount == true);
            }
            ViewBag.TrangThaiTaiKhoan = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = "Chưa có tài khoản",
                        Value = "NoAccount"
                    },
                    new SelectListItem
                    {
                        Text = "Đã có tài khoản",
                        Value = "HasAccount"
                    }
                },
                "Value",
                "Text"
            );
            return View(doctorlist);
        }
		public IActionResult AssignDoctorAccount(int DoctorId)
		{
            ViewBag.Doctor = _unitofwork.DoctorRepository.Get(u => u.DoctorId == DoctorId);
			return View();
		}
		[HttpPost]
		[ActionName("AssignDoctorAccount")]
		public async Task<IActionResult> AssignDoctorAccountPost(int DoctorId, AccountModel account)
		{
			if (!ModelState.IsValid)
			{
                ViewBag.Doctor=_unitofwork.DoctorRepository.Get(u=>u.DoctorId==DoctorId);
                return View();
            }
			else
            {
                var true_user = new CustomedUser();
                true_user.UserId = DoctorId;
                true_user.UserName = account.UserName;
                true_user.UserRole = Ultilities.Utilities.ERole.doctor;
                var result=_usermanager.CreateAsync(true_user, account.Password).GetAwaiter().GetResult();
                if (!result.Succeeded)
                {
                    TempData["error"] = "Tên đăng nhập đã tồn tại!";
                    ViewBag.Doctor = _unitofwork.DoctorRepository.Get(u => u.DoctorId == DoctorId);
                    return View();
                }
                _usermanager.AddToRoleAsync(true_user, "Doctor").GetAwaiter().GetResult();
                var doctor = _unitofwork.DoctorRepository.Get(u => u.DoctorId == DoctorId);
                doctor.HasAccount = true;
                doctor.Username = account.UserName;
                _unitofwork.DoctorRepository.Update(doctor);
                _unitofwork.Save();
                return RedirectToAction("DoctorIndex");
            }
		}

		public IActionResult NurseIndex()
		{
			var nurselist=_unitofwork.NurseRepository.GetAll();
            ViewBag.TrangThaiTaiKhoan = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = "Chưa có tài khoản",
                        Value = "NoAccount"
                    },
                    new SelectListItem
                    {
                        Text = "Đã có tài khoản",
                        Value = "HasAccount"
                    }
                },
                "Value",
                "Text"
            );
            return View(nurselist);
		}
        [HttpPost]
        public IActionResult NurseIndex(string SearchName, string TrangThaiTaiKhoan)
        {
            var nurselist = _unitofwork.NurseRepository.GetAll();
            if (!string.IsNullOrEmpty(SearchName)) nurselist = nurselist.Where(u => u.NurseName.ToLower().Contains(SearchName.ToLower()));
            if (TrangThaiTaiKhoan != "NoFilter")
            {
                if (TrangThaiTaiKhoan == "NoAccount") nurselist = nurselist.Where(u => u.HasAccount == false);
                else if (TrangThaiTaiKhoan == "HasAccount") nurselist = nurselist.Where(u => u.HasAccount == true);
            }
            ViewBag.TrangThaiTaiKhoan = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = "Chưa có tài khoản",
                        Value = "NoAccount"
                    },
                    new SelectListItem
                    {
                        Text = "Đã có tài khoản",
                        Value = "HasAccount"
                    }
                },
                "Value",
                "Text"
            );
            return View(nurselist);
        }
		
        public IActionResult AssignNurseAccount(int NurseId)
        {
            ViewBag.Nurse= _unitofwork.NurseRepository.Get(u => u.NurseId == NurseId);
            return View();
        }
        [HttpPost]
        [ActionName("AssignNurseAccount")]
        public async Task<IActionResult> AssignNurseAccountPost(int NurseId, AccountModel account)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Nurse = _unitofwork.NurseRepository.Get(u => u.NurseId == NurseId);
                return View();
            }
            else
            {
                var true_user = new CustomedUser();
                true_user.UserId = NurseId;
                true_user.UserName = account.UserName;
                true_user.UserRole = Ultilities.Utilities.ERole.nurse;
                var result=_usermanager.CreateAsync(true_user, account.Password).GetAwaiter().GetResult();
                if (!result.Succeeded)
                {
                    TempData["error"] = "Tên đăng nhập đã tồn tại!";
                    ViewBag.Nurse = _unitofwork.NurseRepository.Get(u => u.NurseId == NurseId);
                    return View();
                }
                _usermanager.AddToRoleAsync(true_user, "Nurse").GetAwaiter().GetResult();
                var nurse = _unitofwork.NurseRepository.Get(u => u.NurseId == NurseId);
                nurse.HasAccount = true;
                nurse.Username = account.UserName;
                _unitofwork.NurseRepository.Update(nurse);
                _unitofwork.Save();
                return RedirectToAction("NurseIndex");
            }
        }

		public async Task<IActionResult> DeleteDoctorAccount(int DoctorId)
		{
			var doctor=_unitofwork.DoctorRepository.Get(u=>u.DoctorId == DoctorId);
			if (doctor != null)
			{
                doctor.HasAccount = false;
                _unitofwork.DoctorRepository.Update(doctor);
                _unitofwork.Save();
            }
			var user=_db.customedUsers.FirstOrDefault(u=>(u.UserId == DoctorId && u.UserRole==Ultilities.Utilities.ERole.doctor));
			if (user != null)
			{
				await _usermanager.DeleteAsync(user);
			}
			return RedirectToAction("AssignDoctorAccount", new {DoctorId=DoctorId});
		}
        public async Task<IActionResult> DeleteNurseAccount(int NurseId)
        {
            var nurse = _unitofwork.NurseRepository.Get(u => u.NurseId == NurseId);
            if (nurse != null)
            {
                nurse.HasAccount = false;
                _unitofwork.NurseRepository.Update(nurse);
                _unitofwork.Save();
            }
            var user = _db.customedUsers.FirstOrDefault(u => u.UserId == NurseId && u.UserRole == Ultilities.Utilities.ERole.nurse);
            if (user != null)
            {
                await _usermanager.DeleteAsync(user);
            }
            return RedirectToAction("AssignNurseAccount", new { NurseId = NurseId });
        }
        public IActionResult NhanSuIndex()
        {
            var nhansulist = _unitofwork.NhanSuRepository.GetAll();
            ViewBag.TrangThaiTaiKhoan = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = "Chưa có tài khoản",
                        Value = "NoAccount"
                    },
                    new SelectListItem
                    {
                        Text = "Đã có tài khoản",
                        Value = "HasAccount"
                    }
                },
                "Value",
                "Text"
            );
            ViewBag.VaiTro = new SelectList(new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Quản lí nhân sự",
                    Value = "QuanLiNhanSu"
                },
                new SelectListItem
                {
                    Text = "Quản lí vật tư",
                    Value = "QuanLiVatTu"
                },
                new SelectListItem
                {
                    Text="Quản lí bệnh nhân",
                    Value="QuanLiBenhNhan"
                },
                new SelectListItem
                {
                    Text="Chưa có",
                    Value="NoRole"
                }
            }, "Value", "Text");
            return View(nhansulist);
        }
        [HttpPost]
        public IActionResult NhanSuIndex(string SearchName, string TrangThaiTaiKhoan, string SearchVaiTro)
        {
            var nhansulist = _unitofwork.NhanSuRepository.GetAll();
            if (!string.IsNullOrEmpty(SearchName)) nhansulist = nhansulist.Where(u => u.NhanSuName.ToLower().Contains(SearchName.ToLower()));
            if (TrangThaiTaiKhoan != "NoFilter")
            {
                if (TrangThaiTaiKhoan == "NoAccount") nhansulist = nhansulist.Where(u => u.HasAccount == false);
                else if (TrangThaiTaiKhoan == "HasAccount") nhansulist = nhansulist.Where(u => u.HasAccount == true);
            }
            if (!string.IsNullOrEmpty(SearchVaiTro))
            {
                if (SearchVaiTro == "QuanLiVatTu") nhansulist = nhansulist.Where(u => u.Role == "QuanLiVatTu");
                else if (SearchVaiTro == "QuanLiNhanSu") nhansulist = nhansulist.Where(u => u.Role == "QuanLiNhanSu");
                else if (SearchVaiTro == "QuanLiBenhNhan") nhansulist = nhansulist.Where(u => u.Role == "QuanLiBenhNhan");
                else if (SearchVaiTro == "NoRole") nhansulist = nhansulist.Where(u => string.IsNullOrEmpty(u.Role));
            }
            ViewBag.TrangThaiTaiKhoan = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = "Chưa có tài khoản",
                        Value = "NoAccount"
                    },
                    new SelectListItem
                    {
                        Text = "Đã có tài khoản",
                        Value = "HasAccount"
                    }
                },
                "Value",
                "Text"
            );
            ViewBag.VaiTro = new SelectList(new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Quản lí nhân sự",
                    Value = "QuanLiNhanSu"
                },
                new SelectListItem
                {
                    Text = "Quản lí vật tư",
                    Value = "QuanLiVatTu"
                },
                new SelectListItem
                {
                    Text="Quản lí bệnh nhân",
                    Value="QuanLiBenhNhan"
                },
                new SelectListItem
                {
                    Text="Chưa có",
                    Value="NoRole"
                }
            }, "Value", "Text");
            return View(nhansulist);
        }
        public async Task<IActionResult> AssignNhanSuAccount(int NhanSuId)
        {
            var nhansu = _unitofwork.NhanSuRepository.Get(u => u.NhanSuId == NhanSuId);
            if (string.IsNullOrEmpty(nhansu.Role))
            {
                TempData["error"] = "Nhân sự phải có vai trò mới được cấp tài khoản!";
                return RedirectToAction("NhanSuIndex");
            }
            ViewBag.nhansu = _unitofwork.NhanSuRepository.Get(u => u.NhanSuId == NhanSuId);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AssignNhanSuAccount(int NhanSuId, AccountModel account, string Role)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.nhansu = _unitofwork.NhanSuRepository.Get(u => u.NhanSuId == NhanSuId);
                return View();
            }
            else
            {
                var true_user = new CustomedUser();
                true_user.UserId = NhanSuId;
                true_user.UserName = account.UserName;
                if (Role == "QuanLiVatTu") true_user.UserRole = ERole.quanlivattu;
                else if (Role == "QuanLiBenhNhan") true_user.UserRole = ERole.quanlibenhnhan;
                else if (Role == "QuanLiNhanSu") true_user.UserRole = ERole.quanlinhansu;
                var result = _usermanager.CreateAsync(true_user, account.Password).GetAwaiter().GetResult();
                if (!result.Succeeded)
                {
                    TempData["error"] = "Tên đăng nhập đã tồn tại!";
                    ViewBag.nhansu = _unitofwork.NhanSuRepository.Get(u => u.NhanSuId == NhanSuId);
                    return View();
                }
                if (true_user.UserRole == ERole.quanlinhansu) _usermanager.AddToRoleAsync(true_user, "QuanLiNhanSu").GetAwaiter().GetResult();
                if (true_user.UserRole == ERole.quanlibenhnhan) _usermanager.AddToRoleAsync(true_user, "QuanLiBenhNhan").GetAwaiter().GetResult();
                if (true_user.UserRole == ERole.quanlivattu) _usermanager.AddToRoleAsync(true_user, "QuanLiVatTu").GetAwaiter().GetResult();
                var nhansu = _unitofwork.NhanSuRepository.Get(u => u.NhanSuId == NhanSuId);
                nhansu.HasAccount = true;
                nhansu.UserName = account.UserName;
                _unitofwork.NhanSuRepository.Update(nhansu);
                _unitofwork.Save();
                return RedirectToAction("NhanSuIndex");
            }
        }
        public async Task<IActionResult> DeleteNhanSuAccount(int NhanSuId)
        {
            var nhansu = _unitofwork.NhanSuRepository.Get(u => u.NhanSuId == NhanSuId);
            if (nhansu != null)
            {
                nhansu.HasAccount = false;
                _unitofwork.NhanSuRepository.Update(nhansu);
                _unitofwork.Save();
            }
            ERole tmp=new ERole();
            if (nhansu.Role == "QuanLiNhanSu") tmp = ERole.quanlinhansu;
            else if (nhansu.Role == "QuanLiVatTu") tmp = ERole.quanlivattu;
            else if (nhansu.Role == "QuanLiBenhNhan") tmp = ERole.quanlibenhnhan;
            var user = _db.customedUsers.FirstOrDefault(u => u.UserId == NhanSuId && u.UserRole == tmp);
            if (user != null)
            {
                await _usermanager.DeleteAsync(user);
            }
            return RedirectToAction("AssignNhanSuAccount", new { NhanSuId = NhanSuId });
        }
    }
}
