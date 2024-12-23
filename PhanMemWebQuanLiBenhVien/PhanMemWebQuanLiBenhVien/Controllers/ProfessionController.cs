using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using PhanMemWebQuanLiBenhVien.DataAccess;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;
using static PhanMemWebQuanLiBenhVien.Ultilities.Utilities;

namespace PhanMemWebQuanLiBenhVien.Controllers
{
	[Route("Profession")]
	public class ProfessionController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private ApplicationDbContext _db;
		private UserManager<IdentityUser> _userManager;
		public ProfessionController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, ApplicationDbContext db)
		{
			_unitOfWork = unitOfWork;
			_userManager = userManager;
			_db = db;
		}

		[HttpGet("Index")]
		public IActionResult Index()
		{
			var listProfession = _unitOfWork.ProfessionRepository.GetAll();
			foreach (var profession in listProfession)
			{
				var truongkhoa=_unitOfWork.DoctorRepository.Get(u => u.DoctorId == profession.TruongKhoaId);
				if (truongkhoa!=null) profession.TruongKhoaName = truongkhoa.DoctorName;
			}
			return View(listProfession);
		}
        [HttpPost("Index")]
        public IActionResult Index(string SearchName)
        {
            var listProfession = _unitOfWork.ProfessionRepository.GetAll();
			if (!string.IsNullOrEmpty(SearchName)) listProfession = listProfession.Where(u => u.ProfessionName.ToLower().Contains(SearchName.ToLower()));
            foreach (var profession in listProfession)
            {
                var truongkhoa = _unitOfWork.DoctorRepository.Get(u => u.DoctorId == profession.TruongKhoaId);
                if (truongkhoa != null) profession.TruongKhoaName = truongkhoa.DoctorName;
            }
            return View(listProfession);
        }

        [HttpGet("Create")]
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost("Create")]
		public IActionResult Create(Profession profession)
		{
			if (ModelState.IsValid)
			{
				_unitOfWork.ProfessionRepository.Add(profession);
				_unitOfWork.Save();
                TempData["success"] = "Thêm chuyên khoa thành công!";
                ActivityTrackingFunction trackingtool = new ActivityTrackingFunction(_db, _unitOfWork);
                var tmpuser = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
                var truetmp_user = (CustomedUser)tmpuser;
                trackingtool.TrackingActivity(truetmp_user.UserId, truetmp_user.UserName, ETypeOfActivity.them, truetmp_user.UserRole, profession.ProfessionId, profession, null);
                return RedirectToAction("Index");
			}

			return View();
		}


		[HttpGet("Detail/{ProfessionId}")]
		public IActionResult Detail(int ProfessionId)
		{
			var profession = _unitOfWork.ProfessionRepository.Get(pf => pf.ProfessionId == ProfessionId);
			profession.DoctorList = _unitOfWork.DoctorRepository.GetAll(dr => dr.ProfessionId == ProfessionId).ToList(); 
			return View(profession);
		}


		[HttpGet("Update/{ProfessionId}")]
		public IActionResult Update(int ProfessionId)
		{
			var profession = _unitOfWork.ProfessionRepository.Get(pf => pf.ProfessionId == ProfessionId);
			if (profession != null)
			{
                ViewBag.DoctorsInProfession = _unitOfWork.DoctorRepository.GetAll(u => u.ProfessionId == ProfessionId).Select(u => new SelectListItem
                {
                    Text = u.DoctorName,
                    Value = u.DoctorId.ToString()
                });
                return View(profession);
			}
			return View(null);
		}
		[HttpPost("Update/{ProfessionId}")]
		public IActionResult Update(Profession profession)
		{
			if (ModelState.IsValid)
			{
				var details=new List<string>();
				var objFromDb=_unitOfWork.ProfessionRepository.Get(u=>u.ProfessionId== profession.ProfessionId);
				if (profession.ProfessionName != objFromDb.ProfessionName) details.Add($"Tên: {objFromDb.ProfessionName} -> {profession.ProfessionName}");
                if (profession.Description != objFromDb.Description) details.Add($"Mô tả: {objFromDb.Description} -> {profession.Description}");
                if (objFromDb.TruongKhoaId != profession.TruongKhoaId)
				{
					if (objFromDb.TruongKhoaId != null && profession.TruongKhoaId != null) details.Add($"Trưởng khoa: {_unitOfWork.DoctorRepository.Get(u => u.DoctorId == objFromDb.TruongKhoaId).DoctorName} -> {_unitOfWork.DoctorRepository.Get(u => u.DoctorId == profession.TruongKhoaId).DoctorName}");
					else if (objFromDb.TruongKhoaId != null && profession.TruongKhoaId == null) details.Add($"Bỏ trưởng khoa: {_unitOfWork.DoctorRepository.Get(u => u.DoctorId == objFromDb.TruongKhoaId).DoctorName}");
					else details.Add($"Trưởng khoa mới: {_unitOfWork.DoctorRepository.Get(u => u.DoctorId == profession.TruongKhoaId).DoctorName}");
                }
                ActivityTrackingFunction trackingtool = new ActivityTrackingFunction(_db, _unitOfWork);
                var tmpuser = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
                var truetmp_user = (CustomedUser)tmpuser;
                trackingtool.TrackingActivity(truetmp_user.UserId, truetmp_user.UserName, ETypeOfActivity.sua, truetmp_user.UserRole, profession.ProfessionId, profession, details);
                _unitOfWork.ProfessionRepository.Update(profession);
				_unitOfWork.Save();
                TempData["success"] = "Cập nhật chuyên khoa thành công!";
                return RedirectToAction("Index");
			}
			return View();
		}

		[HttpPost("Delete/{ProfessionId}")]
        public IActionResult Delete(int ProfessionId)
        {
            var profession = _unitOfWork.ProfessionRepository.Get(pf => pf.ProfessionId == ProfessionId);
			profession.DoctorList = _unitOfWork.DoctorRepository.GetAll(dr => dr.ProfessionId == ProfessionId).ToList();
			if(profession.DoctorList.Count() == 0)
			{
				
                _unitOfWork.ProfessionRepository.Remove(profession);
                _unitOfWork.Save();
                ActivityTrackingFunction trackingtool = new ActivityTrackingFunction(_db, _unitOfWork);
                var tmpuser = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
                var truetmp_user = (CustomedUser)tmpuser;
                trackingtool.TrackingActivity(truetmp_user.UserId, truetmp_user.UserName, ETypeOfActivity.xoa, truetmp_user.UserRole, profession.ProfessionId, profession, null);
                TempData["success"] = "Chuyên khoa đã được xóa thành công!";
            }
			else
			{
                TempData["error"] = "Không thể xoá chuyên khoa do đang có bác sĩ";
            }
            return RedirectToAction("Index");
        }

        [HttpGet("ThemTruongKhoa")]
		public IActionResult ThemTruongKhoa(int ProfessionId)
		{
			var doctorlist = _unitOfWork.DoctorRepository.GetAll(u => u.ProfessionId == ProfessionId);
			ViewBag.doctorlist = doctorlist.Select(u => new SelectListItem{
				Text=u.DoctorName,
				Value=u.DoctorId.ToString()
			}).ToList();
			var profession=_unitOfWork.ProfessionRepository.Get(u=>u.ProfessionId==ProfessionId);
			return View(profession);
		}
		[HttpPost("ThemTruongKhoa")]
		public IActionResult ThemTruongKhoa(int TruongKhoaId, int ProfessionId)
		{
			var profession = _unitOfWork.ProfessionRepository.Get(u => u.ProfessionId == ProfessionId);
			var doctor = _unitOfWork.DoctorRepository.Get(u => u.DoctorId == TruongKhoaId);
			if(TruongKhoaId != 0) 
			{ 
                profession.TruongKhoaId = TruongKhoaId;
                profession.TruongKhoaName = doctor.DoctorName;
                doctor.IsTruongKhoa = true;
                var details = new List<string>();
                var objFromDb = _unitOfWork.ProfessionRepository.Get(u => u.ProfessionId == profession.ProfessionId);
                _unitOfWork.ProfessionRepository.Update(profession);
                _unitOfWork.DoctorRepository.Update(doctor);
				TempData["success"] = "Cập nhật trưởng khoa thành công"; 
                _unitOfWork.Save();
                if (profession.ProfessionName != objFromDb.ProfessionName) details.Add($"Tên: {objFromDb.ProfessionName} -> {profession.ProfessionName}");
                if (profession.Description != objFromDb.Description) details.Add($"Mô tả: {objFromDb.Description} -> {profession.Description}");
				if (objFromDb.TruongKhoaId != profession.TruongKhoaId)
				{
					if (profession.ProfessionId!=null) details.Add($"Trưởng khoa mới: {_unitOfWork.DoctorRepository.Get(u => u.DoctorId == profession.TruongKhoaId).DoctorName}");
					else details.Add($"Bỏ trưởng khoa: {_unitOfWork.DoctorRepository.Get(u => u.DoctorId == objFromDb.TruongKhoaId).DoctorName}");
                }
				ActivityTrackingFunction trackingtool = new ActivityTrackingFunction(_db, _unitOfWork);
                var tmpuser = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
                var truetmp_user = (CustomedUser)tmpuser;
                trackingtool.TrackingActivity(truetmp_user.UserId, truetmp_user.UserName, ETypeOfActivity.sua, truetmp_user.UserRole, profession.ProfessionId, profession, details);
                return RedirectToAction("Index");
            }
			else
			{
				TempData["error"] = "Hãy chọn trưởng khoa hợp lệ";
                var doctorlist = _unitOfWork.DoctorRepository.GetAll(u => u.ProfessionId == ProfessionId);
                ViewBag.doctorlist = doctorlist.Select(u => new SelectListItem
                {
                    Text = u.DoctorName,
                    Value = u.DoctorId.ToString()
                }).ToList();
                var pr = _unitOfWork.ProfessionRepository.Get(u => u.ProfessionId == ProfessionId);
				return View(pr);
            }
		}
	}
}
