using Microsoft.AspNetCore.Mvc;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;

namespace PhanMemWebQuanLiBenhVien.Controllers
{
	[Route("PhongBenh")]
	public class PhongBenhController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public PhongBenhController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpGet("Index")]
		public IActionResult Index()
		{
			var listPhongBenh = _unitOfWork.PhongBenhRepository.GetAll();
			foreach (var phong in listPhongBenh)
			{
				phong.Patients = (ICollection<Patient>?)_unitOfWork.PatientRepository.GetAll(pt => pt.PhongBenhId == phong.RoomId);
			}
			return View(listPhongBenh);
		}


		[HttpGet("Create")]
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost("Create")]
		public IActionResult Create(PhongBenh phongBenh)
		{
			if (ModelState.IsValid)
			{
				_unitOfWork.PhongBenhRepository.Add(phongBenh);
				_unitOfWork.Save();
				return RedirectToAction("Index");
			}

			return View();
		}


		[HttpGet("Detail/{PhongBenhId}")]
		public IActionResult Detail(int PhongBenhId)
		{
			var phongBenh = _unitOfWork.PhongBenhRepository.Get(pb => pb.RoomId == PhongBenhId);
			phongBenh.Patients = (ICollection<Patient>?)_unitOfWork.PatientRepository.GetAll(pt => pt.PhongBenhId == PhongBenhId);
			return View(phongBenh);
		}

		[HttpPost("Delete/{PhongBenhId}")]
		public IActionResult Delete(int PhongBenhId)
		{
			var phongBenh = _unitOfWork.PhongBenhRepository.Get(pb => pb.RoomId == PhongBenhId);
			if (phongBenh != null)
			{
				_unitOfWork.PhongBenhRepository.Remove(phongBenh);
				_unitOfWork.Save();
				return RedirectToAction("Index");
			}
			return View();
		}
		[HttpGet("Update/{PhongBenhId}")]
		// lấy thông tin thông qua ID
		public IActionResult Update(int PhongBenhId)
		{
			var phongBenh = _unitOfWork.PhongBenhRepository.Get(pb => pb.RoomId == PhongBenhId);
			if (phongBenh == null)
			{
				return NotFound();
			}
			return View(phongBenh);
		}

		[HttpPost("Update/{PhongBenhId}")]
		[ActionName("Update")]
		// cập nhật dữ liệu
		public IActionResult UpdatePost(PhongBenh phongBenh)
		{
			if (ModelState.IsValid)
			{
				_unitOfWork.PhongBenhRepository.Update(phongBenh);
				_unitOfWork.Save();
				return RedirectToAction("Index");
			}
			return View(phongBenh);
		}

	}

}