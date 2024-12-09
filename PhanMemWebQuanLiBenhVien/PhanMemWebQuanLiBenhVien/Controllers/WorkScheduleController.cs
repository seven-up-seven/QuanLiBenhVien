using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public IActionResult Index(string date)
        {
            var listphongkham = _unitOfWork.PhongKhamRepository.GetAll();

            var listphongcapcuu = _unitOfWork.PhongCapCuuRepository.GetAll();
            ViewBag.listphongcapcuu = listphongcapcuu;
            
            foreach(var phongkham in listphongkham)
            {
                var workschedule = _unitOfWork.WorkScheduleRepository.Get(ws => ws.PhongKhamId == phongkham.RoomId);
                string Thu = workschedule.DayOfWeek; 
                switch (Thu)
                {
                    case "Monday":
                        phongkham.WorkSchedules[0] = workschedule;
                        break;
                    case "Tuesday":
                        phongkham.WorkSchedules[1] = workschedule;
                        break;
                    case "Wednesday":
                        phongkham.WorkSchedules[2] = workschedule;
                        break;
                    case "Thursday":
                        phongkham.WorkSchedules[3] = workschedule;
                        break;
                    case "Friday":
                        phongkham.WorkSchedules[4] = workschedule;
                        break;
                    case "Saturday":
                        phongkham.WorkSchedules[5] = workschedule;
                        break;
                    case "Sunday":
                        phongkham.WorkSchedules[6] = workschedule;
                        break;
                    default:
                        break;
                }
            }
            return View(listphongkham);
        }


        [HttpGet("Create")]
        public IActionResult Create(int PhongKhamId, string Thu)
        {
            var phongkham=_unitOfWork.PhongKhamRepository.Get(u=>u.RoomId==PhongKhamId);
            ViewBag.Thu=Thu;
            ViewBag.phongkham= phongkham;
            ViewBag.doctorlist = _unitOfWork.DoctorRepository.GetAll(u => u.ProfessionId == phongkham.ProfessionId).Select(u => new SelectListItem { 
                Text=u.DoctorName+" "+u.DoctorId,
                Value=u.DoctorId.ToString()
            });
            return View();
        } 
        [HttpPost("Create")]
        public IActionResult Create(WorkSchedule workschedule)
        {
            var phongkham = _unitOfWork.PhongKhamRepository.Get(u => u.RoomId == workschedule.PhongKhamId);
            if (ModelState.IsValid)
            {
                _unitOfWork.WorkScheduleRepository.Add(workschedule);
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


        [HttpGet]
        public IActionResult Update(int PhongKhamId, string Thu)
        {
            var phongkham = _unitOfWork.PhongKhamRepository.Get(u => u.RoomId == PhongKhamId);
            ViewBag.Thu = Thu;
            ViewBag.phongkham = phongkham;
            ViewBag.doctorlist = _unitOfWork.DoctorRepository.GetAll(u => u.ProfessionId == phongkham.ProfessionId).Select(u => new SelectListItem
            {
                Text = u.DoctorName + " " + u.DoctorId,
                Value = u.DoctorId.ToString()
            });
            var workschedule = _unitOfWork.WorkScheduleRepository.Get(ws => ws.PhongKhamId == PhongKhamId);
            
            return View(workschedule);
        }
        [HttpPost]
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
