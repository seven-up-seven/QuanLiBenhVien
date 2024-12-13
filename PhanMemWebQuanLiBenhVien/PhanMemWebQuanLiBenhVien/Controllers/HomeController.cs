using Microsoft.AspNetCore.Mvc;
using PhanMemWebQuanLiBenhVien.Models;
using System.Diagnostics;
using PhanMemWebQuanLiBenhVien.DataAccess;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using static PhanMemWebQuanLiBenhVien.Ultilities.Utilities;
using PhanMemWebQuanLiBenhVien.DataAccess.Migrations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PhanMemWebQuanLiBenhVien.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            ViewBag.Doctors = _unitOfWork.DoctorRepository.GetAll();
            ViewBag.Nurses = _unitOfWork.NurseRepository.GetAll();
            var patients = _unitOfWork.PatientRepository.GetAll();
            var patientdata= patients.Where(x => x.TrangThaiBenhAn==ETrangThaiBenhAn.dangchuatri).ToList();
            ViewBag.Patients = patientdata;
            ViewBag.Professions=_unitOfWork.ProfessionRepository.GetAll();
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
            return View();
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
