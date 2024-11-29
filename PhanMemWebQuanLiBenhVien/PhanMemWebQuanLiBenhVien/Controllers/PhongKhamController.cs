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


        [HttpGet]
        public IActionResult Update(int PhongKhamId)
        {
            var phongKham = _unitOfWork.PhongKhamRepository.Get(pk => pk.RoomId == PhongKhamId);
            if(phongKham != null)
            {
                return View(phongKham);
            }
            return View();
        }
        [HttpPost]
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

        [HttpPost]
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
