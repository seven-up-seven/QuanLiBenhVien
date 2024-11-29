using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;
using System.Security.Principal;
using static PhanMemWebQuanLiBenhVien.Ultilities.Utilities;

namespace PhanMemWebQuanLiBenhVien.Controllers
{
	public class DoctorController : Controller
	{
		private IUnitOfWork _unitOfWork;
		private string wwwroot;
		private IWebHostEnvironment _webHostEnvironment;
		public DoctorController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
		{
			_unitOfWork = unitOfWork;
			_webHostEnvironment = webHostEnvironment;
		}
		public IActionResult Index()
		{
			var DoctorList = _unitOfWork.DoctorRepository.GetAll();
			foreach (var doctor in DoctorList)
			{
				var profession = _unitOfWork.ProfessionRepository.Get(u => u.ProfessionId == doctor.ProfessionId);
				doctor.Profession = profession;
			}
			return View(DoctorList);
		}

		[HttpGet("Create")]
		public IActionResult Create()
		{
			ViewBag.Professions = _unitOfWork.ProfessionRepository.GetAll().Select(u => new SelectListItem
			{
				Text = u.ProfessionName,
				Value = u.ProfessionId.ToString()
			});
			var genderList = Enum.GetValues(typeof(EGender))
			.Cast<EGender>()
			.Select(gender => new SelectListItem
			{
				Value = gender.ToString(),
				Text = gender.ToString()
			}).ToList();
			ViewBag.Genders = genderList;
			return View();
		}
		[HttpPost("Create")]
		public IActionResult Create(Doctor doctor, IFormFile? DoctorImg)
		{
			wwwroot = _webHostEnvironment.WebRootPath;
			if (DoctorImg != null)
			{
				string filename = Path.GetFileNameWithoutExtension(DoctorImg.FileName) + Path.GetExtension(DoctorImg.FileName);
				string filepath = Path.Combine(wwwroot, @"images\");
				using (var filestream = new FileStream(Path.Combine(filepath, filename), FileMode.Create))
				{
					DoctorImg.CopyTo(filestream);
				}
				doctor.DoctorImgURL = @"\images\" + filename;
			}
			else doctor.DoctorImgURL = "";
			_unitOfWork.DoctorRepository.Add(doctor);
			_unitOfWork.Save();
			return RedirectToAction("Index");
		}
		public IActionResult Update()
		{
			return View();
		}
		public IActionResult Delete(int DoctorId)
		{
			var doctor = _unitOfWork.DoctorRepository.Get(u => u.DoctorId == DoctorId);
			wwwroot = _webHostEnvironment.WebRootPath;
			if (!string.IsNullOrEmpty(doctor.DoctorImgURL))
			{
				var oldpath = Path.Combine(wwwroot, doctor.DoctorImgURL.TrimStart('\\'));
				if (System.IO.File.Exists(oldpath)) System.IO.File.Delete(oldpath);
			}
			_unitOfWork.DoctorRepository.Remove(doctor);
			_unitOfWork.Save();
			return RedirectToAction("Index");
		}
		public IActionResult Detail(int DoctorId)
		{
			var doctor=_unitOfWork.DoctorRepository.Get(u=>u.DoctorId==DoctorId);
			doctor.Profession=_unitOfWork.ProfessionRepository.Get(u=>u.ProfessionId==doctor.ProfessionId);
			return View(doctor);
		}
	}
}
