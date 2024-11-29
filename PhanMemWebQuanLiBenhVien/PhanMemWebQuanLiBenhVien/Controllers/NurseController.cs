using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;
using static PhanMemWebQuanLiBenhVien.Ultilities.Utilities;

namespace PhanMemWebQuanLiBenhVien.Controllers
{
    public class NurseController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private string wwwroot;
        private IWebHostEnvironment _webHostEnvironment;
        public NurseController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
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
        public IActionResult Update()
        {
            return View();
        }
        public IActionResult Delete(int NurseId)
        {
            var nurse = _unitOfWork.NurseRepository.Get(u => u.NurseId == NurseId);
            wwwroot = _webHostEnvironment.WebRootPath;
            if (!string.IsNullOrEmpty(nurse.NurseImgURL))
            {
                var oldpath = Path.Combine(wwwroot, nurse.NurseImgURL.TrimStart('\\'));
                if (System.IO.File.Exists(oldpath)) System.IO.File.Delete(oldpath);
            }
            _unitOfWork.NurseRepository.Remove(nurse);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        public IActionResult Detail(int NurseId)
        {
            var nurse = _unitOfWork.NurseRepository.Get(u => u.NurseId == NurseId);
            return View(nurse);
        }
    }
}
