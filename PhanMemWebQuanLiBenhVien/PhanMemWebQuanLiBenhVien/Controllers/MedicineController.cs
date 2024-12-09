using Microsoft.AspNetCore.Mvc;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models.Models;

namespace PhanMemWebQuanLiBenhVien.Controllers
{
    public class MedicineController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MedicineController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var medicines = _unitOfWork.MedicineRepository.GetAll();
            return View(medicines);
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

        //public IActionResult Edit(int id)
        //{
        //    var medicine = _unitOfWork.MedicineRepository.GetById(id);
        //    if (medicine == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(medicine);
        //}

        //[HttpPost]
        //public IActionResult Edit(Medicine medicine)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.MedicineRepository.Update(medicine);
        //        _unitOfWork.Save();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(medicine);
        //}

        
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

        public IActionResult KeDonThuoc(int MedicalVisitId)
        {
            
            return View();
        }

    }

}

