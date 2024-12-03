using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using PhanMemWebQuanLiBenhVien.DataAccess;
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
		private UserManager<IdentityUser> _userManager;
		private IWebHostEnvironment _webHostEnvironment;
		private ApplicationDbContext _db;
		public DoctorController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, UserManager<IdentityUser> userManager, ApplicationDbContext db)
		{
			_unitOfWork = unitOfWork;
			_webHostEnvironment = webHostEnvironment;
			_userManager = userManager;
			_db = db;
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
		public IActionResult Update(int DoctorId)
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
			var doctor=_unitOfWork.DoctorRepository.Get(u=>u.DoctorId == DoctorId);
            return View(doctor);
		}
		[HttpPost]
		public ActionResult Update(Doctor doctor, IFormFile? DoctorImg)
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
            _unitOfWork.DoctorRepository.Update(doctor);
			_unitOfWork.Save();
			if (User.IsInRole("Doctor")) return RedirectToAction("DoctorHomePage", new {DoctorId=doctor.DoctorId});
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> Delete(int DoctorId)
		{
			var doctor = _unitOfWork.DoctorRepository.Get(u => u.DoctorId == DoctorId);
			wwwroot = _webHostEnvironment.WebRootPath;
			if (!string.IsNullOrEmpty(doctor.DoctorImgURL))
			{
				var oldpath = Path.Combine(wwwroot, doctor.DoctorImgURL.TrimStart('\\'));
				if (System.IO.File.Exists(oldpath)) System.IO.File.Delete(oldpath);
			}
			var user=_db.customedUsers.FirstOrDefault(u=>(u.UserId == DoctorId && u.UserRole==ERole.doctor));
			_userManager.DeleteAsync(user).GetAwaiter().GetResult();
			_unitOfWork.DoctorRepository.Remove(doctor);
			_unitOfWork.Save();
			return RedirectToAction("Index");
		}
		public IActionResult Detail(int DoctorId)
		{
			var doctor=_unitOfWork.DoctorRepository.Get(u=>u.DoctorId==DoctorId);
			doctor.Profession=_unitOfWork.ProfessionRepository.Get(u=>u.ProfessionId==doctor.ProfessionId);
			doctor.PatientList=_unitOfWork.PatientRepository.GetAll(u=>u.DoctorId == DoctorId).ToList();
			return View(doctor);
		}
		public IActionResult DoctorHomePage(int DoctorId)
		{
			var doctor = _unitOfWork.DoctorRepository.Get(u => u.DoctorId == DoctorId);
			return View(doctor);
		}

		public IActionResult DashBoard(int DoctorId)
		{
			var doctor = _unitOfWork.DoctorRepository.Get(dr => dr.DoctorId == DoctorId); 

			return View(doctor);
		}

        public IActionResult DoctorWorkSchedule(int DoctorId)
        {
            var doctor = _unitOfWork.DoctorRepository.Get(dr => dr.DoctorId == DoctorId);
			var workschedule = _unitOfWork.WorkScheduleRepository.Get(ws => ws.DoctorId == DoctorId);
			ViewBag.WS = workschedule; 
            return View(doctor);
        }

        public IActionResult DoctorPatients(int DoctorId)
        {
            var doctor = _unitOfWork.DoctorRepository.Get(dr => dr.DoctorId == DoctorId);
			var patients = _unitOfWork.PatientRepository.GetAll(pt => (pt.TrangThaiDieuTri == ETrangThaiDieuTri.nhapvien && pt.DoctorId == DoctorId));
			foreach (var patient in patients)
			{
				patient.PhongBenh = _unitOfWork.PhongBenhRepository.Get(pb => pb.RoomId == patient.PhongBenhId);
            }
			ViewBag.Patients = patients;
			
            return View(doctor);
        }

        public IActionResult DoctorPatientDetail(int PatientId)
        {
            var patient = _unitOfWork.PatientRepository.Get(u => u.PatientId == PatientId);
            if (patient.DoctorId != null) patient.Doctor = _unitOfWork.DoctorRepository.Get(u => u.DoctorId == patient.DoctorId);
            if (patient.NurseId != null) patient.Nurse = _unitOfWork.NurseRepository.Get(u => u.NurseId == patient.NurseId);
            if (patient.PhongBenhId != null) patient.PhongBenh = _unitOfWork.PhongBenhRepository.Get(u => u.RoomId == patient.PhongBenhId);
            if (patient.PhongKhamId != null) patient.PhongKham = _unitOfWork.PhongKhamRepository.Get(u => u.RoomId == patient.PhongKhamId);
			patient.MedicalRecords = (ICollection<MedicalRecord>?)_unitOfWork.MedicalRecordRepository.GetAll(mr => mr.PatientId == patient.PatientId); 
            return View(patient);
        }
    }
}
