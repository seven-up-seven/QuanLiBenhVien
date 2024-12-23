using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using PhanMemWebQuanLiBenhVien.DataAccess;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;
using static PhanMemWebQuanLiBenhVien.Ultilities.Utilities;

namespace PhanMemWebQuanLiBenhVien.Controllers
{
	[Route("PhongBenh")]
	public class PhongBenhController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
        private ApplicationDbContext _db;
        private UserManager<IdentityUser> _userManager;
		public PhongBenhController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, ApplicationDbContext db)
		{
			_unitOfWork = unitOfWork;
            _userManager = userManager;
            _db = db;
		}

		[HttpGet("Index")]
        public IActionResult Index()
        {
            var listPhongBenh = _unitOfWork.PhongBenhRepository.GetAll();
            var medicalRecords = _unitOfWork.MedicalRecordRepository.GetAll();
            var listBenhNhan = _unitOfWork.PatientRepository.GetAll();
            foreach (var phong in listPhongBenh)
            {
                phong.Patients = new List<Patient>();
                foreach (var patient in listBenhNhan)
                {
                    patient.MedicalRecords = (ICollection<MedicalRecord>?)_unitOfWork.MedicalRecordRepository.GetAll(mr => mr.PatientId == patient.PatientId && mr.PhongBenhId == phong.RoomId);
                    if (patient.TrangThaiBenhAn == Ultilities.Utilities.ETrangThaiBenhAn.dangchuatri &&
                        patient.MedicalRecords != null &&
                        patient.MedicalRecords.LastOrDefault() != null &&
                        patient.MedicalRecords.LastOrDefault().TrangThaiBenhAn == Ultilities.Utilities.ETrangThaiBenhAn.dangchuatri &&
                        patient.MedicalRecords.LastOrDefault().TrangThaiDieuTri == Ultilities.Utilities.ETrangThaiDieuTri.noitru)
                    {
                        phong.Patients.Add(patient);
                    }
                }
            }
            ViewBag.Professions = _unitOfWork.ProfessionRepository.GetAll().Select(u => new SelectListItem
            {
                Text = u.ProfessionName,
                Value = u.ProfessionId.ToString()
            });
            return View(listPhongBenh);
        }
        [HttpPost("Index")]
        public IActionResult Index(string SearchName, string SearchProfession)
        {
            var listPhongBenh = _unitOfWork.PhongBenhRepository.GetAll();
            var medicalRecords = _unitOfWork.MedicalRecordRepository.GetAll();
            var listBenhNhan = _unitOfWork.PatientRepository.GetAll();
            if (!string.IsNullOrEmpty(SearchName)) listPhongBenh = listPhongBenh.Where(u => u.Name.ToLower().Contains(SearchName.ToLower()));
            if (SearchProfession != "NoFilter") listPhongBenh = listPhongBenh.Where(u => u.ProfessionId.ToString() == SearchProfession);
            foreach (var phong in listPhongBenh)
            {
                phong.Patients = new List<Patient>();
                foreach (var patient in listBenhNhan)
                {
                    patient.MedicalRecords = (ICollection<MedicalRecord>?)_unitOfWork.MedicalRecordRepository.GetAll(mr => mr.PatientId == patient.PatientId && mr.PhongBenhId == phong.RoomId);
                    if (patient.TrangThaiBenhAn == Ultilities.Utilities.ETrangThaiBenhAn.dangchuatri &&
                        patient.MedicalRecords != null &&
                        patient.MedicalRecords.LastOrDefault() != null &&
                        patient.MedicalRecords.LastOrDefault().TrangThaiBenhAn == Ultilities.Utilities.ETrangThaiBenhAn.dangchuatri &&
                        patient.MedicalRecords.LastOrDefault().TrangThaiDieuTri == Ultilities.Utilities.ETrangThaiDieuTri.noitru)
                    {
                        phong.Patients.Add(patient);
                    }
                }
            }
            ViewBag.Professions = _unitOfWork.ProfessionRepository.GetAll().Select(u => new SelectListItem
            {
                Text = u.ProfessionName,
                Value = u.ProfessionId.ToString()
            });
            return View(listPhongBenh);
        }

        [HttpGet("Create")]
		public IActionResult Create()
		{
			ViewBag.Professions = _unitOfWork.ProfessionRepository.GetAll().Select(u => new SelectListItem
			{
				Text = u.ProfessionName,
				Value = u.ProfessionId.ToString()
			});
			return View();
		}
		[HttpPost("Create")]
		public IActionResult Create(PhongBenh phongBenh)
		{
			if (ModelState.IsValid)
			{
				_unitOfWork.PhongBenhRepository.Add(phongBenh);
				_unitOfWork.Save();
                ActivityTrackingFunction trackingtool = new ActivityTrackingFunction(_db, _unitOfWork);
                var tmpuser = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
                var truetmp_user = (CustomedUser)tmpuser;
                trackingtool.TrackingActivity(truetmp_user.UserId, truetmp_user.UserName, ETypeOfActivity.them, truetmp_user.UserRole, phongBenh.RoomId, phongBenh, null);
                return RedirectToAction("Index");
			}
            ViewBag.Professions = _unitOfWork.ProfessionRepository.GetAll().Select(u => new SelectListItem
            {
                Text = u.ProfessionName,
                Value = u.ProfessionId.ToString()
            });
            return View();
		}


        [HttpGet("Detail/{PhongBenhId}")]
        public IActionResult Detail(int PhongBenhId)
        {
            var medicalRecords = _unitOfWork.MedicalRecordRepository.GetAll(mr => mr.PhongBenhId == PhongBenhId);
            var phongBenh = _unitOfWork.PhongBenhRepository.Get(pb => pb.RoomId == PhongBenhId);
			phongBenh.Profession = _unitOfWork.ProfessionRepository.Get(pf => pf.ProfessionId == phongBenh.ProfessionId); 
            if (phongBenh == null)
            {
                return NotFound(); // or handle the null case appropriately
            }

            phongBenh.Patients = new List<Patient>();
            var listBenhNhan = _unitOfWork.PatientRepository.GetAll();
            foreach (var patient in listBenhNhan)
            {
                patient.MedicalRecords = (ICollection<MedicalRecord>?)_unitOfWork.MedicalRecordRepository.GetAll(mr => mr.PatientId == patient.PatientId && mr.PhongBenhId == phongBenh.RoomId);
                if (patient.TrangThaiBenhAn == Ultilities.Utilities.ETrangThaiBenhAn.dangchuatri &&
                    patient.MedicalRecords != null &&
                    patient.MedicalRecords.LastOrDefault() != null &&
                    patient.MedicalRecords.LastOrDefault().TrangThaiBenhAn == Ultilities.Utilities.ETrangThaiBenhAn.dangchuatri &&
                    patient.MedicalRecords.LastOrDefault().TrangThaiDieuTri == Ultilities.Utilities.ETrangThaiDieuTri.noitru)
                {
                    phongBenh.Patients.Add(patient);
                }
            }

            return View(phongBenh);
        }




        [HttpGet("Update/{PhongBenhId}")]
		public IActionResult Update(int PhongBenhId)
		{
			var phongBenh = _unitOfWork.PhongBenhRepository.Get(pb => pb.RoomId == PhongBenhId);
			if (phongBenh != null)
			{
                ViewBag.Professions = _unitOfWork.ProfessionRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.ProfessionName,
                    Value = u.ProfessionId.ToString()
                });
                return View(phongBenh);
			}
			return View(null);
		}
		[HttpPost("Update/{PhongBenhId}")]
		public IActionResult Update(PhongBenh phongBenh)
		{
			if (ModelState.IsValid)
			{
                var old = _unitOfWork.PhongBenhRepository.Get(pb => pb.RoomId == phongBenh.RoomId);
                old.Patients = new List<Patient>(); 
                var mrs = _unitOfWork.MedicalRecordRepository.GetAll(mr => mr.PhongBenhId == phongBenh.RoomId && mr.TrangThaiBenhAn == Ultilities.Utilities.ETrangThaiBenhAn.dangchuatri); 
                foreach(var mr in mrs)
                {
                    old.Patients.Add(_unitOfWork.PatientRepository.Get(pt => pt.PatientId == mr.PatientId)); 
                }
                if(phongBenh.NumberOfBeds < old.Patients.Count() || phongBenh.NumberOfBeds <=0)
                {
                    TempData["error"] = "Số giường không thể bé hơn số bệnh nhân hiện tại";
                    ViewBag.Professions = _unitOfWork.ProfessionRepository.GetAll().Select(u => new SelectListItem
                    {
                        Text = u.ProfessionName,
                        Value = u.ProfessionId.ToString()
                    });
                    return View(phongBenh);
                }
                else
                {
                    var details = new List<string>();
                    var objFromDb = _unitOfWork.PhongBenhRepository.Get(u => u.RoomId == phongBenh.RoomId);
                    if (phongBenh.NumberOfBeds != objFromDb.NumberOfBeds) details.Add($"Số giường: {objFromDb.NumberOfBeds} -> {phongBenh.NumberOfBeds}");
                    if (phongBenh.Name != objFromDb.Name) details.Add($"Tên: {objFromDb.Name} -> {phongBenh.Name}");
                    if (phongBenh.ProfessionId != objFromDb.ProfessionId) details.Add($"Chuyên khoa: {_unitOfWork.ProfessionRepository.Get(u => u.ProfessionId == objFromDb.ProfessionId).ProfessionName} -> {_unitOfWork.ProfessionRepository.Get(u => u.ProfessionId == phongBenh.ProfessionId).ProfessionName}");
                    ActivityTrackingFunction trackingtool = new ActivityTrackingFunction(_db, _unitOfWork);
                    var tmpuser = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
                    var truetmp_user = (CustomedUser)tmpuser;
                    trackingtool.TrackingActivity(truetmp_user.UserId, truetmp_user.UserName, ETypeOfActivity.sua, truetmp_user.UserRole, phongBenh.RoomId, phongBenh, details);
                    _unitOfWork.PhongBenhRepository.Update(phongBenh);
                    _unitOfWork.Save();
                    TempData["success"] = "Cập nhật thông tin phòng bệnh thành công"; 
                    return RedirectToAction("Index");
                }
			}
			return View();
		}

		[HttpPost("Delete/{PhongBenhId}")]
		public IActionResult Delete(int PhongBenhId)
		{
            var phongBenh = _unitOfWork.PhongBenhRepository.Get(pb => pb.RoomId == PhongBenhId);
            phongBenh.Patients = new List<Patient>(); 
            var mrl = _unitOfWork.MedicalRecordRepository.GetAll(m => m.PhongBenhId == PhongBenhId && m.TrangThaiBenhAn == Ultilities.Utilities.ETrangThaiBenhAn.dangchuatri); 
            foreach(var mr in mrl)
            {
                phongBenh.Patients.Add(_unitOfWork.PatientRepository.Get(pt => pt.PatientId == mr.PatientId)); 
            }
            if(phongBenh.Patients == null || phongBenh.Patients.Count() == 0)
            {
                var mr_fk = _unitOfWork.MedicalRecordRepository.GetAll(m => m.PhongBenhId == PhongBenhId); 
                if (mr_fk != null)
                {
                    _unitOfWork.MedicalRecordRepository.RemoveRange(mr_fk);
                }
                _unitOfWork.PhongBenhRepository.Remove(phongBenh);
                _unitOfWork.Save();
                TempData["success"] = "Xóa phòng bệnh thành công";
                ActivityTrackingFunction trackingtool = new ActivityTrackingFunction(_db, _unitOfWork);
                var tmpuser = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
                var truetmp_user = (CustomedUser)tmpuser;
                trackingtool.TrackingActivity(truetmp_user.UserId, truetmp_user.UserName, ETypeOfActivity.xoa, truetmp_user.UserRole, phongBenh.RoomId, phongBenh, null);
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Phòng bệnh đang có người không thể xoá";
                return RedirectToAction("Index"); 
            }
        }
	}
}
