using ClosedXML.Excel;
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
using PhanMemWebQuanLiBenhVien.Ultilities;
using System.Data;
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
        public IActionResult Index(string SearchDoctorName, string SearchDoctorCCCD, int ProfessionId, string DoctorId)
        {
            var doctorlist = _unitOfWork.DoctorRepository.GetAll();
            if (!string.IsNullOrEmpty(DoctorId))
            {
                if (int.TryParse(DoctorId, out _))
                {
                    doctorlist = doctorlist.Where(u => u.DoctorId == int.Parse(DoctorId));
                }
                else
                {
                    TempData["error"] = "ID bác sĩ phải là số!";
                    return View(doctorlist);
                }
            }
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
                GlobalFunctions globalfunction = new GlobalFunctions(_unitOfWork, doctor);
                if (globalfunction.ExistDuplicateCCCD())
                {
                    TempData["error"] = "CCCD đã tồn tại!";
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
                else
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
                GlobalFunctions globalfunction = new GlobalFunctions(_unitOfWork, doctor);
                if (globalfunction.ExistDuplicateCCCD())
                {
                    TempData["error"] = "CCCD đã tồn tại!";
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
                    if (User.IsInRole("Doctor"))
                    {
                        return RedirectToAction("DoctorHomePage", "Doctor", new { DoctorId = doctor.DoctorId });
                    }
                    return View(doctor);
                }
                else
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
            doctor.PatientList = new List<Patient>(); 
            var mrl = _unitOfWork.MedicalRecordRepository.GetAll(mr => mr.DoctorId == DoctorId && mr.TrangThaiBenhAn == ETrangThaiBenhAn.dangchuatri); 
            foreach(var mr in mrl)
            {
                doctor.PatientList.Add(_unitOfWork.PatientRepository.Get(pt => pt.PatientId == mr.PatientId));
            }
            if(doctor.PatientList.Count() > 0)
            {
			    wwwroot = _webHostEnvironment.WebRootPath;
			    if (!string.IsNullOrEmpty(doctor.DoctorImgURL))
			    {
				    var oldpath = Path.Combine(wwwroot, doctor.DoctorImgURL.TrimStart('\\'));
				    if (System.IO.File.Exists(oldpath)) System.IO.File.Delete(oldpath);
			    }
			    var user=_db.customedUsers.FirstOrDefault(u=>(u.UserId == DoctorId && u.UserRole==ERole.doctor));
                if(user!=null)
                {
                    _userManager.DeleteAsync(user).GetAwaiter().GetResult();
                }
			    _unitOfWork.DoctorRepository.Remove(doctor);
			    _unitOfWork.Save();
                    TempData["success"] = "Xoá bác sĩ thành công";
            }
            else
            {
                TempData["error"] = "Bác sĩ đang phụ trách cho một số bệnh nhân, không thể xoá"; 
            }
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
            ViewBag.Doctors = _unitOfWork.DoctorRepository.GetAll();
            ViewBag.Nurses = _unitOfWork.NurseRepository.GetAll();
            var patients = _unitOfWork.PatientRepository.GetAll();
            var patientdata = patients.Where(x => x.TrangThaiBenhAn == ETrangThaiBenhAn.dangchuatri).ToList();
            ViewBag.Patients = patientdata;
            ViewBag.Professions = _unitOfWork.ProfessionRepository.GetAll();
            ViewBag.Medicines = _unitOfWork.MedicineRepository.GetAll();
            var records = _unitOfWork.MedicalRecordRepository.GetAll();
            ViewBag.KhongTrieuChung = records.Count(r => r.TinhTrangBenhNhan == ETinhTrangBenhNhan.khongtrieuchung);
            ViewBag.CoTrieuChung = records.Count(r => r.TinhTrangBenhNhan == ETinhTrangBenhNhan.cotrieuchung);
            ViewBag.TroNang = records.Count(r => r.TinhTrangBenhNhan == ETinhTrangBenhNhan.tronang);
            // Tính toán số lượng MedicalVisit theo tháng và năm
            var visits = _unitOfWork.MedicalVisitRepository.GetAll();
            var visitData = visits
                .GroupBy(v => new { v.VisitDate.Year, v.VisitDate.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Count = g.Count()
                })
                .OrderBy(v => v.Year)
                .ThenBy(v => v.Month)
                .ToList();

            // Chuyển dữ liệu sang dạng JSON để sử dụng trong JavaScript
            ViewBag.VisitData = System.Text.Json.JsonSerializer.Serialize(visitData);
            return View(doctor);
		}

        public IActionResult DashBoard(int DoctorId)
        {
            var doctor = _unitOfWork.DoctorRepository.Get(dr => dr.DoctorId == DoctorId);
            var date = DateTime.Now.DayOfWeek.ToString(); 
            var wsList = _unitOfWork.WorkScheduleRepository.GetAll(u => (u.DoctorId1 == DoctorId || u.DoctorId2 == DoctorId || u.DoctorId3 == DoctorId) && u.DayOfWeek == date);
            foreach (var ws in wsList)
            {
                if (ws.DoctorId1 == DoctorId)
                {
                    if (ws.PhongKhamId != null)
                    {
                        var pk = _unitOfWork.PhongKhamRepository.Get(pk => pk.RoomId == ws.PhongKhamId);
                        pk.Patients = new List<Patient>();

                        var mrlist = _unitOfWork.MedicalRecordRepository.GetAll(mr => mr.TrangThaiBenhAn == ETrangThaiBenhAn.dangchuatri && mr.PhongKhamId == ws.PhongKhamId);
                        foreach (var mr in mrlist)
                        {
                            mr.Visits = _unitOfWork.MedicalVisitRepository.GetAll(v => v.MedicalRecordId == mr.MedicalRecordId).ToList();
                            if(mr.Visits.Count() > 0)
                            {
                                DateTime? d = mr.Visits.LastOrDefault().NgayTaiKham;
                                if (d?.Date.ToString("yyyy-MM-dd") == DateTime.Now.Date.ToString("yyyy-MM-dd"))
                                {
                                    pk.Patients.Add(_unitOfWork.PatientRepository.Get(pt => pt.PatientId == mr.PatientId));
                                }
                            }
                        }
                        ViewBag.CaTruc1 = pk;
                    }
                }

                if (ws.DoctorId2 == DoctorId)
                {
                    if (ws.PhongKhamId != null)
                    {
                        var pk = _unitOfWork.PhongKhamRepository.Get(pk => pk.RoomId == ws.PhongKhamId);
                        pk.Patients = new List<Patient>();

                        var mrlist = _unitOfWork.MedicalRecordRepository.GetAll(mr => mr.TrangThaiBenhAn == ETrangThaiBenhAn.dangchuatri && mr.PhongKhamId == ws.PhongKhamId);
                        foreach (var mr in mrlist)
                        {
                            mr.Visits = _unitOfWork.MedicalVisitRepository.GetAll(v => v.MedicalRecordId == mr.MedicalRecordId).ToList();
                            if (mr.Visits.Count() > 0)
                            {
                                DateTime? d = mr.Visits.LastOrDefault().NgayTaiKham;
                                if (d?.Date.ToString("yyyy-MM-dd") == DateTime.Now.Date.ToString("yyyy-MM-dd"))
                                {
                                    pk.Patients.Add(_unitOfWork.PatientRepository.Get(pt => pt.PatientId == mr.PatientId));
                                }
                            }
                        }
                        ViewBag.CaTruc2 = pk;
                    }
                }
                if (ws.DoctorId3 == DoctorId)
                {
                    if (ws.PhongKhamId != null)
                    {
                        var pk = _unitOfWork.PhongKhamRepository.Get(pk => pk.RoomId == ws.PhongKhamId);
                        pk.Patients = new List<Patient>();

                        var mrlist = _unitOfWork.MedicalRecordRepository.GetAll(mr => mr.TrangThaiBenhAn == ETrangThaiBenhAn.dangchuatri && mr.PhongKhamId == ws.PhongKhamId);
                        foreach (var mr in mrlist)
                        {
                            mr.Visits = _unitOfWork.MedicalVisitRepository.GetAll(v => v.MedicalRecordId == mr.MedicalRecordId).ToList();
                            if (mr.Visits.Count() > 0)
                            {
                                DateTime? d = mr.Visits.LastOrDefault().NgayTaiKham;
                                if (d?.Date.ToString("yyyy-MM-dd") == DateTime.Now.Date.ToString("yyyy-MM-dd"))
                                {
                                    pk.Patients.Add(_unitOfWork.PatientRepository.Get(pt => pt.PatientId == mr.PatientId));
                                }
                            }
                        }
                        ViewBag.CaTruc3 = pk;
                    }
                }
            }
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
        public FileResult DoctorExportExcel(string filename, IEnumerable<Doctor> list)
        {
            DataTable dataTable = new DataTable("Doctor");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("ID bác sĩ"),
                new DataColumn("Tên bác sĩ"),
                new DataColumn("CCCD"),
                new DataColumn("Giới tính"),
                new DataColumn("Tuổi"),
                new DataColumn("Chuyên khoa")
            });
            foreach (var doctor in list)
            {
                var profession=_unitOfWork.ProfessionRepository.Get(u=>u.ProfessionId==doctor.ProfessionId);
                string gioitinh = "";
                string truongkhoa = "";
                if (doctor.DoctorGender == EGender.male) gioitinh = "nam";
                else gioitinh = "nữ";
                if (doctor.IsTruongKhoa == false) truongkhoa = "không";
                else truongkhoa = "có";
                dataTable.Rows.Add(doctor.DoctorId, doctor.DoctorName, doctor.DoctorCCCD, gioitinh, doctor.DoctorAge, profession.ProfessionName);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
                }
            }
        }
        public FileResult DoctorExport(string ids)
        {
            var realid = ids.TrimEnd(',');
            var idList = realid.Split(',').Select(int.Parse).ToList();
            var list = new List<Doctor>();
            foreach (var id in idList)
            {
                list.Add(_unitOfWork.DoctorRepository.Get(u=>u.DoctorId == id));    
            }
            return DoctorExportExcel("danhsachbacsi.xlsx", list);
        }
    }
}
