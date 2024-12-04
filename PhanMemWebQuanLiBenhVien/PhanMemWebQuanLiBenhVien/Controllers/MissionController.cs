using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;

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
            foreach (var mision in missionList)
            {
                var doctor = _unitOfWork.DoctorRepository.Get(u => u.DoctorId == mision.DoctorId);
                mision.Doctor=doctor;
            }
            return View(missionList);
        }

        [HttpGet]
        public IActionResult Create()
        {
      
   	        ViewBag.Doctors = _unitOfWork.DoctorRepository.GetAll().Select(u => new SelectListItem
			{
				Text = "Tên: " + u.DoctorName + " ID: "+u.DoctorId,
				Value = u.DoctorId.ToString()
			});
            return View();
        }

        [HttpPost]
        public IActionResult Create(Mission mission)
        {
            // Kiểm tra nếu nội dung không được nhập
            if (string.IsNullOrEmpty(mission.Content))
            {
                // Gán lại danh sách bác sĩ nếu không có nội dung
                ViewBag.Doctors = _unitOfWork.DoctorRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.DoctorName,
                    Value = u.DoctorId.ToString()
                });
                ModelState.AddModelError("Content", "Nội dung không được để trống !");
                return View(mission); // Trả về view để người dùng sửa lại
            }

            // Kiểm tra nếu thời gian không được nhập
            if (mission.Time == default(DateTime))
            {
                // Gán lại danh sách bác sĩ nếu thời gian không hợp lệ
                ViewBag.Doctors = _unitOfWork.DoctorRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.DoctorName,
                    Value = u.DoctorId.ToString()
                });
                ModelState.AddModelError("Time", "Thời gian không được để trống !"); // Thêm lỗi cho trường Time
                return View(mission); // Trả về view để người dùng sửa lại
            }

            // Nếu tất cả các trường hợp trên đều hợp lệ
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

            // Gán lại danh sách bác sĩ
            ViewBag.Doctors = _unitOfWork.DoctorRepository.GetAll().Select(u => new SelectListItem
            {
                Text = u.DoctorName,
                Value = u.DoctorId.ToString()
            });

            return View(mission); // Trả nhiệm vụ hiện tại cho View
        }

        [HttpPost]
        public IActionResult Update(Mission mission)
        {
            // Kiểm tra nếu nội dung không được nhập
            if (string.IsNullOrEmpty(mission.Content))
            {
                // Gán lại danh sách bác sĩ nếu không có nội dung
                ViewBag.Doctors = _unitOfWork.DoctorRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.DoctorName,
                    Value = u.DoctorId.ToString()
                });
                ModelState.AddModelError("Content", "Nội dung không được để trống !");
                return View(mission); // Trả về view để người dùng sửa lại
            }

            // Kiểm tra nếu thời gian không được nhập
            if (mission.Time == default(DateTime))
            {
                // Gán lại danh sách bác sĩ nếu thời gian không hợp lệ
                ViewBag.Doctors = _unitOfWork.DoctorRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.DoctorName,
                    Value = u.DoctorId.ToString()
                });
                ModelState.AddModelError("Time", "Thời gian không được để trống !"); // Thêm lỗi cho trường Time
                return View(mission); // Trả về view để người dùng sửa lại
            }

            // Cập nhật dữ liệu nhiệm vụ
            _unitOfWork.MissionRepository.Update(mission);  // Sử dụng phương thức Update thay vì Add
            _unitOfWork.Save();

            return RedirectToAction("Index"); // Quay lại danh sách nhiệm vụ
        }


        [HttpPost("Delete")]
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




    }
}
