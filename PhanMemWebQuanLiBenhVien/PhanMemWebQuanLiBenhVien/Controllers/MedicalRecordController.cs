using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;
using static System.Net.Mime.MediaTypeNames;

namespace PhanMemWebQuanLiBenhVien.Controllers
{
    [Route("MedicalRecord")]
    public class MedicalRecordController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public MedicalRecordController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            var listMedicalRecord = _unitOfWork.MedicalRecordRepository.GetAll();
            return View(listMedicalRecord);
        }


        [HttpGet("AdminCreate")]
        public IActionResult AdminCreate()
        {
            var patientList = _unitOfWork.PatientRepository.GetAll();

            // Tạo danh sách PatientId và tên để truyền vào ViewBag
            ViewBag.PatientList = patientList.Select(u =>
                new SelectListItem
                {
                    Text = $"ID: {u.PatientId} Tên: {u.Name}",
                    Value = u.PatientId.ToString()
                }).ToList();

            // Cung cấp thông tin tên bệnh nhân cho javascript
            ViewBag.PatientNames = patientList.ToDictionary(u => u.PatientId.ToString(), u => u.Name);

            return View();
        }
        [HttpPost("AdminCreate")]
        public IActionResult Create(MedicalRecord medicalRecord)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.MedicalRecordRepository.Add(medicalRecord);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("AdminCreate");
        }

        [HttpGet("DoctorCreate/{PatientId}")]
        public IActionResult DoctorCreate(int PatientId)
        {
            var patient = _unitOfWork.PatientRepository.Get(pt => pt.PatientId == PatientId); 
            ViewBag.Patient = patient;
            ViewBag.PatientId = PatientId;
            return View();
        }
        [HttpPost("DoctorCreate/{PatientId}")]
        public IActionResult DoctorCreate(MedicalRecord medicalRecord, int PatientId)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.MedicalRecordRepository.Add(medicalRecord);
                _unitOfWork.Save();
                return RedirectToAction("DoctorPatientDetail", "Doctor", new {PatientId = medicalRecord.PatientId});
            }
            return RedirectToAction("DoctorCreate");
        }



        [HttpGet("DoctorDetail/{MedicalRecordId}")]
        public IActionResult DoctorDetail(int MedicalRecordId)
        {
            var medicalRecord = _unitOfWork.MedicalRecordRepository.Get(mr => mr.MedicalRecordId == MedicalRecordId);
            ViewBag.MedicalVisits = _unitOfWork.MedicalVisitRepository.GetAll(mv => mv.MedicalRecordId == MedicalRecordId);
            return View("DoctorDetail", medicalRecord);
        }

        [HttpGet("AdminDetail/{MedicalRecordId}")]
        public IActionResult AdminDetail(int MedicalRecordId)
        {
            var medicalRecord = _unitOfWork.MedicalRecordRepository.Get(mr => mr.MedicalRecordId == MedicalRecordId);
            ViewBag.MedicalVisits = _unitOfWork.MedicalVisitRepository.GetAll(mv => mv.MedicalRecordId == MedicalRecordId);
            return View("AdminDetail", medicalRecord);
        }

        [HttpGet("Update/{MedicalRecordId}")]
        public IActionResult Update(int MedicalRecordId)
        {
            var medicalRecord = _unitOfWork.MedicalRecordRepository.Get(mr => mr.MedicalRecordId == MedicalRecordId);
            if (medicalRecord != null)
            {
                return View(medicalRecord);
            }
            return View();
        }
        [HttpPost("Update/{MedicalRecordId}")]
        public IActionResult Update(MedicalRecord medicalRecord)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.MedicalRecordRepository.Update(medicalRecord);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost("Delete/{MedicalRecordId}")]
        public IActionResult Delete(int MedicalRecordId)
        {
            var medicalRecord = _unitOfWork.MedicalRecordRepository.Get(mr => mr.MedicalRecordId == MedicalRecordId);
            if (medicalRecord != null)
            {
                _unitOfWork.MedicalRecordRepository.Remove(medicalRecord);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet("CreateMedicalVisit/{MedicalRecordId}")]
        public IActionResult CreateMedicalVisit(int MedicalRecordId)
        {
            ViewBag.Id = MedicalRecordId;
            return View();
        }
        [HttpPost("CreateMedicalVisit/{MedicalRecordId}")]
        public IActionResult CreateMedicalVisit(MedicalVisit medicalVisit)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.MedicalVisitRepository.Add(medicalVisit);
                _unitOfWork.Save();
                return RedirectToAction("DoctorDetail", new { MedicalRecordId = medicalVisit.MedicalRecordId });
            }
            return View();
        }
    }
}
