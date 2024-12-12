using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using PhanMemWebQuanLiBenhVien.DataAccess;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;
using PhanMemWebQuanLiBenhVien.Models.Models;
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
            ViewBag.Professions = _unitOfWork.ProfessionRepository.GetAll().Select(u => new SelectListItem
            {
                Text = u.ProfessionName,
                Value = u.ProfessionId.ToString()
            });
            return View(DoctorList);
		}
        [HttpPost]
        public IActionResult Index(string SearchDoctorName, string SearchDoctorCCCD, int ProfessionId)
        {
            var doctorlist = _unitOfWork.DoctorRepository.GetAll();
            if (!string.IsNullOrEmpty(SearchDoctorName)) doctorlist = doctorlist.Where(u => u.DoctorName.ToLower().Contains(SearchDoctorName.ToLower()));
            if (!string.IsNullOrEmpty(SearchDoctorCCCD)) doctorlist = doctorlist.Where(u => u.DoctorCCCD.ToLower().Contains(SearchDoctorCCCD.ToLower()));
            if (ProfessionId != 0) doctorlist = doctorlist.Where(u => u.ProfessionId == ProfessionId);
            ViewBag.Professions = _unitOfWork.ProfessionRepository.GetAll().Select(u => new SelectListItem
            {
                Text = u.ProfessionName,
                Value = u.ProfessionId.ToString()
            });
            return View(doctorlist);
        }
        public IActionResult DanhSachBacSiThuocKhoa(int KhoaId)
        {
            var DoctorList = _unitOfWork.DoctorRepository.GetAll(dr => dr.ProfessionId == KhoaId);
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
			if (ModelState.IsValid)
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
				TempData["success"] = "Tạo bác sĩ mới thành công!";
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
			else
			{
				TempData["error"] = "Thông tin bác sĩ không hợp lệ!";
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
		public IActionResult Update(Doctor doctor, IFormFile? DoctorImg)
		{
            if (ModelState.IsValid)
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
                TempData["success"] = "Cập nhật bác sĩ thành công!";
                if (User.IsInRole("Doctor")) return RedirectToAction("DoctorHomePage", new { DoctorId = doctor.DoctorId });
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Thông tin bác sĩ không hợp lệ!";
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
                return View(doctor);
            }
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
            doctor.PatientList = _unitOfWork.MedicalRecordRepository.GetAll(u => u.DoctorId == DoctorId)
                       .Select(mr => new Patient
                       {
                           PatientId = mr.PatientId,
                           Name = mr.PatientName
                       }).ToList();
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
			Dictionary<string, Dictionary<int, PhongKham>> CaTrucPhongKham = new Dictionary<string, Dictionary<int, PhongKham>>();
            Dictionary<string, Dictionary<int, PhongCapCuu>> CaTrucPhongCapCuu = new Dictionary<string, Dictionary<int, PhongCapCuu>>();
            var phongkhamworkschedules = _unitOfWork.WorkScheduleRepository.GetAll(u=>u.PhongKhamId!=null);
            var phongcapcuuworkschedules = _unitOfWork.WorkScheduleRepository.GetAll(u => u.PhongCapCuuId != null);
			foreach(var ws in phongkhamworkschedules)
			{
                if (ws.DayOfWeek!=null)
                {
                    if (!CaTrucPhongKham.ContainsKey(ws.DayOfWeek))
                    {
                        CaTrucPhongKham[ws.DayOfWeek] = new Dictionary<int, PhongKham>();
                    }
                    if (ws.DoctorId1 == doctor.DoctorId)
                    {
                        var phongkham = _unitOfWork.PhongKhamRepository.Get(u => u.RoomId == ws.PhongKhamId);
                        CaTrucPhongKham[ws.DayOfWeek][1] = phongkham;
                    }
                    if (ws.DoctorId2 == doctor.DoctorId)
                    {
                        var phongkham = _unitOfWork.PhongKhamRepository.Get(u => u.RoomId == ws.PhongKhamId);
                        CaTrucPhongKham[ws.DayOfWeek][2] = phongkham;
                    }
                    if (ws.DoctorId3 == doctor.DoctorId)
                    {
                        var phongkham = _unitOfWork.PhongKhamRepository.Get(u => u.RoomId == ws.PhongKhamId);
                        CaTrucPhongKham[ws.DayOfWeek][3] = phongkham;
                    }
                }
            }
            foreach (var ws in phongcapcuuworkschedules)
            {
                if (ws.DayOfWeek != null)
                {
                    if (!CaTrucPhongCapCuu.ContainsKey(ws.DayOfWeek))
                    {
                        CaTrucPhongCapCuu[ws.DayOfWeek] = new Dictionary<int, PhongCapCuu>();
                    }
                    if (ws.DoctorId1 == doctor.DoctorId)
                    {
                        var phongcapcuu = _unitOfWork.PhongCapCuuRepository.Get(u => u.RoomId == ws.PhongCapCuuId);
                        CaTrucPhongCapCuu[ws.DayOfWeek][1] = phongcapcuu;
                    }
                    if (ws.DoctorId2 == doctor.DoctorId)
                    {
                        var phongcapcuu = _unitOfWork.PhongCapCuuRepository.Get(u => u.RoomId == ws.PhongCapCuuId);
                        CaTrucPhongCapCuu[ws.DayOfWeek][2] = phongcapcuu;
                    }
                    if (ws.DoctorId3 == doctor.DoctorId)
                    {
                        var phongcapcuu = _unitOfWork.PhongCapCuuRepository.Get(u => u.RoomId == ws.PhongCapCuuId);
                        CaTrucPhongCapCuu[ws.DayOfWeek][3] = phongcapcuu;
                    }
                }
            }
            ViewBag.CaTrucPhongCapCuu = CaTrucPhongCapCuu;
            return View(CaTrucPhongKham);
        }

        public IActionResult DoctorPatients(int DoctorId)
        {
            var doctor = _unitOfWork.DoctorRepository.Get(dr => dr.DoctorId == DoctorId);
			doctor.PatientList = new List<Patient>();
			var medicalRecords = _unitOfWork.MedicalRecordRepository.GetAll();
			var patients = _unitOfWork.PatientRepository.GetAll(); 
			foreach (var patient in patients)
			{
                patient.MedicalRecords = (ICollection<MedicalRecord>?)_unitOfWork.MedicalRecordRepository.GetAll(mr => mr.PatientId == patient.PatientId);
                var lastMedicalRecord = patient.MedicalRecords?.LastOrDefault();
                if (patient.TrangThaiBenhAn == Ultilities.Utilities.ETrangThaiBenhAn.dangchuatri &&
                    lastMedicalRecord != null &&
                    lastMedicalRecord.TrangThaiBenhAn == Ultilities.Utilities.ETrangThaiBenhAn.dangchuatri &&
                    lastMedicalRecord.TrangThaiDieuTri == Ultilities.Utilities.ETrangThaiDieuTri.noitru)
                {
                    if (lastMedicalRecord.DoctorId == doctor.DoctorId)
					{
                        lastMedicalRecord.PhongBenh = _unitOfWork.PhongBenhRepository.Get(pb => pb.RoomId == lastMedicalRecord.PhongBenhId);
                        doctor.PatientList.Add(patient);
					
					}
                }
            }
			ViewBag.TinhTrangBenhNhan = new SelectList(
				new List<SelectListItem>
				{
					new SelectListItem
					{
						Text = "Không triệu chứng",
						Value = ETinhTrangBenhNhan.khongtrieuchung.ToString()
					},
					new SelectListItem
					{
						Text = "Có triệu chứng",
						Value = ETinhTrangBenhNhan.cotrieuchung.ToString()
					},
					new SelectListItem
					{
						Text = "Trở nặng",
						Value = ETinhTrangBenhNhan.tronang.ToString()
					}
				},
				"Value",
				"Text"
			);
			ViewBag.doctor = doctor;
            return View(doctor.PatientList);
        }
        [HttpPost]
		public IActionResult DoctorPatients(int DoctorId, string SearchPatientName, string SearchPatientCCCD, string TinhTrangBenhNhan)
        {
			var doctor = _unitOfWork.DoctorRepository.Get(dr => dr.DoctorId == DoctorId);
			doctor.PatientList = new List<Patient>();
			var medicalRecords = _unitOfWork.MedicalRecordRepository.GetAll();
			var patients = _unitOfWork.PatientRepository.GetAll();
            if (!string.IsNullOrEmpty(SearchPatientName)) patients = patients.Where(u => u.Name.ToLower().Contains(SearchPatientName.ToLower()));
            if (!string.IsNullOrEmpty(SearchPatientCCCD)) patients = patients.Where(u => u.CCCD.Contains(SearchPatientCCCD));
			foreach (var patient in patients)
			{
				patient.MedicalRecords = (ICollection<MedicalRecord>?)_unitOfWork.MedicalRecordRepository.GetAll(mr => mr.PatientId == patient.PatientId);
				var lastMedicalRecord = patient.MedicalRecords?.LastOrDefault();
				if (patient.TrangThaiBenhAn == Ultilities.Utilities.ETrangThaiBenhAn.dangchuatri &&
					lastMedicalRecord != null &&
					lastMedicalRecord.TrangThaiBenhAn == Ultilities.Utilities.ETrangThaiBenhAn.dangchuatri &&
					lastMedicalRecord.TrangThaiDieuTri == Ultilities.Utilities.ETrangThaiDieuTri.noitru)
				{
                    if (TinhTrangBenhNhan!="NoFilter")
                    {
						if (lastMedicalRecord.DoctorId == doctor.DoctorId && lastMedicalRecord.TinhTrangBenhNhan.ToString()==TinhTrangBenhNhan)
						{
							lastMedicalRecord.PhongBenh = _unitOfWork.PhongBenhRepository.Get(pb => pb.RoomId == lastMedicalRecord.PhongBenhId);
							doctor.PatientList.Add(patient);
						}
					}
                    else
                    {
						if (lastMedicalRecord.DoctorId == doctor.DoctorId)
						{
							lastMedicalRecord.PhongBenh = _unitOfWork.PhongBenhRepository.Get(pb => pb.RoomId == lastMedicalRecord.PhongBenhId);
							doctor.PatientList.Add(patient);
						}
					}
				}
			}
			ViewBag.doctor = doctor;
			ViewBag.TinhTrangBenhNhan = new SelectList(
				new List<SelectListItem>
				{
					new SelectListItem
					{
						Text = "Không triệu chứng",
						Value = ETinhTrangBenhNhan.khongtrieuchung.ToString()
					},
					new SelectListItem
					{
						Text = "Có triệu chứng",
						Value = ETinhTrangBenhNhan.cotrieuchung.ToString()
					},
					new SelectListItem
					{
						Text = "Trở nặng",
						Value = ETinhTrangBenhNhan.tronang.ToString()
					}
				},
				"Value",
				"Text"
			);
			return View(doctor.PatientList);
		}


		public IActionResult DoctorPatientDetail(int PatientId)
        {
            var medicalRecord = _unitOfWork.MedicalRecordRepository.GetAll(u => u.PatientId == PatientId);
            foreach(var mr in medicalRecord)
			{
                if (mr.DoctorId != null) mr.Doctor = _unitOfWork.DoctorRepository.Get(u => u.DoctorId == mr.DoctorId);
                if (mr.NurseId != null) mr.Nurse = _unitOfWork.NurseRepository.Get(u => u.NurseId == mr.NurseId);
                if (mr.PhongBenhId != null) mr.PhongBenh = _unitOfWork.PhongBenhRepository.Get(u => u.RoomId == mr.PhongBenhId);
                if (mr.PhongKhamId != null) mr.PhongKham = _unitOfWork.PhongKhamRepository.Get(u => u.RoomId == mr.PhongKhamId);
            }
			//patient.MedicalRecords = (ICollection<MedicalRecord>?)_unitOfWork.MedicalRecordRepository.GetAll(mr => mr.PatientId == patient.PatientId); \
			var patient = _unitOfWork.PatientRepository.Get(pt => pt.PatientId == PatientId);
			patient.MedicalRecords = (ICollection<MedicalRecord>?)medicalRecord;
			patient.Profession = _unitOfWork.ProfessionRepository.Get(u => u.ProfessionId == patient.ProfesisonId);
            return View(patient);
        }
        public async Task<IActionResult> DoctorMission(int month, int year)
        {
            // Lấy thông tin người dùng hiện tại
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return NotFound("Người dùng không tồn tại.");
            var trueUser = (CustomedUser)user;

            // Lấy thông tin bác sĩ từ UserName
            var doctor = _unitOfWork.DoctorRepository.Get(u => u.Username == trueUser.UserName);
            if (doctor == null) return NotFound("Bác sĩ không tồn tại.");

            // Lấy thời gian hiện tại nếu không truyền tháng và năm
            if (month == 0 || year == 0)
            {
                var currentDate = DateTime.Now;
                month = currentDate.Month;
                year = currentDate.Year;
            }

            // Gán danh sách nhiệm vụ của bác sĩ, chỉ lấy các nhiệm vụ trong tháng và năm được chọn
            doctor.MissionList = _unitOfWork.MissionRepository.GetAll(m =>
                m.DoctorId == doctor.DoctorId &&
                m.Time.Month == month &&
                m.Time.Year == year).ToList();
            foreach (var mission in doctor.MissionList)
            {
                if (mission.PhongKhamId != null)
                {
                    var phongkham = _unitOfWork.PhongKhamRepository.Get(u => u.RoomId == mission.PhongKhamId);
                    mission.PhongKham = phongkham;
                }
                else
                {
                    var phongbenh = _unitOfWork.PhongBenhRepository.Get(u => u.RoomId == mission.PhongBenhId);
                    mission.PhongBenh = phongbenh;
                }
            }
            // Truyền tháng và năm cho View
            ViewBag.SelectedMonth = month;
            ViewBag.SelectedYear = year;

            return View(doctor);
        }

    }
}
