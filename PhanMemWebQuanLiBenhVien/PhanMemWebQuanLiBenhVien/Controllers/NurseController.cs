﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhanMemWebQuanLiBenhVien.DataAccess;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;
using System.Numerics;
using static PhanMemWebQuanLiBenhVien.Ultilities.Utilities;

namespace PhanMemWebQuanLiBenhVien.Controllers
{
    public class NurseController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private string wwwroot;
        private IWebHostEnvironment _webHostEnvironment;
        private ApplicationDbContext _db;
        private UserManager<IdentityUser> _userManager;
        public NurseController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _db = db;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var NurseList = _unitOfWork.NurseRepository.GetAll();
            return View(NurseList);
        }

        public IActionResult Create()
        {
            var genderList = Enum.GetValues(typeof(EGender))
            .Cast<EGender>()
            .Select(gender => new SelectListItem
            {
                Value = gender.ToString(),
                Text = gender.ToString()
            }).ToList();
            ViewBag.Genders = genderList;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Nurse nurse, IFormFile? NurseImg)
        {
            wwwroot = _webHostEnvironment.WebRootPath;
            if (NurseImg != null)
            {
                string filename = Path.GetFileNameWithoutExtension(NurseImg.FileName) + Path.GetExtension(NurseImg.FileName);
                string filepath = Path.Combine(wwwroot, @"images\");
                using (var filestream = new FileStream(Path.Combine(filepath, filename), FileMode.Create))
                {
                    NurseImg.CopyTo(filestream);
                }
                nurse.NurseImgURL = @"\images\" + filename;
            }
            else nurse.NurseImgURL = "";
            _unitOfWork.NurseRepository.Add(nurse);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int NurseId)
        {
            var genderList = Enum.GetValues(typeof(EGender))
            .Cast<EGender>()
            .Select(gender => new SelectListItem
            {
                Value = gender.ToString(),
                Text = gender.ToString()
            }).ToList();
            ViewBag.Genders = genderList;
            var nurse=_unitOfWork.NurseRepository.Get(u=>u.NurseId == NurseId);
            return View(nurse);
        }
        [HttpPost]
        public IActionResult Update(Nurse nurse, IFormFile? NurseImg)
        {
            wwwroot = _webHostEnvironment.WebRootPath;
            if (NurseImg != null)
            {
                string filename = Path.GetFileNameWithoutExtension(NurseImg.FileName) + Path.GetExtension(NurseImg.FileName);
                string filepath = Path.Combine(wwwroot, @"images\");
                using (var filestream = new FileStream(Path.Combine(filepath, filename), FileMode.Create))
                {
                    NurseImg.CopyTo(filestream);
                }
                nurse.NurseImgURL = @"\images\" + filename;
            }
            _unitOfWork.NurseRepository.Update(nurse);
            _unitOfWork.Save();
            if (User.IsInRole("Nurse")) return RedirectToAction("NurseHomePage", new { NurseId = nurse.NurseId });
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int NurseId)
        {
            var nurse = _unitOfWork.NurseRepository.Get(u => u.NurseId == NurseId);
            wwwroot = _webHostEnvironment.WebRootPath;
            if (!string.IsNullOrEmpty(nurse.NurseImgURL))
            {
                var oldpath = Path.Combine(wwwroot, nurse.NurseImgURL.TrimStart('\\'));
                if (System.IO.File.Exists(oldpath)) System.IO.File.Delete(oldpath);
            }
            var user = _db.customedUsers.FirstOrDefault(u => (u.UserId == NurseId && u.UserRole == ERole.nurse));
            if (user!=null) _userManager.DeleteAsync(user).GetAwaiter().GetResult();
            _unitOfWork.NurseRepository.Remove(nurse);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        public IActionResult Detail(int NurseId)
        {
            var nurse = _unitOfWork.NurseRepository.Get(u => u.NurseId == NurseId);
            nurse.PatientList = _unitOfWork.PatientRepository.GetAll(u => u.NurseId == NurseId).ToList();
            return View(nurse);
        }
        public IActionResult NurseHomePage(int NurseId)
        {
            var nurse= _unitOfWork.NurseRepository.Get(u=>u.NurseId == NurseId);
            return View(nurse);
        }
        public IActionResult NursePatients(int NurseId)
        {
            var nurse = _unitOfWork.NurseRepository.Get(u => u.NurseId == NurseId);
            var patients = _unitOfWork.PatientRepository.GetAll(pt => (pt.TrangThaiDieuTri == ETrangThaiDieuTri.nhapvien && pt.NurseId == NurseId));
            ViewBag.Patients = patients;
            return View(nurse);
        }
    }
}
