using Microsoft.AspNetCore.Mvc;
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
				return RedirectToAction("Index");
			}
			return View();
		}

		[HttpPost("Delete/{ProfessionId}")]
		public IActionResult Delete(int ProfessionId)
		{
			var profession = _unitOfWork.ProfessionRepository.Get(pf => pf.ProfessionId == ProfessionId);
			if (profession != null)
			{
				_unitOfWork.ProfessionRepository.Remove(profession);
				_unitOfWork.Save();
				return RedirectToAction("Index");
			}
			return View();
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
			if (profession.TruongKhoaId!=null)
			{
				var olddoctor = _unitOfWork.DoctorRepository.Get(u => u.DoctorId == profession.TruongKhoaId);
				olddoctor.IsTruongKhoa = false;
				_unitOfWork.DoctorRepository.Update(olddoctor);
			}
			profession.TruongKhoaId= TruongKhoaId;
			profession.TruongKhoaName = doctor.DoctorName;
			doctor.IsTruongKhoa= true;
			_unitOfWork.ProfessionRepository.Update(profession);
			_unitOfWork.DoctorRepository.Update(doctor);
			_unitOfWork.Save();
			return RedirectToAction("Index");
		}
	}
}
