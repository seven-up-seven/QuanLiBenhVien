using Microsoft.AspNetCore.Mvc;
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
				return View(profession);
			}
			return View();
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
	}
}
