using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;

namespace PhanMemWebQuanLiBenhVien.Controllers
{
    [Route("WorkSchedule")]
    public class WorkScheduleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public WorkScheduleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
			var listWorkChedule = _unitOfWork.WorkScheduleRepository.GetAll();
			return View(listWorkChedule);
        }


        [HttpGet("Create")]
        public IActionResult Create()
        {
			ViewBag.listDoctor = _unitOfWork.DoctorRepository.GetAll().Select(u => new SelectListItem
			{
				Text = u.DoctorName,
				Value = u.DoctorId.ToString()
			});
			return View();
        }
        [HttpPost("Create")]
        public IActionResult Create(WorkSchedule workSchedule)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.WorkScheduleRepository.Add(workSchedule);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View();
        }


        [HttpGet("Detail/{WorkScheduleId}")]
        public IActionResult Detail(int WorkScheduleId)
        {
            var workSchedule = _unitOfWork.WorkScheduleRepository.Get(ws => ws.WorkScheduleId == WorkScheduleId);
            return View(workSchedule);
        }


        [HttpGet("Update/{WorkScheduleId}")]
        public IActionResult Update(int WorkScheduleId)
        {
            var workSchedule = _unitOfWork.WorkScheduleRepository.Get(ws => ws.WorkScheduleId == WorkScheduleId);
            if (workSchedule != null)
            {
                return View(workSchedule);
            }
            return View();
        }
        [HttpPost("Update/{WorkScheduleId}")]
        public IActionResult Update(WorkSchedule workSchedule)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.WorkScheduleRepository.Update(workSchedule);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost("Delete/{WorkScheduleId}")]
        public IActionResult Delete(int WorkScheduleId)
        {
            var workSchedule = _unitOfWork.WorkScheduleRepository.Get(ws => ws.WorkScheduleId == WorkScheduleId);
            if (workSchedule != null)
            {
                _unitOfWork.WorkScheduleRepository.Remove(workSchedule);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View();
        }

        public static (DateTime StartOfWeek, DateTime EndOfWeek) GetWeekRange(DateTime currentDate)
        {
            var startOfWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek + 1); // Thứ Hai
            var endOfWeek = startOfWeek.AddDays(6); // Chủ Nhật
            return (startOfWeek, endOfWeek);
        }

    }
}
