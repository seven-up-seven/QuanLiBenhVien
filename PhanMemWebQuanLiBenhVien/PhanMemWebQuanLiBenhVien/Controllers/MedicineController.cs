﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;
using PhanMemWebQuanLiBenhVien.Models.Models;

namespace PhanMemWebQuanLiBenhVien.Controllers
{
    public class MedicineController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        public MedicineController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var medicines = _unitOfWork.MedicineRepository.GetAll();
            var validMedicines = medicines.Where(m => m.Quantity > -1 && m.ExpiryDate >= DateTime.Now).ToList();
            var invalidMedicines = medicines.Where(m => m.Quantity <= -1 || m.ExpiryDate < DateTime.Now).ToList();
            var sortedMedicines = validMedicines.Concat(invalidMedicines).ToList();
            return View(sortedMedicines);
        }
        [HttpPost]
        public IActionResult Index(int SearchID, string SearchMedicineName)
        {
            var medicines = _unitOfWork.MedicineRepository.GetAll();
            var validMedicines = medicines.Where(m => m.Quantity > -1 && m.ExpiryDate >= DateTime.Now).ToList();
            var invalidMedicines = medicines.Where(m => m.Quantity <= -1 || m.ExpiryDate < DateTime.Now).ToList();
            var sortedMedicines = validMedicines.Concat(invalidMedicines).ToList();
            if (!string.IsNullOrEmpty(SearchMedicineName)) sortedMedicines = sortedMedicines.Where(u => u.Name.ToLower().Contains(SearchMedicineName.ToLower())).ToList();
            if (SearchID != null && SearchID != 0) sortedMedicines = sortedMedicines.Where(u => u.MedicineId == SearchID).ToList();
            return View(sortedMedicines);
        }


        //public IActionResult Details(int id)
        //{
        //    var medicine = _unitOfWork.MedicineRepository.GetById(id);
        //    if (medicine == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(medicine);
        //}

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Medicine medicine)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.MedicineRepository.Add(medicine);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(medicine);
        }

        public IActionResult Edit(int id)
        {
            var medicine = _unitOfWork.MedicineRepository.Get(u => u.MedicineId == id); 
            return View(medicine);
        }


        [HttpPost]
        public IActionResult Edit(Medicine medicine)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.MedicineRepository.Update(medicine);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(medicine);
        }


        public IActionResult Delete(int id)
        {
            var medicine = _unitOfWork.MedicineRepository.Get(mc => mc.MedicineId == id);
            if (medicine == null)
            {
                return NotFound();
            }
            _unitOfWork.MedicineRepository.Remove(medicine);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> KeDonThuoc(int MedicalVisitId)
        {
            var medicalVisit = _unitOfWork.MedicalVisitRepository.Get(mv => mv.VisitId == MedicalVisitId);
            if (medicalVisit == null)
            {
                return NotFound();
            }

            var medicalRecord = _unitOfWork.MedicalRecordRepository.Get(mr => mr.MedicalRecordId == medicalVisit.MedicalRecordId);
            if (medicalRecord == null)
            {
                return NotFound();
            }

            var true_user = (CustomedUser?)await _userManager.GetUserAsync(User);
            if (true_user == null)
            {
                return NotFound();
            }

            var doctor = _unitOfWork.DoctorRepository.Get(u => u.DoctorId == true_user.UserId);
            if (doctor == null)
            {
                return NotFound();
            }

            doctor.Profession = _unitOfWork.ProfessionRepository.Get(pf => pf.ProfessionId == doctor.ProfessionId);
            var patient = _unitOfWork.PatientRepository.Get(pt => pt.PatientId == medicalRecord.PatientId);
            if (patient == null)
            {
                return NotFound();
            }

            var idThuocs = medicalVisit.IdThuocs?.Split(',').Select(int.Parse).ToList();
            var soLuongs = medicalVisit.SoLuongThuocs?.Split(',').Select(int.Parse).ToList();
            if (idThuocs == null || soLuongs == null)
            {
                return NotFound();
            }

            var medicineList = idThuocs.Select(id => _unitOfWork.MedicineRepository.Get(m => m.MedicineId == id))
                                       .Zip(soLuongs, (medicine, qty) => new { Medicine = medicine, Quantity = qty })
                                       .ToList();

            ViewBag.doctor = doctor;
            ViewBag.patient = patient;
            ViewBag.medicine = medicineList;

            return View();
        }

        public IActionResult SearchMedicineByName(string name)
        {
            if (name == null)
            {
                TempData["error"] = "Nhập tên thuốc";
                return RedirectToAction("Index");
            }
            var thuocs = _unitOfWork.MedicineRepository.GetAll(u => u.Name.ToLower().StartsWith(name.ToLower()));
            return View("Index", thuocs);
        }

        public IActionResult TrichXuatThuoc(string ids)
        {
            if (string.IsNullOrEmpty(ids) || ids==",")
            {
                TempData["error"] = "Chọn ít nhất một thuốc";
                return RedirectToAction("Index");
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(ids, @"^[0-9,]+$"))
            {
                TempData["error"] = "ID thuốc đã nhập không hợp lệ";
                return RedirectToAction("Index");
            }

            var idList = ids.Split(',').Select(int.Parse).ToList();
            var medicines = new List<Medicine>(); 
            foreach(var id in idList)
            {
                var temp = _unitOfWork.MedicineRepository.Get(m => m.MedicineId == id); 
                if(temp == null)
                {
                    TempData["error"] = "ID thuốc đã nhập không hợp lệ";
                    return RedirectToAction("Index");
                }
                if(temp.Quantity <= -1)
                {
                    TempData["error"] = "Thuốc đã hết số lượng, vui lòng chọn thuốc khác";
                    return RedirectToAction("Index");
                }
                if (temp.ExpiryDate < DateTime.Now)
                {
                    TempData["error"] = "Thuốc đã hết hạn, vui lòng chọn thuốc khác";
                    return RedirectToAction("Index");
                }
                medicines.Add(temp); 
            }
            

            if (medicines == null || !medicines.Any() || medicines[0] == null)
            {
                TempData["error"] = "Không tìm thấy";
                return RedirectToAction("Index");
            }
           
            return View(medicines);
        }

        [HttpPost]
        public IActionResult SubmitQuantities(Dictionary<int, int> quantities)
        {
            foreach (var key in quantities.Keys)
            {
                var thuoc = _unitOfWork.MedicineRepository.Get(u => u.MedicineId == key);
                if (thuoc.Quantity < quantities[key])
                {
                    string ids = string.Join(",", quantities.Keys);

                    var idList = ids.Split(',').Select(int.Parse).ToList();
                    var medicines = new List<Medicine>();
                    foreach (var id in idList)
                    {
                        var temp = _unitOfWork.MedicineRepository.Get(m => m.MedicineId == id);
                        medicines.Add(temp);
                    }
                    TempData["error"] = "Số lượng không hợp lệ";    
                    return View("TrichXuatThuoc", medicines);

                }
                if (thuoc.Quantity == quantities[key])
                {
                    thuoc.Quantity = -1;
                }
                else
                {
                    thuoc.Quantity = thuoc.Quantity - quantities[key];
                }
                _unitOfWork.MedicineRepository.Update(thuoc);
            }
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }

}

