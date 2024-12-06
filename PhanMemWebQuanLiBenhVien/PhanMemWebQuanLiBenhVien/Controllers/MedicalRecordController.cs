using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;
using static PhanMemWebQuanLiBenhVien.Ultilities.Utilities;
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
            ViewBag.statuslist = Enum.GetValues(typeof(ETrangThaiDieuTri))
                                       .Cast<ETrangThaiDieuTri>()
                                       .Select(e => new SelectListItem
                                       {
                                           Value = e.ToString(),
                                           Text = e.ToString()
                                       }).ToList();
            var doctorList = _unitOfWork.DoctorRepository.GetAll()
                        .Select(d => new SelectListItem
                        {
                            Value = d.DoctorId.ToString(),
                            Text = d.DoctorName
                        }).ToList();
            ViewBag.doctorlist = doctorList;

            // Fetch and set the nurse list
            var nurseList = _unitOfWork.NurseRepository.GetAll()
                                .Select(n => new SelectListItem
                                {
                                    Value = n.NurseId.ToString(),
                                    Text = n.NurseName
                                }).ToList();
            ViewBag.nurselist = nurseList;

            // Fetch and set the PhongBenh list
            var phongBenhList = _unitOfWork.PhongBenhRepository.GetAll()
                                .Where(pb =>
                                {
                                    var patients = _unitOfWork.PatientRepository.GetAll();
                                    foreach(var patient in patients)
                                    {
                                        patient.MedicalRecords = _unitOfWork.MedicalRecordRepository.GetAll(mr => mr.PatientId == patient.PatientId).ToList();
                                    }
                                    var validPatients = patients.Where(patient =>
                                    {
                                        var medicalRecords = _unitOfWork.MedicalRecordRepository.GetAll(mr => mr.PatientId == patient.PatientId && mr.PhongBenhId == pb.RoomId).ToList();
                                        patient.MedicalRecords = (ICollection<MedicalRecord>?)medicalRecords;
                                        return patient.TrangThaiBenhAn == Ultilities.Utilities.ETrangThaiBenhAn.dangchuatri &&
                                               medicalRecords.LastOrDefault() != null &&
                                               medicalRecords.LastOrDefault().TrangThaiBenhAn == Ultilities.Utilities.ETrangThaiBenhAn.dangchuatri &&
                                               medicalRecords.LastOrDefault().TrangThaiDieuTri == Ultilities.Utilities.ETrangThaiDieuTri.noitru;
                                    }).ToList();

                                    return validPatients.Count < pb.NumberOfBeds;
                                })
                                .Select(pb => new SelectListItem
                                {
                                    Value = pb.RoomId.ToString(),
                                    Text = pb.Name
                                }).ToList();

            ViewBag.phongbenhlist = phongBenhList;


            // Fetch and set the PhongKham list
            var phongKhamList = _unitOfWork.PhongKhamRepository.GetAll()
                                .Select(pk => new SelectListItem
                                {
                                    Value = pk.RoomId.ToString(),
                                    Text = pk.Name
                                }).ToList();
            ViewBag.phongkhamlist = phongKhamList;
            return View();
        }
        [HttpPost("DoctorCreate/{PatientId}")]
        public IActionResult DoctorCreate(MedicalRecord medicalRecord, int PatientId)
        {
            if (ModelState.IsValid)
            {
                var patient = _unitOfWork.PatientRepository.Get(pt => pt.PatientId == PatientId);
                patient.TrangThaiBenhAn = ETrangThaiBenhAn.dangchuatri;
                medicalRecord.TrangThaiBenhAn = ETrangThaiBenhAn.dangchuatri; 
                _unitOfWork.PatientRepository.Update(patient);
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
            medicalRecord.Doctor = _unitOfWork.DoctorRepository.Get(dr => dr.DoctorId == medicalRecord.DoctorId);
            medicalRecord.Nurse = _unitOfWork.NurseRepository.Get(nr => nr.NurseId == medicalRecord.NurseId);
            medicalRecord.PhongBenh = _unitOfWork.PhongBenhRepository.Get(pb => pb.RoomId == medicalRecord.PhongBenhId);
            medicalRecord.PhongKham = _unitOfWork.PhongKhamRepository.Get(pk => pk.RoomId == medicalRecord.PhongKhamId);
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
            ViewBag.MedicalRecordId = MedicalRecordId;
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

        [HttpPost("CloseMedicalRecord/{MedicalRecordId}")]
        public IActionResult CloseMedicalRecord(int MedicalRecordId)
        {
            var medicalRecord = _unitOfWork.MedicalRecordRepository.Get(mr => mr.MedicalRecordId == MedicalRecordId);
            if (medicalRecord != null)
            {
                medicalRecord.TrangThaiBenhAn = ETrangThaiBenhAn.ketthucchuatri;
                var patient = _unitOfWork.PatientRepository.Get(pt => pt.PatientId == medicalRecord.PatientId);
                patient.TrangThaiBenhAn = ETrangThaiBenhAn.ketthucchuatri;
                _unitOfWork.PatientRepository.Update(patient);
                _unitOfWork.MedicalRecordRepository.Update(medicalRecord);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        
    }
}
