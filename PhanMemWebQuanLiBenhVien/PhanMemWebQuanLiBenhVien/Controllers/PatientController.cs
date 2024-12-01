using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;
using static PhanMemWebQuanLiBenhVien.Ultilities.Utilities;

namespace PhanMemWebQuanLiBenhVien.Controllers
{
    public class PatientController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public PatientController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var PatientList = _unitOfWork.PatientRepository.GetAll();
            foreach (var patient in PatientList)
            {
				if (patient.DoctorId != null) patient.Doctor = _unitOfWork.DoctorRepository.Get(u => u.DoctorId == patient.DoctorId);
				if (patient.NurseId != null) patient.Nurse = _unitOfWork.NurseRepository.Get(u => u.NurseId == patient.NurseId);
				if (patient.PhongBenhId != null) patient.PhongBenh = _unitOfWork.PhongBenhRepository.Get(u => u.RoomId == patient.PhongBenhId);
				if (patient.PhongKhamId != null) patient.PhongKham = _unitOfWork.PhongKhamRepository.Get(u => u.RoomId == patient.PhongKhamId);
			}
            return View(PatientList);
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
            ViewBag.BHYT = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {Text="Có", Value="true"},
                new SelectListItem {Text="Không", Value="false"} 
            }, "Value", "Text");
            var statuslist=Enum.GetValues(typeof(ETrangThaiDieuTri))
            .Cast<ETrangThaiDieuTri>()
            .Where(e=>e==ETrangThaiDieuTri.chikham || e==ETrangThaiDieuTri.nhapvien)
            .Select(e=>new SelectListItem
            {
                Value=e.ToString(),
                Text=e.ToString()
            }).ToList();
            ViewBag.statuslist=statuslist;
            var phongkhamlist = _unitOfWork.PhongKhamRepository.GetAll().Select(p => new SelectListItem
            {
                Text=p.Name,
                Value=p.RoomId.ToString()
            });
            ViewBag.phongkhamlist=phongkhamlist;
            var doctorlist = _unitOfWork.DoctorRepository.GetAll().Select(p => new SelectListItem
            {
                Text = p.DoctorName,
                Value = p.DoctorId.ToString()
            });
            ViewBag.doctorlist= doctorlist;
            var phongbenhlist = _unitOfWork.PhongBenhRepository.GetAll().Select(p => new SelectListItem
            { 
                Text=p.Name,
                Value=p.RoomId.ToString()   
            });
            ViewBag.phongbenhlist=phongbenhlist;
            var nurselist = _unitOfWork.NurseRepository.GetAll().Select(p => new SelectListItem
            {
                Text=p.NurseName,
                Value=p.NurseId.ToString()
            });
            ViewBag.nurselist=nurselist;    
            return View();
        }
        [HttpPost]
        public IActionResult Create(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.PatientRepository.Add(patient);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Bệnh nhân không hợp lệ";
                return RedirectToAction("Create");
            }
        }
        public IActionResult Update()
        {
            return View();
        }
        public IActionResult Delete(int PatientId)
        {
            var patient = _unitOfWork.PatientRepository.Get(u => u.PatientId == PatientId);
            _unitOfWork.PatientRepository.Remove(patient);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        public IActionResult Detail(int PatientId)
        {
            var patient = _unitOfWork.PatientRepository.Get(u => u.PatientId == PatientId);
			if (patient.DoctorId != null) patient.Doctor = _unitOfWork.DoctorRepository.Get(u => u.DoctorId == patient.DoctorId);
			if (patient.NurseId != null) patient.Nurse = _unitOfWork.NurseRepository.Get(u => u.NurseId == patient.NurseId);
			if (patient.PhongBenhId != null) patient.PhongBenh = _unitOfWork.PhongBenhRepository.Get(u => u.RoomId == patient.PhongBenhId);
			if (patient.PhongKhamId != null) patient.PhongKham = _unitOfWork.PhongKhamRepository.Get(u => u.RoomId == patient.PhongKhamId);
			return View(patient);
        }
    }
}
