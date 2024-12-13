using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;
using System.Numerics;
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
                Text = phong switch
                {
                    EPhong.phongkham => "Phòng khám",
                    EPhong.phongbenh => "Phòng bệnh",
                    _ => phong.ToString() // Trường hợp dự phòng
                }
            }).ToList();
            var LeverList = Enum.GetValues(typeof(Elever))
            .Cast<Elever>()
            .Select(lever => new SelectListItem
            {
                Value = lever.ToString(),
                Text = lever switch
                {
                    Elever.binhthuong => "Bình Thường",
                    Elever.uutien => "Ưu Tiên",
                    Elever.nguycap => "Nguy Cấp",
                    _ => lever.ToString() // Trường hợp dự phòng
                }
            }).ToList();

            ViewBag.Levers = LeverList;

            ViewBag.Phongs = PhongList;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Mission mission)
        {
            void PopulateDropdowns()
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
                        Text = phong switch
                        {
                            EPhong.phongkham => "Phòng khám",
                            EPhong.phongbenh => "Phòng bệnh",
                            _ => phong.ToString()
                        }
                    }).ToList();

                var LeverList = Enum.GetValues(typeof(Elever))
                    .Cast<Elever>()
                    .Select(lever => new SelectListItem
                    {
                        Value = lever.ToString(),
                        Text = lever switch
                        {
                            Elever.binhthuong => "Bình Thường",
                            Elever.uutien => "Ưu Tiên",
                            Elever.nguycap => "Nguy Cấp",
                            _ => lever.ToString()
                        }
                    }).ToList();

                ViewBag.Levers = LeverList;
                ViewBag.Phongs = PhongList;
            }


            if (!ModelState.IsValid)
            {
                PopulateDropdowns();
                return View(mission);
            }
            _unitOfWork.MissionRepository.Add(mission);
            _unitOfWork.Save();
            TempData["success"] = "Thêm nhiệm vụ thành công!";
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
               Text = phong switch
               {
                   EPhong.phongkham => "Phòng khám",
                   EPhong.phongbenh => "Phòng bệnh",
                   _ => phong.ToString() // Trường hợp dự phòng
               }
           }).ToList();
            var LeverList = Enum.GetValues(typeof(Elever))
            .Cast<Elever>()
            .Select(lever => new SelectListItem
            {
                Value = lever.ToString(),
                Text = lever switch
                {
                    Elever.binhthuong => "Bình Thường",
                    Elever.uutien => "Ưu Tiên",
                    Elever.nguycap => "Nguy Cấp",
                    _ => lever.ToString() // Trường hợp dự phòng
                }
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

                var PhongList = Enum.GetValues(typeof(EPhong))
               .Cast<EPhong>()
               .Select(phong => new SelectListItem
               {
                   Value = phong.ToString(),
                   Text = phong switch
                   {
                       EPhong.phongkham => "Phòng khám",
                       EPhong.phongbenh => "Phòng bệnh",
                       _ => phong.ToString() // Trường hợp dự phòng
                   }
               }).ToList();
                var LeverList = Enum.GetValues(typeof(Elever))
                .Cast<Elever>()
                .Select(lever => new SelectListItem
                {
                    Value = lever.ToString(),
                    Text = lever switch
                    {
                        Elever.binhthuong => "Bình Thường",
                        Elever.uutien => "Ưu Tiên",
                        Elever.nguycap => "Nguy Cấp",
                        _ => lever.ToString() // Trường hợp dự phòng
                    }
                }).ToList();
                ViewBag.Levers = LeverList;
                ViewBag.Phongs = PhongList;
            }
            if (!ModelState.IsValid)
            {
                PopulateDropdowns();
                return View(mission);
            }
            _unitOfWork.MissionRepository.Update(mission);
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
            TempData["success"] = "Xóa nhiệm vụ thành công!";
            return RedirectToAction("Index");
        }

        [HttpGet]
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



        public IActionResult Tick(int id)
        {
            var mission = _unitOfWork.MissionRepository.Get(u => u.MissionId == id);
            if (mission == null)
            {
                return NotFound(); // Nếu không tìm thấy nhiệm vụ
            }

            // Đánh dấu nhiệm vụ là hoàn thành
            mission.IsCompleted = !mission.IsCompleted;
            _unitOfWork.MissionRepository.Update(mission);
            _unitOfWork.Save();

            // Chuyển hướng về trang chi tiết
            return RedirectToAction("DetailMission", new { missionId = id });
        }


    }
}
