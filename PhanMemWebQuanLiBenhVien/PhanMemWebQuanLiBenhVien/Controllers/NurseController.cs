using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhanMemWebQuanLiBenhVien.DataAccess;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;
using System.Numerics;
using static PhanMemWebQuanLiBenhVien.Ultilities.Utilities;

namespace PhanMemWebQuanLiBenhVien.Controllers
{
    public class NurseController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private string wwwroot;
        private IWebHostEnvironment _webHostEnvironment;
        private ApplicationDbContext _db;
        private UserManager<IdentityUser> _userManager;
        public NurseController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _db = db;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var NurseList = _unitOfWork.NurseRepository.GetAll();
            return View(NurseList);
        }
        [HttpPost]
        public IActionResult Index(string SearchNurseName, string SearchNurseCCCD, int ProfessionId)
        {
            var nurselist = _unitOfWork.NurseRepository.GetAll();
            if (!string.IsNullOrEmpty(SearchNurseName)) nurselist = nurselist.Where(u => u.NurseName.ToLower().Contains(SearchNurseName.ToLower()));
            if (!string.IsNullOrEmpty(SearchNurseCCCD)) nurselist = nurselist.Where(u => u.NurseCCCD.ToLower().Contains(SearchNurseCCCD.ToLower()));
            return View(nurselist);
        }

        public IActionResult Create()
        {
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
        [HttpPost]
        public IActionResult Create(Nurse nurse, IFormFile? NurseImg)
        {
            if (ModelState.IsValid)
            {
                Doctor doctor = null;
                Patient patient = null;
                GlobalFunctions globalfunction = new GlobalFunctions(_unitOfWork, doctor, nurse, patient);
                if (globalfunction.ExistDuplicateCCCD())
                {
                    TempData["error"] = "CCCD đã tồn tại!";
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
                    if (NurseImg != null)
                    {
                        string filename = Path.GetFileNameWithoutExtension(NurseImg.FileName) + Path.GetExtension(NurseImg.FileName);
                        string filepath = Path.Combine(wwwroot, @"images\");
                        using (var filestream = new FileStream(Path.Combine(filepath, filename), FileMode.Create))
                        {
                            NurseImg.CopyTo(filestream);
                        }
                        nurse.NurseImgURL = @"\images\" + filename;
                    }
                    else nurse.NurseImgURL = "";
                    _unitOfWork.NurseRepository.Add(nurse);
                    _unitOfWork.Save();
                    TempData["success"] = "Thêm y tá mới thành công!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
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
        public IActionResult Update(int NurseId)
        {
            var genderList = Enum.GetValues(typeof(EGender))
            .Cast<EGender>()
            .Select(gender => new SelectListItem
            {
                Value = gender.ToString(),
                Text = gender.ToString()
            }).ToList();
            ViewBag.Genders = genderList;
            var nurse=_unitOfWork.NurseRepository.Get(u=>u.NurseId == NurseId);
            return View(nurse);
        }
        [HttpPost]
        public IActionResult Update(Nurse nurse, IFormFile? NurseImg)
        {
            if (ModelState.IsValid)
            {
                wwwroot = _webHostEnvironment.WebRootPath;
                if (NurseImg != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(NurseImg.FileName) + Path.GetExtension(NurseImg.FileName);
                    string filepath = Path.Combine(wwwroot, @"images\");
                    using (var filestream = new FileStream(Path.Combine(filepath, filename), FileMode.Create))
                    {
                        NurseImg.CopyTo(filestream);
                    }
                    nurse.NurseImgURL = @"\images\" + filename;
                }
                _unitOfWork.NurseRepository.Update(nurse);
                _unitOfWork.Save();
                TempData["success"] = "Cập nhật y tá thành công!";
                if (User.IsInRole("Nurse")) return RedirectToAction("NurseHomePage", new { NurseId = nurse.NurseId });
                return RedirectToAction("Index");
            }
            else
            {
                var genderList = Enum.GetValues(typeof(EGender))
                .Cast<EGender>()
                .Select(gender => new SelectListItem
                {
                    Value = gender.ToString(),
                    Text = gender.ToString()
                }).ToList();
                ViewBag.Genders = genderList;
                return View(nurse);
            }
        }
        public async Task<IActionResult> Delete(int NurseId)
        {
            var nurse = _unitOfWork.NurseRepository.Get(u => u.NurseId == NurseId);
            wwwroot = _webHostEnvironment.WebRootPath;
            if (!string.IsNullOrEmpty(nurse.NurseImgURL))
            {
                var oldpath = Path.Combine(wwwroot, nurse.NurseImgURL.TrimStart('\\'));
                if (System.IO.File.Exists(oldpath)) System.IO.File.Delete(oldpath);
            }
            var user = _db.customedUsers.FirstOrDefault(u => (u.UserId == NurseId && u.UserRole == ERole.nurse));
            if (user!=null) _userManager.DeleteAsync(user).GetAwaiter().GetResult();
            _unitOfWork.NurseRepository.Remove(nurse);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        public IActionResult Detail(int NurseId)
        {
            var nurse = _unitOfWork.NurseRepository.Get(n => n.NurseId == NurseId);
            nurse.PatientList = new List<Patient>();
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
                    if (lastMedicalRecord.NurseId == nurse.NurseId)
                    {
                        lastMedicalRecord.PhongBenh = _unitOfWork.PhongBenhRepository.Get(pb => pb.RoomId == lastMedicalRecord.PhongBenhId);
                        nurse.PatientList.Add(patient);
                    }
                }
            }
            return View(nurse);
        }
        public IActionResult NurseHomePage(int NurseId)
        {
            var nurse= _unitOfWork.NurseRepository.Get(u=>u.NurseId == NurseId);
            return View(nurse);
        }
        public IActionResult NursePatients(int NurseId)
        {
            var nurse = _unitOfWork.NurseRepository.Get(n => n.NurseId == NurseId);
            nurse.PatientList = new List<Patient>();
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
                    if (lastMedicalRecord.NurseId == nurse.NurseId)
                    {
                        lastMedicalRecord.PhongBenh = _unitOfWork.PhongBenhRepository.Get(pb => pb.RoomId == lastMedicalRecord.PhongBenhId);
                        nurse.PatientList.Add(patient);
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
            ViewBag.nurse = nurse;
            return View(nurse.PatientList);
        }
        [HttpPost]
        public IActionResult NursePatients(int NurseId, string SearchPatientName, string SearchPatientCCCD, string TinhTrangBenhNhan)
        {
            var nurse = _unitOfWork.NurseRepository.Get(n => n.NurseId == NurseId);
            nurse.PatientList = new List<Patient>();
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
                    if (TinhTrangBenhNhan != "NoFilter")
                    {
                        if (lastMedicalRecord.NurseId == nurse.NurseId && lastMedicalRecord.TinhTrangBenhNhan.ToString()==TinhTrangBenhNhan)
                        {
                            lastMedicalRecord.PhongBenh = _unitOfWork.PhongBenhRepository.Get(pb => pb.RoomId == lastMedicalRecord.PhongBenhId);
                            nurse.PatientList.Add(patient);
                        }
                    }
                    else
                    {
                        if (lastMedicalRecord.NurseId == nurse.NurseId)
                        {
                            lastMedicalRecord.PhongBenh = _unitOfWork.PhongBenhRepository.Get(pb => pb.RoomId == lastMedicalRecord.PhongBenhId);
                            nurse.PatientList.Add(patient);
                        }
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
            ViewBag.nurse = nurse;
            return View(nurse.PatientList);
        }
    }
}
