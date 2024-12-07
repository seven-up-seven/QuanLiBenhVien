using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;

namespace PhanMemWebQuanLiBenhVien.Controllers
{
	[Route("PhongBenh")]
	public class PhongBenhController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public PhongBenhController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
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
				return RedirectToAction("Index");
			}

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
				_unitOfWork.PhongBenhRepository.Update(phongBenh);
				_unitOfWork.Save();
				return RedirectToAction("Index");
			}
			return View();
		}

		[HttpPost("Delete/{PhongBenhId}")]
		public IActionResult Delete(int PhongBenhId)
		{
			var phongBenh = _unitOfWork.PhongBenhRepository.Get(pb => pb.RoomId == PhongBenhId);
			if (phongBenh != null)
			{
				_unitOfWork.PhongBenhRepository.Remove(phongBenh);
				_unitOfWork.Save();
				return RedirectToAction("Index");
			}
			return View();
		}
	}
}
