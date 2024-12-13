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
            if (listphongkham.Count()>0)
            {
                foreach (var phongkham in listphongkham)
                {
                    var workschedulelist = _unitOfWork.WorkScheduleRepository.GetAll(ws => ws.PhongKhamId == phongkham.RoomId);
                    foreach (var workschedule in workschedulelist)
                    {
                        if (workschedule != null)
                        {
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
                    }
                }
            }
            if (listphongcapcuu.Count()>0)
            {
                foreach (var phongcapcuu in listphongcapcuu)
                {
                    var workschedulelist = _unitOfWork.WorkScheduleRepository.GetAll(ws => ws.PhongCapCuuId == phongcapcuu.RoomId);
                    foreach (var workschedule in workschedulelist)
                    {
                        if (workschedule != null)
                        {
                            string Thu = workschedule.DayOfWeek;
                            switch (Thu)
                            {
                                case "Monday":
                                    phongcapcuu.WorkSchedules[0] = workschedule;
                                    break;
                                case "Tuesday":
                                    phongcapcuu.WorkSchedules[1] = workschedule;
                                    break;
                                case "Wednesday":
                                    phongcapcuu.WorkSchedules[2] = workschedule;
                                    break;
                                case "Thursday":
                                    phongcapcuu.WorkSchedules[3] = workschedule;
                                    break;
                                case "Friday":
                                    phongcapcuu.WorkSchedules[4] = workschedule;
                                    break;
                                case "Saturday":
                                    phongcapcuu.WorkSchedules[5] = workschedule;
                                    break;
                                case "Sunday":
                                    phongcapcuu.WorkSchedules[6] = workschedule;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            ViewBag.listphongcapcuu = listphongcapcuu;
            return View(listphongkham);
        }


        [HttpGet("Create")]
        public IActionResult Create(int PhongKhamId, string Thu)
        {
            var phongkham = _unitOfWork.PhongKhamRepository.Get(u => u.RoomId == PhongKhamId);
            ViewBag.Thu = Thu;
            ViewBag.phongkham = phongkham;
            var workschedulelist = _unitOfWork.WorkScheduleRepository.GetAll(u => u.DayOfWeek == Thu);
            var ca1doctorlist = new List<Doctor>();
            var ca2doctorlist = new List<Doctor>();
            var ca3doctorlist = new List<Doctor>();
            var doctorlist = _unitOfWork.DoctorRepository.GetAll(u=>u.ProfessionId==phongkham.ProfessionId);
            foreach (var doctor in doctorlist)
            {
                if (workschedulelist.Count()!=0)
                {
                    bool flag1 = false;
                    bool flag2 = false;
                    bool flag3 = false;
                    foreach (var workschedule in workschedulelist)
                    {
						if (workschedule != null)
						{
                            if (doctor.DoctorId == workschedule.DoctorId1) flag1 = true;
                            if (doctor.DoctorId == workschedule.DoctorId2) flag2 = true;
                            if (doctor.DoctorId == workschedule.DoctorId3) flag3 = true;
						}
					}
                    if (flag1 == false) ca1doctorlist.Add(doctor);
                    if (flag2 == false) ca2doctorlist.Add(doctor);
                    if (flag3 == false) ca3doctorlist.Add(doctor);
                }
                else
                {
                    ca1doctorlist.Add(doctor);
                    ca2doctorlist.Add(doctor);
                    ca3doctorlist.Add(doctor);
                }
            }
            ViewBag.doctorlistca1 = ca1doctorlist.Select(u => new SelectListItem
            {
                Text = u.DoctorName + " " + u.DoctorId,
                Value = u.DoctorId.ToString()
            });
            ViewBag.doctorlistca2 = ca2doctorlist.Select(u => new SelectListItem
            {
                Text = u.DoctorName + " " + u.DoctorId,
                Value = u.DoctorId.ToString()
            });
            ViewBag.doctorlistca3 = ca3doctorlist.Select(u => new SelectListItem
            {
                Text = u.DoctorName + " " + u.DoctorId,
                Value = u.DoctorId.ToString()
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

        [HttpGet("CreateWorkScheduleForPhongCapCuu")]
        public IActionResult CreateWorkScheduleForPhongCapCuu(int PhongCapCuuId, string Thu)
        {
            var phongcapcuu = _unitOfWork.PhongCapCuuRepository.Get(u => u.RoomId == PhongCapCuuId);
            ViewBag.Thu = Thu;
            ViewBag.phongcapcuu = phongcapcuu;
			var workschedulelist = _unitOfWork.WorkScheduleRepository.GetAll(u => u.DayOfWeek == Thu);
			var ca1doctorlist = new List<Doctor>();
			var ca2doctorlist = new List<Doctor>();
			var ca3doctorlist = new List<Doctor>();
			var doctorlist = _unitOfWork.DoctorRepository.GetAll();
			foreach (var doctor in doctorlist)
			{
				if (workschedulelist.Count() != 0)
				{
					bool flag1 = false;
					bool flag2 = false;
					bool flag3 = false;
					foreach (var workschedule in workschedulelist)
					{
						if (workschedule != null)
						{
							if (doctor.DoctorId == workschedule.DoctorId1) flag1 = true;
							if (doctor.DoctorId == workschedule.DoctorId2) flag2 = true;
							if (doctor.DoctorId == workschedule.DoctorId3) flag3 = true;
						}
					}
					if (flag1 == false) ca1doctorlist.Add(doctor);
					if (flag2 == false) ca2doctorlist.Add(doctor);
					if (flag3 == false) ca3doctorlist.Add(doctor);
				}
				else
				{
					ca1doctorlist.Add(doctor);
					ca2doctorlist.Add(doctor);
					ca3doctorlist.Add(doctor);
				}
			}
			ViewBag.doctorlistca1 = ca1doctorlist.Select(u => new SelectListItem
            {
                Text = u.DoctorName + " " + u.DoctorId,
                Value = u.DoctorId.ToString()
            });
            ViewBag.doctorlistca2 = ca2doctorlist.Select(u => new SelectListItem
            {
                Text = u.DoctorName + " " + u.DoctorId,
                Value = u.DoctorId.ToString()
            });
            ViewBag.doctorlistca3 = ca3doctorlist.Select(u => new SelectListItem
            {
                Text = u.DoctorName + " " + u.DoctorId,
                Value = u.DoctorId.ToString()
            });
            return View();
        }
        [HttpPost("CreateWorkScheduleForPhongCapCuu")]
        public IActionResult CreateWorkScheduleForPhongCapCuu(WorkSchedule workschedule)
        {
            var phongcapcuu = _unitOfWork.PhongCapCuuRepository.Get(u => u.RoomId == workschedule.PhongCapCuuId);
            if (ModelState.IsValid)
            {
                _unitOfWork.WorkScheduleRepository.Add(workschedule);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet("UpdateWorkScheduleForPhongCapCuu")]
        public IActionResult UpdateWorkScheduleForPhongCapCuu(int PhongCapCuuId, string Thu)
        {
            var phongcapcuu = _unitOfWork.PhongCapCuuRepository.Get(u => u.RoomId == PhongCapCuuId);
            ViewBag.Thu = Thu;
            ViewBag.phongcapcuu = phongcapcuu;
            var updateworkschedule = _unitOfWork.WorkScheduleRepository.Get(u => u.PhongCapCuuId == PhongCapCuuId && u.DayOfWeek == Thu);
			var workschedulelist = _unitOfWork.WorkScheduleRepository.GetAll(u => u.DayOfWeek == Thu);
			var ca1doctorlist = new List<Doctor>();
			var ca2doctorlist = new List<Doctor>();
			var ca3doctorlist = new List<Doctor>();
			var doctorlist = _unitOfWork.DoctorRepository.GetAll();
			foreach (var doctor in doctorlist)
			{
				if (workschedulelist.Count() != 0)
				{
					bool flag1 = false;
					bool flag2 = false;
					bool flag3 = false;
					foreach (var workschedule in workschedulelist)
					{
						if (workschedule != null)
						{
							if (doctor.DoctorId == workschedule.DoctorId1) flag1 = true;
							if (doctor.DoctorId == workschedule.DoctorId2) flag2 = true;
							if (doctor.DoctorId == workschedule.DoctorId3) flag3 = true;
						}
					}
					if (flag1 == false) ca1doctorlist.Add(doctor);
					if (flag2 == false) ca2doctorlist.Add(doctor);
					if (flag3 == false) ca3doctorlist.Add(doctor);
				}
				else
				{
					ca1doctorlist.Add(doctor);
					ca2doctorlist.Add(doctor);
					ca3doctorlist.Add(doctor);
				}
			}
            if (updateworkschedule.DoctorId1!=null)
            {
                var doctor = _unitOfWork.DoctorRepository.Get(u => u.DoctorId == updateworkschedule.DoctorId1);
                ca1doctorlist.Add(doctor);
            }
			if (updateworkschedule.DoctorId2 != null)
			{
				var doctor = _unitOfWork.DoctorRepository.Get(u => u.DoctorId == updateworkschedule.DoctorId2);
				ca2doctorlist.Add(doctor);
			}
			if (updateworkschedule.DoctorId3 != null)
			{
				var doctor = _unitOfWork.DoctorRepository.Get(u => u.DoctorId == updateworkschedule.DoctorId3);
				ca3doctorlist.Add(doctor);
			}
			ViewBag.doctorlistca1 = ca1doctorlist.Select(u => new SelectListItem
			{
				Text = u.DoctorName + " " + u.DoctorId,
				Value = u.DoctorId.ToString()
			});
			ViewBag.doctorlistca2 = ca2doctorlist.Select(u => new SelectListItem
			{
				Text = u.DoctorName + " " + u.DoctorId,
				Value = u.DoctorId.ToString()
			});
			ViewBag.doctorlistca3 = ca3doctorlist.Select(u => new SelectListItem
			{
				Text = u.DoctorName + " " + u.DoctorId,
				Value = u.DoctorId.ToString()
			});
			return View(updateworkschedule);
        }
        [HttpPost("UpdateWorkScheduleForPhongCapCuu")]
        public IActionResult UpdateWorkScheduleForPhongCapCuu(WorkSchedule workSchedule)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.WorkScheduleRepository.Update(workSchedule);
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


        [HttpGet("Update")]
        public IActionResult Update(int PhongKhamId, string Thu)
        {
            var phongkham = _unitOfWork.PhongKhamRepository.Get(u => u.RoomId == PhongKhamId);
            ViewBag.Thu = Thu;
            ViewBag.phongkham = phongkham;
            var updateworkschedule = _unitOfWork.WorkScheduleRepository.Get(u => u.PhongKhamId == PhongKhamId && u.DayOfWeek == Thu);
			var workschedulelist = _unitOfWork.WorkScheduleRepository.GetAll(u => u.DayOfWeek == Thu);
			var ca1doctorlist = new List<Doctor>();
			var ca2doctorlist = new List<Doctor>();
			var ca3doctorlist = new List<Doctor>();
			var doctorlist = _unitOfWork.DoctorRepository.GetAll(u => u.ProfessionId == phongkham.ProfessionId);
			foreach (var doctor in doctorlist)
			{
				if (workschedulelist.Count() != 0)
				{
					bool flag1 = false;
					bool flag2 = false;
					bool flag3 = false;
					foreach (var workschedule in workschedulelist)
					{
						if (workschedule != null)
						{
							if (doctor.DoctorId == workschedule.DoctorId1) flag1 = true;
							if (doctor.DoctorId == workschedule.DoctorId2) flag2 = true;
							if (doctor.DoctorId == workschedule.DoctorId3) flag3 = true;
						}
					}
					if (flag1 == false) ca1doctorlist.Add(doctor);
					if (flag2 == false) ca2doctorlist.Add(doctor);
					if (flag3 == false) ca3doctorlist.Add(doctor);
				}
				else
				{
					ca1doctorlist.Add(doctor);
					ca2doctorlist.Add(doctor);
					ca3doctorlist.Add(doctor);
				}
			}
			if (updateworkschedule.DoctorId1 != null)
			{
				var doctor = _unitOfWork.DoctorRepository.Get(u => u.DoctorId == updateworkschedule.DoctorId1);
				ca1doctorlist.Add(doctor);
			}
			if (updateworkschedule.DoctorId2 != null)
			{
				var doctor = _unitOfWork.DoctorRepository.Get(u => u.DoctorId == updateworkschedule.DoctorId2);
				ca2doctorlist.Add(doctor);
			}
			if (updateworkschedule.DoctorId3 != null)
			{
				var doctor = _unitOfWork.DoctorRepository.Get(u => u.DoctorId == updateworkschedule.DoctorId3);
				ca3doctorlist.Add(doctor);
			}
			ViewBag.doctorlistca1 = ca1doctorlist.Select(u => new SelectListItem
			{
				Text = u.DoctorName + " " + u.DoctorId,
				Value = u.DoctorId.ToString()
			});
			ViewBag.doctorlistca2 = ca2doctorlist.Select(u => new SelectListItem
			{
				Text = u.DoctorName + " " + u.DoctorId,
				Value = u.DoctorId.ToString()
			});
			ViewBag.doctorlistca3 = ca3doctorlist.Select(u => new SelectListItem
			{
				Text = u.DoctorName + " " + u.DoctorId,
				Value = u.DoctorId.ToString()
			});

			return View(updateworkschedule);
        }
        [HttpPost("Update")]
        public IActionResult Update(WorkSchedule workSchedule)
        {
            if (ModelState.IsValid)
            {
                if (workSchedule.DoctorId1 == 0) workSchedule.DoctorId1 = null;
                if (workSchedule.DoctorId2 == 0) workSchedule.DoctorId2 = null;
                if (workSchedule.DoctorId3 == 0) workSchedule.DoctorId3 = null;
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
