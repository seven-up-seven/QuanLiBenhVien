﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;

namespace PhanMemWebQuanLiBenhVien.Controllers
{
	[Route("Profession")]
	public class ProfessionController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public ProfessionController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
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
                _unitOfWork.ProfessionRepository.Update(profession);
                _unitOfWork.DoctorRepository.Update(doctor);
				TempData["success"] = "Cập nhật trưởng khoa thành công"; 
                _unitOfWork.Save();
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
