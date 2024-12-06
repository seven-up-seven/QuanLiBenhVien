﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;
using PhanMemWebQuanLiBenhVien.Models.Models;

namespace PhanMemWebQuanLiBenhVien.Controllers
{
    [Route("PhongCapCuu")]
    public class PhongCapCuuController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public PhongCapCuuController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            var listPhongCapCuu = _unitOfWork.PhongCapCuuRepository.GetAll();
            return View(listPhongCapCuu);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost("Create")]
        public IActionResult Create(PhongCapCuu phongCapCuu)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.PhongCapCuuRepository.Add(phongCapCuu);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View();
        }

        //Tạm bỏ detail 
        
        //[HttpGet("Detail/{PhongCapCuuId}")]
        //public IActionResult Detail(int PhongCapCuuId)
        //{
        //    var phongCapCuu = _unitOfWork.PhongCapCuuRepository.Get(pk => pk.RoomId == PhongCapCuuId);
            

        //    return View(phongKham);
        //}


        [HttpGet("Update/{PhongCapCuuId}")]
        public IActionResult Update(int PhongCapCuuId)
        {
            var phongCapCuu = _unitOfWork.PhongCapCuuRepository.Get(pk => pk.RoomId == PhongCapCuuId);
            if (phongCapCuu != null)
            {
                ViewBag.Availability = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Trống", Value = "true" },
                    new SelectListItem { Text = "Đang Trưng Dụng", Value = "false" }
                };
                return View(phongCapCuu);
            }
            return View(null);
        }

        [HttpPost("Update/{PhongCapCuuId}")]
        public IActionResult Update(PhongCapCuu phongCapCuu)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.PhongCapCuuRepository.Update(phongCapCuu);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost("Delete/{PhongCapCuuId}")]
        public IActionResult Delete(int PhongCapCuuId)
        {
            var phongCapCuu = _unitOfWork.PhongCapCuuRepository.Get(pk => pk.RoomId == PhongCapCuuId);
            if (phongCapCuu != null)
            {
                _unitOfWork.PhongCapCuuRepository.Remove(phongCapCuu);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult ChangeAvailability(int PhongCapCuuId)
        {
            var phongCapCuu = _unitOfWork.PhongCapCuuRepository.Get(pcc => pcc.RoomId == PhongCapCuuId);
            if(phongCapCuu.isAvailable == false)
            {
                phongCapCuu.isAvailable = true; 
            }
            else
            {
                phongCapCuu.isAvailable = false;
            }
            _unitOfWork.PhongCapCuuRepository.Update(phongCapCuu);
            _unitOfWork.Save();
            return RedirectToAction("Index"); 
        }
    }
}
