using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;
using System.Security.Principal;
using static PhanMemWebQuanLiBenhVien.Ultilities.Utilities;

namespace PhanMemWebQuanLiBenhVien.Controllers
{
    public class DoctorController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public DoctorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var DoctorList=_unitOfWork.DoctorRepository.GetAll().ToList();
            return View(DoctorList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Professions = _unitOfWork.ProfessionRepository.GetAll().Select(u => new SelectListItem { 
                Text=u.ProfessionName,
                Value=u.ProfessionId.ToString()
            });
			var genderList = Enum.GetValues(typeof(EGender))
			.Cast<EGender>()
			.Select(gender => new SelectListItem
			{
				Value = gender.ToString(),
				Text = gender.ToString()
			}).ToList();
			ViewBag.Genders=genderList;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Doctor doctor)
        {
            _unitOfWork.DoctorRepository.Add(doctor);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        public IActionResult Update()
        {
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }
    }
}
