using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;
using static PhanMemWebQuanLiBenhVien.Ultilities.Utilities;

namespace PhanMemWebQuanLiBenhVien.Controllers
{

    public class MissionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MissionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var missionList = _unitOfWork.MissionRepository.GetAll();
            foreach (var mission in missionList)
            {
                var doctor = _unitOfWork.DoctorRepository.Get(u => u.DoctorId == mission.DoctorId);
                var phongkham = _unitOfWork.PhongKhamRepository.Get(u => u.RoomId == mission.PhongKhamId);
                var phongbenh = _unitOfWork.PhongBenhRepository.Get(u => u.RoomId == mission.PhongBenhId);
                mission.Doctor = doctor;
                mission.PhongKham = phongkham;
                mission.PhongBenh = phongbenh;
            }
            return View(missionList);
        }

        [HttpGet]
        public IActionResult Create()
        {

            ViewBag.Doctors = _unitOfWork.DoctorRepository.GetAll().Select(u => new SelectListItem
            {
                Text = "Tên: " + u.DoctorName + " ID: " + u.DoctorId,
                Value = u.DoctorId.ToString()
            });
            ViewBag.PhongKhams = _unitOfWork.PhongKhamRepository.GetAll().Select(u => new SelectListItem
            {
                Text = "Tên: " + u.Name + " ID: " + u.RoomId,
                Value = u.RoomId.ToString()
            });
            ViewBag.PhongBenhs = _unitOfWork.PhongBenhRepository.GetAll().Select(u => new SelectListItem
            {
                Text = "Tên: " + u.Name + " ID: " + u.RoomId,
                Value = u.RoomId.ToString()
            });
            var PhongList = Enum.GetValues(typeof(EPhong))
            .Cast<EPhong>()
            .Select(phong => new SelectListItem
            {
                Value = phong.ToString(),
                Text = phong.ToString()
            }).ToList();
            var LeverList = Enum.GetValues(typeof(ELever))
           .Cast<ELever>()
           .Select(lever => new SelectListItem
           {
               Value = lever.ToString(),
               Text = lever.ToString()
           }).ToList();
            ViewBag.Levers = LeverList;
            ViewBag.Phongs = PhongList;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Mission mission)
        {
            // Hàm hỗ trợ: Lấy lại dữ liệu cho dropdowns trong trường hợp có lỗi
            void PopulateDropdowns()
            {
                ViewBag.Doctors = _unitOfWork.DoctorRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.DoctorName,
                    Value = u.DoctorId.ToString()
                });

                ViewBag.PhongKhams = _unitOfWork.PhongKhamRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.RoomId.ToString()
                });

                ViewBag.PhongBenhs = _unitOfWork.PhongBenhRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.RoomId.ToString()
                });

                ViewBag.Phongs = Enum.GetValues(typeof(EPhong))
                    .Cast<EPhong>()
                    .Select(phong => new SelectListItem
                    {
                        Value = phong.ToString(),
                        Text = phong.ToString()
                    }).ToList();
                ViewBag.Levers = Enum.GetValues(typeof(ELever))
                   .Cast<ELever>()
                   .Select(lever => new SelectListItem
                   {
                       Value = lever.ToString(),
                       Text = lever.ToString()
                   }).ToList();


            }

            int count = 0;

            // Kiểm tra thời gian bắt đầu
            if (mission.Time == default(DateTime))
            {
                ModelState.AddModelError("Time", "Vui lòng chọn ngày bắt đầu.");
                count++;
            }

            // Kiểm tra thời gian kết thúc
            if (mission.EndTime == default(DateTime))
            {
                ModelState.AddModelError("EndTime", "Vui lòng chọn ngày kết thúc.");
                count++;
            }

            // Kiểm tra thời gian kết thúc phải lớn hơn thời gian bắt đầu
            if (mission.EndTime <= mission.Time)
            {
                ModelState.AddModelError("EndTime", "Thời gian kết thúc phải lớn hơn thời gian bắt đầu.");
                count++;
            }

            // Kiểm tra bác sĩ
            if (mission.DoctorId == 0)
            {
                ModelState.AddModelError("DoctorId", "Vui lòng chọn bác sĩ.");
                count++;
            }

            // Kiểm tra loại phòng
            if (!Enum.IsDefined(typeof(EPhong), mission.RoomType))
            {
                ModelState.AddModelError("RoomType", "Vui lòng chọn loại phòng.");
                count++;
            }


            if (!Enum.IsDefined(typeof(ELever), mission.Lever))
            {
                ModelState.AddModelError("Lever", "Vui lòng chọn mức độ.");
                count++;
            }

            // Kiểm tra phòng dựa trên loại phòng
            if (mission.RoomType == EPhong.phongkham && mission.PhongKhamId == null)
            {
                ModelState.AddModelError("PhongKhamId", "Vui lòng chọn phòng khám.");
                count++;
            }

            if (mission.RoomType == EPhong.phongbenh && mission.PhongBenhId == null)
            {
                ModelState.AddModelError("PhongBenhId", "Vui lòng chọn phòng bệnh.");
                count++;
            }

            if (mission.Content == "")
            {
                ModelState.AddModelError("Content", "Vui lòng nhập nội dung.");
                count++;
            }
            if (count != 0)
            {
                PopulateDropdowns();
                return View(mission);
            }
            // Thêm nhiệm vụ và lưu
            _unitOfWork.MissionRepository.Add(mission);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }



        [HttpGet]
        public IActionResult Update(int missionId)
        {
            var mission = _unitOfWork.MissionRepository.Get(u => u.MissionId == missionId);
            if (mission == null)
            {
                return NotFound(); // Nếu không tìm thấy nhiệm vụ
            }

            ViewBag.Doctors = _unitOfWork.DoctorRepository.GetAll().Select(u => new SelectListItem
            {
                Text = "Tên: " + u.DoctorName + " ID: " + u.DoctorId,
                Value = u.DoctorId.ToString()
            });
            ViewBag.PhongKhams = _unitOfWork.PhongKhamRepository.GetAll().Select(u => new SelectListItem
            {
                Text = "Tên: " + u.Name + " ID: " + u.RoomId,
                Value = u.RoomId.ToString()
            });
            ViewBag.PhongBenhs = _unitOfWork.PhongBenhRepository.GetAll().Select(u => new SelectListItem
            {
                Text = "Tên: " + u.Name + " ID: " + u.RoomId,
                Value = u.RoomId.ToString()
            });
            var PhongList = Enum.GetValues(typeof(EPhong))
            .Cast<EPhong>()
            .Select(phong => new SelectListItem
            {
                Value = phong.ToString(),
                Text = phong.ToString()
            }).ToList();

            var LeverList = Enum.GetValues(typeof(ELever))
            .Cast<ELever>()
            .Select(lever => new SelectListItem
            {
                Value = lever.ToString(),
                Text = lever.ToString()
            }).ToList();

            ViewBag.Levers = LeverList;
            ViewBag.Phongs = PhongList;

            return View(mission); // Trả nhiệm vụ hiện tại cho View
        }

        [HttpPost]
        public IActionResult Update(Mission mission)
        {

            // Hàm hỗ trợ: Lấy lại dữ liệu cho dropdowns trong trường hợp có lỗi
            void PopulateDropdowns()
            {
                ViewBag.Doctors = _unitOfWork.DoctorRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.DoctorName,
                    Value = u.DoctorId.ToString()
                });

                ViewBag.PhongKhams = _unitOfWork.PhongKhamRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.RoomId.ToString()
                });

                ViewBag.PhongBenhs = _unitOfWork.PhongBenhRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.RoomId.ToString()
                });

                ViewBag.Phongs = Enum.GetValues(typeof(EPhong))
                    .Cast<EPhong>()
                    .Select(phong => new SelectListItem
                    {
                        Value = phong.ToString(),
                        Text = phong.ToString()
                    }).ToList();
                ViewBag.Levers = Enum.GetValues(typeof(ELever))
                  .Cast<ELever>()
                  .Select(lever => new SelectListItem
                  {
                      Value = lever.ToString(),
                      Text = lever.ToString()
                  }).ToList();
            }

            int count = 0;

            // Kiểm tra thời gian bắt đầu
            if (mission.Time == default(DateTime))
            {
                ModelState.AddModelError("Time", "Vui lòng chọn ngày bắt đầu.");
                count++;
            }

            // Kiểm tra thời gian kết thúc
            if (mission.EndTime == default(DateTime))
            {
                ModelState.AddModelError("EndTime", "Vui lòng chọn ngày kết thúc.");
                count++;
            }

            // Kiểm tra thời gian kết thúc phải lớn hơn thời gian bắt đầu
            if (mission.EndTime <= mission.Time)
            {
                ModelState.AddModelError("EndTime", "Thời gian kết thúc phải lớn hơn thời gian bắt đầu.");
                count++;
            }

            // Kiểm tra bác sĩ
            if (mission.DoctorId == 0)
            {
                ModelState.AddModelError("DoctorId", "Vui lòng chọn bác sĩ.");
                count++;
            }

            // Kiểm tra loại phòng
            if (!Enum.IsDefined(typeof(EPhong), mission.RoomType))
            {
                ModelState.AddModelError("RoomType", "Vui lòng chọn loại phòng.");
                count++;
            }

            if (!Enum.IsDefined(typeof(ELever), mission.Lever))
            {
                ModelState.AddModelError("Lever", "Vui lòng chọn mức độ.");
                count++;
            }

            // Kiểm tra phòng dựa trên loại phòng
            if (mission.RoomType == EPhong.phongkham && mission.PhongKhamId == null)
            {
                ModelState.AddModelError("PhongKhamId", "Vui lòng chọn phòng khám.");
                count++;
            }

            if (mission.RoomType == EPhong.phongbenh && mission.PhongBenhId == null)
            {
                ModelState.AddModelError("PhongBenhId", "Vui lòng chọn phòng bệnh.");
                count++;
            }

            if (mission.Content == "")
            {
                ModelState.AddModelError("Content", "Vui lòng nhập nội dung.");
                count++;
            }
            if (count != 0)
            {
                PopulateDropdowns();
                return View(mission);
            }
            // Thêm nhiệm vụ và lưu
            _unitOfWork.MissionRepository.Add(mission);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Delete(int missionId)
        {
            var mission = _unitOfWork.MissionRepository.Get(u => u.MissionId == missionId);
            if (mission == null)
            {
                return NotFound(); // Nếu không tìm thấy nhiệm vụ
            }

            // Xóa nhiệm vụ
            _unitOfWork.MissionRepository.Remove(mission);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }

        public IActionResult DetailMission(int missionId)
        {
            var mission = _unitOfWork.MissionRepository.Get(m => m.MissionId == missionId);
            if (mission == null)
            {
                return NotFound(); // Nếu không tìm thấy nhiệm vụ
            }
            if (mission.PhongKhamId != null)
                mission.PhongKham = _unitOfWork.PhongKhamRepository.Get(m => m.RoomId == mission.PhongKhamId);
            else
                mission.PhongBenh = _unitOfWork.PhongBenhRepository.Get(m => m.RoomId == mission.PhongBenhId);
            mission.Doctor= _unitOfWork.DoctorRepository.Get(d=>d.DoctorId == mission.DoctorId);

            return View(mission);
        }


        [HttpPost]
        public IActionResult Tick(int missionId,int day,int month,int year)
        {
            var mission = _unitOfWork.MissionRepository.Get(u => u.MissionId == missionId);
            if (mission == null)
            {
                return NotFound(); // Nếu không tìm thấy nhiệm vụ
            }

            // Đánh dấu nhiệm vụ là hoàn thành
            mission.IsCompleted = !mission.IsCompleted;
            _unitOfWork.MissionRepository.Update(mission);
            _unitOfWork.Save();

            return RedirectToAction("DoctorMission", "Doctor", new {day, month, year});
        }

    }
}
