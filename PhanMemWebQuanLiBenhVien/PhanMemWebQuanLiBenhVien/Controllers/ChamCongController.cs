using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;
using PhanMemWebQuanLiBenhVien.Models.Models;

namespace PhanMemWebQuanLiBenhVien.Controllers
{
    public class ChamCongController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager; 
        public ChamCongController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager; 
        }
        public IActionResult ChamCongClick()
        {
            if (User.IsInRole("Doctor"))
            {
                var us = (CustomedUser)_userManager.GetUserAsync(User).GetAwaiter().GetResult(); 

                var doctor = _unitOfWork.DoctorRepository.Get(dr => dr.DoctorId == us.UserId);
                var date = DateTime.Now.DayOfWeek.ToString();
                var wsList1 = _unitOfWork.WorkScheduleRepository.Get(u => (u.DoctorId1 == us.UserId) && u.DayOfWeek == date);
                var wsList2 = _unitOfWork.WorkScheduleRepository.Get(u => (u.DoctorId2 == us.UserId) && u.DayOfWeek == date);
                var wsList3 = _unitOfWork.WorkScheduleRepository.Get(u => (u.DoctorId3 == us.UserId) && u.DayOfWeek == date);

                if (DateTime.Now.TimeOfDay >= new TimeSpan(6, 0, 0) && DateTime.Now.TimeOfDay < new TimeSpan(14, 0, 0))
                {
                    if (wsList1 == null) TempData["error"] = "Không có lịch ca 1";
                    else 
                    {
                        var chamcong = _unitOfWork.ChamCongRepository.Get(u => u.DoctorId == us.UserId && u.Time.Date == DateTime.Now.Date && u.Time.TimeOfDay >= new TimeSpan(6, 0, 0) && u.Time.TimeOfDay < new TimeSpan(14, 0, 0));
                        if(chamcong != null)
                        {
                            TempData["error"] = "Đã chấm công ca 1";
                        }
                        else
                        {
                            var chamcongnew = new ChamCong
                            {
                                DoctorId = us.UserId,
                                Time = DateTime.Now
                            };
                            _unitOfWork.ChamCongRepository.Add(chamcongnew);
                            _unitOfWork.Save();
                            TempData["success"] = "Chấm công thành công";
                        }
                    }
                }

                if (DateTime.Now.TimeOfDay >= new TimeSpan(14, 0, 0) && DateTime.Now.TimeOfDay < new TimeSpan(22, 0, 0))
                {
                    if (wsList2 == null) TempData["error"] = "Không có lịch ca 2";
                    else
                    {
                        var chamcong = _unitOfWork.ChamCongRepository.Get(u => u.DoctorId == us.UserId && u.Time.Date == DateTime.Now.Date && u.Time.TimeOfDay >= new TimeSpan(14, 0, 0) && u.Time.TimeOfDay < new TimeSpan(22, 0, 0));
                        if (chamcong != null)
                        {
                            TempData["error"] = "Đã chấm công ca 2";
                        }
                        else
                        {
                            var chamcongnew = new ChamCong
                            {
                                DoctorId = us.UserId,
                                Time = DateTime.Now
                            };
                            _unitOfWork.ChamCongRepository.Add(chamcongnew);
                            _unitOfWork.Save();
                            TempData["success"] = "Chấm công thành công";
                        }
                    }
                }

                if (DateTime.Now.TimeOfDay >= new TimeSpan(22, 0, 0) && DateTime.Now.TimeOfDay < new TimeSpan(6, 0, 0))
                {
                    if (wsList3 == null) TempData["error"] = "Không có lịch ca 3";
                    else
                    {
                        var chamcong = _unitOfWork.ChamCongRepository.Get(u => u.DoctorId == us.UserId && u.Time.Date == DateTime.Now.Date && u.Time.TimeOfDay >= new TimeSpan(22, 0, 0) && u.Time.TimeOfDay < new TimeSpan(6, 0, 0));
                        if (chamcong != null)
                        {
                            TempData["error"] = "Đã chấm công ca 3";
                        }
                        else
                        {
                            var chamcongnew = new ChamCong
                            {
                                DoctorId = us.UserId,
                                Time = DateTime.Now
                            };
                            _unitOfWork.ChamCongRepository.Add(chamcongnew);
                            _unitOfWork.Save();
                            TempData["success"] = "Chấm công thành công";
                        }
                    }
                }

                return RedirectToAction("DashBoard", "Doctor", new {DoctorId = us.UserId});
            }
            if (User.IsInRole("Nurse"))
            {
                var us = (CustomedUser)_userManager.GetUserAsync(User).GetAwaiter().GetResult();
                var chamcong = _unitOfWork.ChamCongRepository.Get(u => u.NurseId == us.UserId && u.Time.Date == DateTime.Now.Date);
                if (chamcong != null) TempData["error"] = "Bạn đã chấm công ngày hôm nay rồi";
                else
                {
                    var chamcongnew = new ChamCong
                    {
                        NurseId = us.UserId,
                        Time = DateTime.Now
                    };
                    _unitOfWork.ChamCongRepository.Add(chamcongnew);
                    _unitOfWork.Save();
                    TempData["success"] = "Chấm công thành công";
                }
                return RedirectToAction("DashBoard", "Nurse", new { NurseId = us.UserId });
            }
            else if (!User.IsInRole("Admin"))
            {
                var us = (CustomedUser)_userManager.GetUserAsync(User).GetAwaiter().GetResult();
                var chamcong = _unitOfWork.ChamCongRepository.Get(u => u.NhanSuId == us.UserId && u.Time.Month == DateTime.Now.Month && u.Time.Year == DateTime.Now.Year);
                if (chamcong != null) TempData["error"] = "Bạn đã chấm công ngày hôm nay rồi";
                else
                {
                    var chamcongnew = new ChamCong
                    {
                        NhanSuId = us.UserId,
                        Time = DateTime.Now
                    };
                    _unitOfWork.ChamCongRepository.Add(chamcongnew);
                    _unitOfWork.Save();
                    TempData["success"] = "Chấm công thành công";
                }
                return RedirectToAction("Home", "NhanSu", new { Id = us.UserId });
            }
            return View(); 
        }
        public IActionResult NgayCongDuTinh()
        {
            return View(); 
        }
    }
}
