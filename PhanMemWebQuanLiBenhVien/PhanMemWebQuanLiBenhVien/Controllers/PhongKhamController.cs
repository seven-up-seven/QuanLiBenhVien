using Microsoft.AspNetCore.Mvc;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;

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

        [HttpGet]
        public IActionResult Index()
        {
            var listPhongKham = _unitOfWork.PhongKhamRepository.GetAll();
            return View(listPhongKham);
            
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(PhongKham phongKham, int PhongKhamId)
        {
            if(ModelState.IsValid)
            {
                _unitOfWork.PhongKhamRepository.Add(phongKham);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Update()
        {
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }
    }
}
