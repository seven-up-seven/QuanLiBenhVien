using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;
using System.Numerics;
using static PhanMemWebQuanLiBenhVien.Ultilities.Utilities;

namespace PhanMemWebQuanLiBenhVien.Controllers
{
    public class PatientController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public PatientController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Avail()
        {
            var patients = _unitOfWork.PatientRepository.GetAll(pt => pt.TrangThaiBenhAn == ETrangThaiBenhAn.dangchuatri);
            return View("Index", patients); 
        }
        public IActionResult Index()
        {
            var PatientList = _unitOfWork.PatientRepository.GetAll();
            ViewBag.Professions = _unitOfWork.ProfessionRepository.GetAll().Select(u => new SelectListItem
            {
                Text = u.ProfessionName,
                Value=u.ProfessionId.ToString()
            });
            ViewBag.TrangThaiBenhAn = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = "Kết thúc chữa trị",
                        Value = ETrangThaiBenhAn.ketthucchuatri.ToString()
                    },
                    new SelectListItem
                    {
                        Text = "Đang chữa trị",
                        Value = ETrangThaiBenhAn.dangchuatri.ToString()
                    }
                },
                "Value",
                "Text"
            );
            return View(PatientList);
        }
        [HttpPost]
        public IActionResult Index(string SearchPatientName, string SearchPatientCCCD, int ProfessionId, string SearchTrangThaiBenhAn)
        {
            var patientlist = _unitOfWork.PatientRepository.GetAll();
            if (!string.IsNullOrEmpty(SearchPatientName)) patientlist=patientlist.Where(u => u.Name.ToLower().Contains(SearchPatientName.ToLower()));
            if (!string.IsNullOrEmpty(SearchPatientCCCD)) patientlist=patientlist.Where(u => u.CCCD.ToLower().Contains(SearchPatientCCCD.ToLower()));
            if (ProfessionId!=0) patientlist = patientlist.Where(u => u.ProfesisonId == ProfessionId);
            if (SearchTrangThaiBenhAn!="NoFilter") patientlist = patientlist.Where(u => u.TrangThaiBenhAn.ToString() == SearchTrangThaiBenhAn);
            ViewBag.Professions = _unitOfWork.ProfessionRepository.GetAll().Select(u => new SelectListItem
            {
                Text = u.ProfessionName,
                Value = u.ProfessionId.ToString()
            });
            ViewBag.TrangThaiBenhAn = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = "Kết thúc chữa trị",
                        Value = ETrangThaiBenhAn.ketthucchuatri.ToString()
                    },
                    new SelectListItem
                    {
                        Text = "Đang chữa trị",
                        Value = ETrangThaiBenhAn.dangchuatri.ToString()
                    }
                },
                "Value",
                "Text"
            );
            return View(patientlist);
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

            var statuslist=Enum.GetValues(typeof(ETrangThaiBenhAn))
            .Cast<ETrangThaiBenhAn>()
            .Where(e=>e==ETrangThaiBenhAn.dangchuatri || e==ETrangThaiBenhAn.ketthucchuatri)

            .Select(e=>new SelectListItem
            {
                Value=e.ToString(),
                Text=e.ToString()
            }).ToList();
            ViewBag.statuslist=statuslist;
            var phongkhamlist = _unitOfWork.PhongKhamRepository.GetAll().Select(p => new SelectListItem
            {
                Text=p.Name,
                Value=p.RoomId.ToString()
            });
            ViewBag.phongkhamlist=phongkhamlist;
            var doctorlist = _unitOfWork.DoctorRepository.GetAll().Select(p => new SelectListItem
            {
                Text = p.DoctorName,
                Value = p.DoctorId.ToString()
            });
            ViewBag.doctorlist= doctorlist;
            var phongbenhlist = _unitOfWork.PhongBenhRepository.GetAll().Select(p => new SelectListItem
            { 
                Text=p.Name,
                Value=p.RoomId.ToString()   
            });
            ViewBag.phongbenhlist=phongbenhlist;
            var nurselist = _unitOfWork.NurseRepository.GetAll().Select(p => new SelectListItem
            {
                Text=p.NurseName,
                Value=p.NurseId.ToString()
            });
            ViewBag.nurselist=nurselist;
            ViewBag.Professions = _unitOfWork.ProfessionRepository.GetAll().Select(u => new SelectListItem
            {
                Text = u.ProfessionName,
                Value = u.ProfessionId.ToString()
            });
            return View();
        }
        [HttpPost]
        public IActionResult Create(Patient patient)
        {
            if (ModelState.IsValid)
            {
                Doctor doctor = null;
                Nurse nurse = null;
                GlobalFunctions globalfunction = new GlobalFunctions(_unitOfWork, doctor, nurse, patient);
                if (globalfunction.ExistDuplicateCCCD())
                {
                    TempData["error"] = "CCCD đã tồn tại!";
                    var genderList = Enum.GetValues(typeof(EGender))
            .Cast<EGender>()
            .Select(gender => new SelectListItem
            {
                Value = gender.ToString(),
                Text = gender.ToString()
            }).ToList();
                    ViewBag.Genders = genderList;

                    var statuslist = Enum.GetValues(typeof(ETrangThaiBenhAn))
                    .Cast<ETrangThaiBenhAn>()
                    .Where(e => e == ETrangThaiBenhAn.dangchuatri || e == ETrangThaiBenhAn.ketthucchuatri)

                    .Select(e => new SelectListItem
                    {
                        Value = e.ToString(),
                        Text = e.ToString()
                    }).ToList();
                    ViewBag.statuslist = statuslist;
                    var phongkhamlist = _unitOfWork.PhongKhamRepository.GetAll().Select(p => new SelectListItem
                    {
                        Text = p.Name,
                        Value = p.RoomId.ToString()
                    });
                    ViewBag.phongkhamlist = phongkhamlist;
                    var doctorlist = _unitOfWork.DoctorRepository.GetAll().Select(p => new SelectListItem
                    {
                        Text = p.DoctorName,
                        Value = p.DoctorId.ToString()
                    });
                    ViewBag.doctorlist = doctorlist;
                    var phongbenhlist = _unitOfWork.PhongBenhRepository.GetAll().Select(p => new SelectListItem
                    {
                        Text = p.Name,
                        Value = p.RoomId.ToString()
                    });
                    ViewBag.phongbenhlist = phongbenhlist;
                    var nurselist = _unitOfWork.NurseRepository.GetAll().Select(p => new SelectListItem
                    {
                        Text = p.NurseName,
                        Value = p.NurseId.ToString()
                    });
                    ViewBag.nurselist = nurselist;
                    ViewBag.Professions = _unitOfWork.ProfessionRepository.GetAll().Select(u => new SelectListItem
                    {
                        Text = u.ProfessionName,
                        Value = u.ProfessionId.ToString()
                    });
                    return View();
                }
                else
                {
                    _unitOfWork.PatientRepository.Add(patient);
                    _unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                var genderList = Enum.GetValues(typeof(EGender))
            .Cast<EGender>()
            .Select(gender => new SelectListItem
            {
                Value = gender.ToString(),
                Text = gender.ToString()
            }).ToList();
                ViewBag.Genders = genderList;

                var statuslist = Enum.GetValues(typeof(ETrangThaiBenhAn))
                .Cast<ETrangThaiBenhAn>()
                .Where(e => e == ETrangThaiBenhAn.dangchuatri || e == ETrangThaiBenhAn.ketthucchuatri)

                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString()
                }).ToList();
                ViewBag.statuslist = statuslist;
                var phongkhamlist = _unitOfWork.PhongKhamRepository.GetAll().Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.RoomId.ToString()
                });
                ViewBag.phongkhamlist = phongkhamlist;
                var doctorlist = _unitOfWork.DoctorRepository.GetAll().Select(p => new SelectListItem
                {
                    Text = p.DoctorName,
                    Value = p.DoctorId.ToString()
                });
                ViewBag.doctorlist = doctorlist;
                var phongbenhlist = _unitOfWork.PhongBenhRepository.GetAll().Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.RoomId.ToString()
                });
                ViewBag.phongbenhlist = phongbenhlist;
                var nurselist = _unitOfWork.NurseRepository.GetAll().Select(p => new SelectListItem
                {
                    Text = p.NurseName,
                    Value = p.NurseId.ToString()
                });
                ViewBag.nurselist = nurselist;
                ViewBag.Professions = _unitOfWork.ProfessionRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.ProfessionName,
                    Value = u.ProfessionId.ToString()
                });
                return View();
            }
        }
        public IActionResult Update(int PatientId)
        {
            var patient = _unitOfWork.PatientRepository.Get(u => u.PatientId == PatientId);
            var genderList = Enum.GetValues(typeof(EGender))
            .Cast<EGender>()
            .Select(gender => new SelectListItem
            {
                Value = gender.ToString(),
                Text = gender.ToString()
            }).ToList();
            ViewBag.Genders = genderList;
            ViewBag.BHYT = new SelectList(new List<SelectListItem>
            {
                new SelectListItem {Text="Có", Value="true"},
                new SelectListItem {Text="Không", Value="false"}
            }, "Value", "Text");
            var statuslist = Enum.GetValues(typeof(ETrangThaiBenhAn))
            .Cast<ETrangThaiBenhAn>()
            .Where(e => e == ETrangThaiBenhAn.dangchuatri || e == ETrangThaiBenhAn.ketthucchuatri)
            .Select(e => new SelectListItem
            {
                Value = e.ToString(),
                Text = e.ToString()
            }).ToList();
            ViewBag.statuslist = statuslist;
            var phongkhamlist = _unitOfWork.PhongKhamRepository.GetAll().Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.RoomId.ToString()
            });
            ViewBag.phongkhamlist = phongkhamlist;
            var doctorlist = _unitOfWork.DoctorRepository.GetAll().Select(p => new SelectListItem
            {
                Text = p.DoctorName,
                Value = p.DoctorId.ToString()
            });
            ViewBag.doctorlist = doctorlist;
            var phongbenhlist = _unitOfWork.PhongBenhRepository.GetAll().Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.RoomId.ToString()
            });
            ViewBag.phongbenhlist = phongbenhlist;
            var nurselist = _unitOfWork.NurseRepository.GetAll().Select(p => new SelectListItem
            {
                Text = p.NurseName,
                Value = p.NurseId.ToString()
            });
            ViewBag.nurselist = nurselist;
            var specialstatuslist = Enum.GetValues(typeof(ETrangThaiBenhAn))
            .Cast<ETrangThaiBenhAn>()
            .Where(e => e == ETrangThaiBenhAn.dangchuatri || e == ETrangThaiBenhAn.ketthucchuatri)
            .Select(e => new SelectListItem
            {
                Value = e.ToString(),
                Text = e.ToString()
            }).ToList();
            ViewBag.specialstatuslist = specialstatuslist;
            ViewBag.Professions = _unitOfWork.ProfessionRepository.GetAll().Select(u => new SelectListItem
            {
                Text = u.ProfessionName,
                Value = u.ProfessionId.ToString()
            });
            return View(patient);
        }
        [HttpPost]
        public IActionResult Update(Patient patient)
        {
            if (ModelState.IsValid)
            {
                Doctor doctor = null;
                Nurse nurse = null;
                GlobalFunctions globalfunction = new GlobalFunctions(_unitOfWork, doctor, nurse, patient);
                if (globalfunction.ExistDuplicateCCCD())
                {
                    TempData["error"] = "CCCD đã tồn tại!";
                    var genderList = Enum.GetValues(typeof(EGender))
                    .Cast<EGender>()
                    .Select(gender => new SelectListItem
                    {
                        Value = gender.ToString(),
                        Text = gender.ToString()
                    }).ToList();
                        ViewBag.Genders = genderList;
                        ViewBag.BHYT = new SelectList(new List<SelectListItem>
                    {
                        new SelectListItem {Text="Có", Value="true"},
                        new SelectListItem {Text="Không", Value="false"}
                    }, "Value", "Text");
                    var statuslist = Enum.GetValues(typeof(ETrangThaiBenhAn))
                    .Cast<ETrangThaiBenhAn>()
                    .Where(e => e == ETrangThaiBenhAn.dangchuatri || e == ETrangThaiBenhAn.ketthucchuatri)
                    .Select(e => new SelectListItem
                    {
                        Value = e.ToString(),
                        Text = e.ToString()
                    }).ToList();
                    ViewBag.statuslist = statuslist;
                    var phongkhamlist = _unitOfWork.PhongKhamRepository.GetAll().Select(p => new SelectListItem
                    {
                        Text = p.Name,
                        Value = p.RoomId.ToString()
                    });
                    ViewBag.phongkhamlist = phongkhamlist;
                    var doctorlist = _unitOfWork.DoctorRepository.GetAll().Select(p => new SelectListItem
                    {
                        Text = p.DoctorName,
                        Value = p.DoctorId.ToString()
                    });
                    ViewBag.doctorlist = doctorlist;
                    var phongbenhlist = _unitOfWork.PhongBenhRepository.GetAll().Select(p => new SelectListItem
                    {
                        Text = p.Name,
                        Value = p.RoomId.ToString()
                    });
                    ViewBag.phongbenhlist = phongbenhlist;
                    var nurselist = _unitOfWork.NurseRepository.GetAll().Select(p => new SelectListItem
                    {
                        Text = p.NurseName,
                        Value = p.NurseId.ToString()
                    });
                    ViewBag.nurselist = nurselist;
                    var specialstatuslist = Enum.GetValues(typeof(ETrangThaiBenhAn))
                    .Cast<ETrangThaiBenhAn>()
                    .Where(e => e == ETrangThaiBenhAn.dangchuatri || e == ETrangThaiBenhAn.ketthucchuatri)
                    .Select(e => new SelectListItem
                    {
                        Value = e.ToString(),
                        Text = e.ToString()
                    }).ToList();
                    ViewBag.specialstatuslist = specialstatuslist;
                    ViewBag.Professions = _unitOfWork.ProfessionRepository.GetAll().Select(u => new SelectListItem
                    {
                        Text = u.ProfessionName,
                        Value = u.ProfessionId.ToString()
                    });
                    return View(patient);
                }
                else
                {
                    _unitOfWork.PatientRepository.Update(patient);
                    var medicalrecordlist = _unitOfWork.MedicalRecordRepository.GetAll(mr => mr.PatientId == patient.PatientId);
                    foreach (var medicalrecord in medicalrecordlist)
                    {
                        medicalrecord.PatientName = patient.Name;
                        medicalrecord.PatientGender = patient.Gender.ToString();
                        medicalrecord.Address = patient.Address;
                        _unitOfWork.MedicalRecordRepository.Update(medicalrecord);
                    }
                    _unitOfWork.Save();
                    TempData["success"] = "Cập nhật bệnh nhân thành công!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                var genderList = Enum.GetValues(typeof(EGender))
                .Cast<EGender>()
                .Select(gender => new SelectListItem
                {
                    Value = gender.ToString(),
                    Text = gender.ToString()
                }).ToList();
                    ViewBag.Genders = genderList;
                    ViewBag.BHYT = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem {Text="Có", Value="true"},
                    new SelectListItem {Text="Không", Value="false"}
                }, "Value", "Text");
                var statuslist = Enum.GetValues(typeof(ETrangThaiBenhAn))
                .Cast<ETrangThaiBenhAn>()
                .Where(e => e == ETrangThaiBenhAn.dangchuatri || e == ETrangThaiBenhAn.ketthucchuatri)
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString()
                }).ToList();
                ViewBag.statuslist = statuslist;
                var phongkhamlist = _unitOfWork.PhongKhamRepository.GetAll().Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.RoomId.ToString()
                });
                ViewBag.phongkhamlist = phongkhamlist;
                var doctorlist = _unitOfWork.DoctorRepository.GetAll().Select(p => new SelectListItem
                {
                    Text = p.DoctorName,
                    Value = p.DoctorId.ToString()
                });
                ViewBag.doctorlist = doctorlist;
                var phongbenhlist = _unitOfWork.PhongBenhRepository.GetAll().Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.RoomId.ToString()
                });
                ViewBag.phongbenhlist = phongbenhlist;
                var nurselist = _unitOfWork.NurseRepository.GetAll().Select(p => new SelectListItem
                {
                    Text = p.NurseName,
                    Value = p.NurseId.ToString()
                });
                ViewBag.nurselist = nurselist;
                var specialstatuslist = Enum.GetValues(typeof(ETrangThaiBenhAn))
                .Cast<ETrangThaiBenhAn>()
                .Where(e => e == ETrangThaiBenhAn.dangchuatri || e == ETrangThaiBenhAn.ketthucchuatri)
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString()
                }).ToList();
                ViewBag.specialstatuslist = specialstatuslist;
                ViewBag.Professions = _unitOfWork.ProfessionRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.ProfessionName,
                    Value = u.ProfessionId.ToString()
                });
                return View(patient);
            }
        }

        public IActionResult Delete(int PatientId)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Moderator"))
            {
                var patient = _unitOfWork.PatientRepository.Get(u => u.PatientId == PatientId);
                _unitOfWork.PatientRepository.Remove(patient);
                _unitOfWork.Save();
                TempData["success"] = "Xoá bệnh nhân thành công"; 
                return RedirectToAction("Index");
            }
            else if (User.IsInRole("Doctor"))
            {
                TempData["error"] = "Bác sĩ không thể xóa bệnh nhân";
                var doctor = _unitOfWork.DoctorRepository.Get(dr => dr.Username == User.Identity.Name);
                return RedirectToAction("DoctorPatients", "Doctor", new { DoctorId = doctor.DoctorId });
            }
            //nurse 
            else return View();
        }
        public IActionResult Detail(int PatientId)
        {
            var patient = _unitOfWork.PatientRepository.Get(u => u.PatientId == PatientId);
            //if (patient.DoctorId != null) patient.Doctor = _unitOfWork.DoctorRepository.Get(u => u.DoctorId == patient.DoctorId);
            //if (patient.NurseId != null) patient.Nurse = _unitOfWork.NurseRepository.Get(u => u.NurseId == patient.NurseId);
            //if (patient.PhongBenhId != null) patient.PhongBenh = _unitOfWork.PhongBenhRepository.Get(u => u.RoomId == patient.PhongBenhId);
            //if (patient.PhongKhamId != null) patient.PhongKham = _unitOfWork.PhongKhamRepository.Get(u => u.RoomId == patient.PhongKhamId);
            patient.MedicalRecords = _unitOfWork.MedicalRecordRepository.GetAll(u => u.PatientId == patient.PatientId).ToList();
            patient.Profession = _unitOfWork.ProfessionRepository.Get(u => u.ProfessionId == patient.ProfesisonId); 
            return View(patient);
        }
    }
}
