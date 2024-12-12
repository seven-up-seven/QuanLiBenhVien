using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;
using System.Linq;

namespace PhanMemWebQuanLiBenhVien.Controllers
{
    [Route("PhongKham")]
    public class PhongKhamController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public PhongKhamController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            var listPhongKham = _unitOfWork.PhongKhamRepository.GetAll();
            var listBenhNhan = _unitOfWork.PatientRepository.GetAll();
            var medicalRecords = _unitOfWork.MedicalRecordRepository.GetAll();

            foreach (var phong in listPhongKham)
            {
                phong.Patients = new List<Patient>();
            }

            foreach (var patient in listBenhNhan)
            {
                patient.MedicalRecords = (ICollection<MedicalRecord>?)_unitOfWork.MedicalRecordRepository.GetAll(mr => mr.PatientId == patient.PatientId);
                var lastMedicalRecord = patient.MedicalRecords?.LastOrDefault();

                if (patient.TrangThaiBenhAn == Ultilities.Utilities.ETrangThaiBenhAn.dangchuatri &&
                    lastMedicalRecord != null &&
                    lastMedicalRecord.TrangThaiBenhAn == Ultilities.Utilities.ETrangThaiBenhAn.dangchuatri &&
                    lastMedicalRecord.TrangThaiDieuTri == Ultilities.Utilities.ETrangThaiDieuTri.ngoaitru)
                {
                    var phongKham = listPhongKham.FirstOrDefault(pk => pk.RoomId == lastMedicalRecord.PhongKhamId);
                    if (phongKham != null && !phongKham.Patients.Any(p => p.PatientId == patient.PatientId))
                    {
                        phongKham.Patients.Add(patient);
                    }
                }
            }
            ViewBag.Professions = _unitOfWork.ProfessionRepository.GetAll().Select(u => new SelectListItem
            {
                Text = u.ProfessionName,
                Value = u.ProfessionId.ToString()
            });
            return View(listPhongKham);
        }
        [HttpPost("Index")]
        public IActionResult Index(string SearchName, string SearchProfession)
        {
            var listPhongKham = _unitOfWork.PhongKhamRepository.GetAll();
            var listBenhNhan = _unitOfWork.PatientRepository.GetAll();
            var medicalRecords = _unitOfWork.MedicalRecordRepository.GetAll();
            if (!string.IsNullOrEmpty(SearchName)) listPhongKham = listPhongKham.Where(u => u.Name.ToLower().Contains(SearchName.ToLower()));
            if (SearchProfession != "NoFilter") listPhongKham = listPhongKham.Where(u => u.ProfessionId.ToString() == SearchProfession);
            foreach (var phong in listPhongKham)
            {
                phong.Patients = new List<Patient>();
            }

            foreach (var patient in listBenhNhan)
            {
                patient.MedicalRecords = (ICollection<MedicalRecord>?)_unitOfWork.MedicalRecordRepository.GetAll(mr => mr.PatientId == patient.PatientId);
                var lastMedicalRecord = patient.MedicalRecords?.LastOrDefault();

                if (patient.TrangThaiBenhAn == Ultilities.Utilities.ETrangThaiBenhAn.dangchuatri &&
                    lastMedicalRecord != null &&
                    lastMedicalRecord.TrangThaiBenhAn == Ultilities.Utilities.ETrangThaiBenhAn.dangchuatri &&
                    lastMedicalRecord.TrangThaiDieuTri == Ultilities.Utilities.ETrangThaiDieuTri.ngoaitru)
                {
                    var phongKham = listPhongKham.FirstOrDefault(pk => pk.RoomId == lastMedicalRecord.PhongKhamId);
                    if (phongKham != null && !phongKham.Patients.Any(p => p.PatientId == patient.PatientId))
                    {
                        phongKham.Patients.Add(patient);
                    }
                }
            }
            ViewBag.Professions = _unitOfWork.ProfessionRepository.GetAll().Select(u => new SelectListItem
            {
                Text = u.ProfessionName,
                Value = u.ProfessionId.ToString()
            });
            return View(listPhongKham);
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
        public IActionResult Create(PhongKham phongKham)
        {
            if(ModelState.IsValid)
            {
                _unitOfWork.PhongKhamRepository.Add(phongKham);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View();
        }


        [HttpGet("Detail/{PhongKhamId}")]
        public IActionResult Detail(int PhongKhamId)
        {
            var phongKham = _unitOfWork.PhongKhamRepository.Get(pk => pk.RoomId == PhongKhamId);
            var listBenhNhan = _unitOfWork.PatientRepository.GetAll();
			phongKham.Profession = _unitOfWork.ProfessionRepository.Get(pf => pf.ProfessionId == phongKham.ProfessionId);
			//var medicalRecords = _unitOfWork.MedicalRecordRepository.GetAll(pt => pt.PhongKhamId == phongKham.RoomId && pt.TrangThaiDieuTri == Ultilities.Utilities.ETrangThaiDieuTri.ngoaitru && pt.Patient.TrangThaiBenhAn == Ultilities.Utilities.ETrangThaiBenhAn.dangchuatri);
			phongKham.Patients = new List<Patient>();
            foreach (var patient in listBenhNhan)
            {
                patient.MedicalRecords = (ICollection<MedicalRecord>?)_unitOfWork.MedicalRecordRepository.GetAll(mr => mr.PatientId == patient.PatientId && mr.PhongKhamId == phongKham.RoomId);
                if (patient.TrangThaiBenhAn == Ultilities.Utilities.ETrangThaiBenhAn.dangchuatri &&
                    patient.MedicalRecords != null &&
                    patient.MedicalRecords.LastOrDefault() != null &&
                    patient.MedicalRecords.LastOrDefault().TrangThaiBenhAn == Ultilities.Utilities.ETrangThaiBenhAn.dangchuatri &&
                    patient.MedicalRecords.LastOrDefault().TrangThaiDieuTri == Ultilities.Utilities.ETrangThaiDieuTri.ngoaitru)
                {
                    phongKham.Patients.Add(patient);
                }

            }

            return View(phongKham);
        }


        [HttpGet("Update/{PhongKhamId}")]
        public IActionResult Update(int PhongKhamId)
        {
            var phongKham = _unitOfWork.PhongKhamRepository.Get(pk => pk.RoomId == PhongKhamId);
            ViewBag.Professions = _unitOfWork.ProfessionRepository.GetAll().Select(u => new SelectListItem
            {
                Text = u.ProfessionName,
                Value = u.ProfessionId.ToString()
            });
            if (phongKham!=null)
            {
                return View(phongKham);
            }
            return View(null);
        }
        [HttpPost("Update/{PhongKhamId}")]
        public IActionResult Update(PhongKham phongKham)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.PhongKhamRepository.Update(phongKham);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost("Delete/{PhongKhamId}")]
        public IActionResult Delete(int PhongKhamId)
        {
            var phongKham = _unitOfWork.PhongKhamRepository.Get(pk => pk.RoomId == PhongKhamId);
			if (phongKham != null)
            {
                _unitOfWork.PhongKhamRepository.Remove(phongKham);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
