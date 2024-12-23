using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using PhanMemWebQuanLiBenhVien.DataAccess;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;
using PhanMemWebQuanLiBenhVien.Models.Models;
using static PhanMemWebQuanLiBenhVien.Ultilities.Utilities;

namespace PhanMemWebQuanLiBenhVien.Controllers
{
    [Route("PhongCapCuu")]
    public class PhongCapCuuController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private ApplicationDbContext _db;
        private UserManager<IdentityUser> _userManager;
        public PhongCapCuuController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, ApplicationDbContext db)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _db = db;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            var listPhongCapCuu = _unitOfWork.PhongCapCuuRepository.GetAll();
            ViewBag.Status = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = "Trống",
                        Value = "Free"
                    },
                    new SelectListItem
                    {
                        Text = "Đang trưng dụng",
                        Value = "Used"
                    }
                },
                "Value",
                "Text"
            );
            return View(listPhongCapCuu);
        }
        [HttpPost("Index")]
        public IActionResult Index(string SearchName, string SearchStatus)
        {
            var listPhongCapCuu = _unitOfWork.PhongCapCuuRepository.GetAll();
            if (!string.IsNullOrEmpty(SearchName)) listPhongCapCuu = listPhongCapCuu.Where(u => u.Name.ToLower().Contains(SearchName.ToLower()));
            if (SearchStatus!="NoFilter")
            {
                if (SearchStatus == "Used") listPhongCapCuu = listPhongCapCuu.Where(u => u.isAvailable == false);
                else if (SearchStatus =="Free") listPhongCapCuu=listPhongCapCuu.Where(u=>u.isAvailable == true);
            }
            ViewBag.Status = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = "Trống",
                        Value = "Free"
                    },
                    new SelectListItem
                    {
                        Text = "Đang trưng dụng",
                        Value = "Used"
                    }
                },
                "Value",
                "Text"
            );
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
                TempData["success"] = "Thêm phòng cấp cứu thành công!";
                ActivityTrackingFunction trackingtool = new ActivityTrackingFunction(_db, _unitOfWork);
                var tmpuser = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
                var truetmp_user = (CustomedUser)tmpuser;
                trackingtool.TrackingActivity(truetmp_user.UserId, truetmp_user.UserName, ETypeOfActivity.them, truetmp_user.UserRole, phongCapCuu.RoomId, phongCapCuu, null);
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
                TempData["success"] = "Cập nhật phòng cấp cứu thành công!";
                var details=new List<string>();
                var objFromDb=_unitOfWork.PhongCapCuuRepository.Get(u=>u.RoomId == phongCapCuu.RoomId);
                if (objFromDb.Name != phongCapCuu.Name) details.Add($"Tên: {objFromDb.Name} -> {phongCapCuu.Name}");
                if (objFromDb.isAvailable != phongCapCuu.isAvailable) details.Add($"Đang trưng dụng: {objFromDb.isAvailable} -> {phongCapCuu.isAvailable}");
                ActivityTrackingFunction trackingtool = new ActivityTrackingFunction(_db, _unitOfWork);
                var tmpuser = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
                var truetmp_user = (CustomedUser)tmpuser;
                trackingtool.TrackingActivity(truetmp_user.UserId, truetmp_user.UserName, ETypeOfActivity.sua, truetmp_user.UserRole, phongCapCuu.RoomId, phongCapCuu, details);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost("Delete/{PhongCapCuuId}")]
        public IActionResult Delete(int PhongCapCuuId)
        {
            try
            {
                var phongCapCuu = _unitOfWork.PhongCapCuuRepository.Get(pk => pk.RoomId == PhongCapCuuId);
                if (phongCapCuu != null && phongCapCuu.isAvailable == true)
                {
                    var ws_fk = _unitOfWork.WorkScheduleRepository.GetAll(r => r.PhongCapCuuId == PhongCapCuuId);
                    if(ws_fk != null || ws_fk.Count() > 0)
                    {
                        _unitOfWork.WorkScheduleRepository.RemoveRange(ws_fk); 
                    }
                    _unitOfWork.PhongCapCuuRepository.Remove(phongCapCuu);
                    _unitOfWork.Save();
                    ActivityTrackingFunction trackingtool = new ActivityTrackingFunction(_db, _unitOfWork);
                    var tmpuser = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
                    var truetmp_user = (CustomedUser)tmpuser;
                    trackingtool.TrackingActivity(truetmp_user.UserId, truetmp_user.UserName, ETypeOfActivity.xoa, truetmp_user.UserRole, phongCapCuu.RoomId, phongCapCuu, null);
                    TempData["success"] = "Xóa phòng cấp cứu thành công!";
                }
           
                else
                {
                   TempData["error"] = "Phòng đang sử dụng không thể xoá";
                }
            }
            catch
            {
                TempData["error"] = "Phòng đang sử dụng không thể xoá";
            }
            return RedirectToAction("Index");
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
            var details = new List<string>();
            var objFromDb = _unitOfWork.PhongCapCuuRepository.Get(u => u.RoomId == phongCapCuu.RoomId);
            if (objFromDb.isAvailable != phongCapCuu.isAvailable) details.Add($"Đang trưng dụng: {objFromDb.isAvailable} -> {phongCapCuu.isAvailable}");
            ActivityTrackingFunction trackingtool = new ActivityTrackingFunction(_db, _unitOfWork);
            var tmpuser = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
            var truetmp_user = (CustomedUser)tmpuser;
            trackingtool.TrackingActivity(truetmp_user.UserId, truetmp_user.UserName, ETypeOfActivity.sua, truetmp_user.UserRole, phongCapCuu.RoomId, phongCapCuu, details);
            _unitOfWork.PhongCapCuuRepository.Update(phongCapCuu);
            _unitOfWork.Save();
            return RedirectToAction("Index"); 
        }
    }
}
