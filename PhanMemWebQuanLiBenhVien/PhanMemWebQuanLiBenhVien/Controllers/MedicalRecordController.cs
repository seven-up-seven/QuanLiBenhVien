using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;
using static PhanMemWebQuanLiBenhVien.Ultilities.Utilities;
using static System.Net.Mime.MediaTypeNames;

namespace PhanMemWebQuanLiBenhVien.Controllers
{
    [Route("MedicalRecord")]
    public class MedicalRecordController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public MedicalRecordController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            var listMedicalRecord = _unitOfWork.MedicalRecordRepository.GetAll();
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
            return View(listMedicalRecord);
        }
        [HttpPost("Index")]
        public IActionResult Index(string SearchPatientName, string SearchPatientCCCD, int ProfessionId, string SearchTrangThaiBenhAn, int SearchID)
        {
            var listMedicalRecord = _unitOfWork.MedicalRecordRepository.GetAll();
            foreach(var medicalrecord in listMedicalRecord)
            {
                medicalrecord.Patient=_unitOfWork.PatientRepository.Get(u=>u.PatientId==medicalrecord.PatientId);
            }
            if (!string.IsNullOrEmpty(SearchPatientName)) listMedicalRecord = listMedicalRecord.Where(u => u.PatientName.ToLower().Contains(SearchPatientName.ToLower()));
            if (!string.IsNullOrEmpty(SearchPatientCCCD)) listMedicalRecord = listMedicalRecord.Where(u => u.Patient.CCCD.Contains(SearchPatientCCCD));
            if (ProfessionId != 0) listMedicalRecord = listMedicalRecord.Where(u=>u.ProfesisonId==ProfessionId);
            if (SearchTrangThaiBenhAn != "NoFilter") listMedicalRecord = listMedicalRecord.Where(u => u.TrangThaiBenhAn.ToString() == SearchTrangThaiBenhAn);
            if(SearchID != null)
            {
                listMedicalRecord = listMedicalRecord.Where(u => u.MedicalRecordId == SearchID); 
            }
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
            return View(listMedicalRecord);
        }

        [HttpGet("AdminCreate")]
        public IActionResult AdminCreate()
        {
            var patientList = _unitOfWork.PatientRepository.GetAll(u=>u.MedicalRecords==null || u.TrangThaiBenhAn==ETrangThaiBenhAn.ketthucchuatri);

            // Tạo danh sách PatientId và tên để truyền vào ViewBag
            ViewBag.PatientList = patientList.Select(u =>
                new SelectListItem
                {
                    Text = $"ID: {u.PatientId} Tên: {u.Name}",
                    Value = u.PatientId.ToString()
                }).ToList();
            ViewBag.Professions = _unitOfWork.ProfessionRepository.GetAll().Select(u => new SelectListItem
            {
                Text = u.ProfessionName,
                Value = u.ProfessionId.ToString()
            });
            // Cung cấp thông tin tên bệnh nhân cho javascript
            ViewBag.PatientNames = patientList.ToDictionary(u => u.PatientId.ToString(), u => u.Name);
            ViewBag.PatientGenders = patientList.ToDictionary(u => u.PatientId, u => u.Gender.ToString());
            ViewBag.PatientAddress = patientList.ToDictionary(u => u.PatientId, u => u.Address);
            ViewBag.statuslist = Enum.GetValues(typeof(ETrangThaiDieuTri))
                                       .Cast<ETrangThaiDieuTri>()
                                       .Select(e => new SelectListItem
                                       {
                                           Value = e.ToString(),
                                           Text = e.ToString()
                                       }).ToList();
            var doctorList = _unitOfWork.DoctorRepository.GetAll()
                        .Select(d => new SelectListItem
                        {
                            Value = d.DoctorId.ToString(),
                            Text = d.DoctorName
                        }).ToList();
            ViewBag.doctorlist = doctorList;

            // Fetch and set the nurse list
            var nurseList = _unitOfWork.NurseRepository.GetAll()
                                .Select(n => new SelectListItem
                                {
                                    Value = n.NurseId.ToString(),
                                    Text = n.NurseName
                                }).ToList();
            ViewBag.nurselist = nurseList;

            // Fetch and set the PhongBenh list
            var phongBenhList = _unitOfWork.PhongBenhRepository.GetAll()
                                .Where(pb =>
                                {
                                    var patients = _unitOfWork.PatientRepository.GetAll();
                                    foreach (var patient in patients)
                                    {
                                        patient.MedicalRecords = _unitOfWork.MedicalRecordRepository.GetAll(mr => mr.PatientId == patient.PatientId).ToList();
                                    }
                                    var validPatients = patients.Where(patient =>
                                    {
                                        var medicalRecords = _unitOfWork.MedicalRecordRepository.GetAll(mr => mr.PatientId == patient.PatientId && mr.PhongBenhId == pb.RoomId).ToList();
                                        patient.MedicalRecords = (ICollection<MedicalRecord>?)medicalRecords;
                                        return patient.TrangThaiBenhAn == Ultilities.Utilities.ETrangThaiBenhAn.dangchuatri &&
                                               medicalRecords.LastOrDefault() != null &&
                                               medicalRecords.LastOrDefault().TrangThaiBenhAn == Ultilities.Utilities.ETrangThaiBenhAn.dangchuatri &&
                                               medicalRecords.LastOrDefault().TrangThaiDieuTri == Ultilities.Utilities.ETrangThaiDieuTri.noitru;
                                    }).ToList();

                                    return validPatients.Count < pb.NumberOfBeds;
                                })
                                .Select(pb => new SelectListItem
                                {
                                    Value = pb.RoomId.ToString(),
                                    Text = pb.Name
                                }).ToList();

            ViewBag.phongbenhlist = phongBenhList;

            ViewBag.TinhTrangBenhNhan = Enum.GetValues(typeof(ETinhTrangBenhNhan))
                                           .Cast<ETinhTrangBenhNhan>()
                                           .Select(e => new SelectListItem
                                           {
                                               Value = e.ToString(),
                                               Text = e.ToString()
                                           }).ToList();

            // Fetch and set the PhongKham list
            var phongKhamList = _unitOfWork.PhongKhamRepository.GetAll()
                                .Select(pk => new SelectListItem
                                {
                                    Value = pk.RoomId.ToString(),
                                    Text = pk.Name
                                }).ToList();
            ViewBag.phongkhamlist = phongKhamList;
            return View();
        }
        [HttpPost("AdminCreate")]
        public IActionResult AdminCreate(MedicalRecord medicalRecord)
        {
            if (ModelState.IsValid)
            {
                int PatientId = medicalRecord.PatientId;
                var patient = _unitOfWork.PatientRepository.Get(pt => pt.PatientId == PatientId);
                patient.TrangThaiBenhAn = ETrangThaiBenhAn.dangchuatri;
                medicalRecord.TrangThaiBenhAn = ETrangThaiBenhAn.dangchuatri;
                medicalRecord.ProfesisonId = patient.ProfesisonId;
                _unitOfWork.PatientRepository.Update(patient);
                _unitOfWork.MedicalRecordRepository.Add(medicalRecord);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            var patientList = _unitOfWork.PatientRepository.GetAll(u => u.MedicalRecords == null || u.TrangThaiBenhAn == ETrangThaiBenhAn.ketthucchuatri);

            // Tạo danh sách PatientId và tên để truyền vào ViewBag
            ViewBag.PatientList = patientList.Select(u =>
                new SelectListItem
                {
                    Text = $"ID: {u.PatientId} Tên: {u.Name}",
                    Value = u.PatientId.ToString()
                }).ToList();
            ViewBag.Professions = _unitOfWork.ProfessionRepository.GetAll().Select(u => new SelectListItem
            {
                Text = u.ProfessionName,
                Value = u.ProfessionId.ToString()
            });
            // Cung cấp thông tin tên bệnh nhân cho javascript
            ViewBag.PatientNames = patientList.ToDictionary(u => u.PatientId.ToString(), u => u.Name);
            ViewBag.PatientGenders = patientList.ToDictionary(u => u.PatientId, u => u.Gender.ToString());
            ViewBag.PatientAddress = patientList.ToDictionary(u => u.PatientId, u => u.Address);
            ViewBag.statuslist = Enum.GetValues(typeof(ETrangThaiDieuTri))
                                       .Cast<ETrangThaiDieuTri>()
                                       .Select(e => new SelectListItem
                                       {
                                           Value = e.ToString(),
                                           Text = e.ToString()
                                       }).ToList();
            var doctorList = _unitOfWork.DoctorRepository.GetAll()
                        .Select(d => new SelectListItem
                        {
                            Value = d.DoctorId.ToString(),
                            Text = d.DoctorName
                        }).ToList();
            ViewBag.doctorlist = doctorList;

            // Fetch and set the nurse list
            var nurseList = _unitOfWork.NurseRepository.GetAll()
                                .Select(n => new SelectListItem
                                {
                                    Value = n.NurseId.ToString(),
                                    Text = n.NurseName
                                }).ToList();
            ViewBag.nurselist = nurseList;

            // Fetch and set the PhongBenh list
            var phongBenhList = _unitOfWork.PhongBenhRepository.GetAll()
                                .Where(pb =>
                                {
                                    var patients = _unitOfWork.PatientRepository.GetAll();
                                    foreach (var patient in patients)
                                    {
                                        patient.MedicalRecords = _unitOfWork.MedicalRecordRepository.GetAll(mr => mr.PatientId == patient.PatientId).ToList();
                                    }
                                    var validPatients = patients.Where(patient =>
                                    {
                                        var medicalRecords = _unitOfWork.MedicalRecordRepository.GetAll(mr => mr.PatientId == patient.PatientId && mr.PhongBenhId == pb.RoomId).ToList();
                                        patient.MedicalRecords = (ICollection<MedicalRecord>?)medicalRecords;
                                        return patient.TrangThaiBenhAn == Ultilities.Utilities.ETrangThaiBenhAn.dangchuatri &&
                                               medicalRecords.LastOrDefault() != null &&
                                               medicalRecords.LastOrDefault().TrangThaiBenhAn == Ultilities.Utilities.ETrangThaiBenhAn.dangchuatri &&
                                               medicalRecords.LastOrDefault().TrangThaiDieuTri == Ultilities.Utilities.ETrangThaiDieuTri.noitru;
                                    }).ToList();

                                    return validPatients.Count < pb.NumberOfBeds;
                                })
                                .Select(pb => new SelectListItem
                                {
                                    Value = pb.RoomId.ToString(),
                                    Text = pb.Name
                                }).ToList();

            ViewBag.phongbenhlist = phongBenhList;

            ViewBag.TinhTrangBenhNhan = Enum.GetValues(typeof(ETinhTrangBenhNhan))
                                           .Cast<ETinhTrangBenhNhan>()
                                           .Select(e => new SelectListItem
                                           {
                                               Value = e.ToString(),
                                               Text = e.ToString()
                                           }).ToList();

            // Fetch and set the PhongKham list
            var phongKhamList = _unitOfWork.PhongKhamRepository.GetAll()
                                .Select(pk => new SelectListItem
                                {
                                    Value = pk.RoomId.ToString(),
                                    Text = pk.Name
                                }).ToList();
            ViewBag.phongkhamlist = phongKhamList;
            return View();
;        }

        [HttpGet("DoctorCreate/{PatientId}")]
        public IActionResult DoctorCreate(int PatientId)
        {
            var patient = _unitOfWork.PatientRepository.Get(pt => pt.PatientId == PatientId);
            patient.Profession = _unitOfWork.ProfessionRepository.Get(pf => pf.ProfessionId == patient.ProfesisonId);
            ViewBag.Patient = patient;
            ViewBag.PatientId = PatientId;
            ViewBag.statuslist = Enum.GetValues(typeof(ETrangThaiDieuTri))
                                       .Cast<ETrangThaiDieuTri>()
                                       .Select(e => new SelectListItem
                                       {
                                           Value = e.ToString(),
                                           Text = e.ToString()
                                       }).ToList();
            var doctorList = _unitOfWork.DoctorRepository.GetAll(dr=> dr.ProfessionId == patient.ProfesisonId)
                        .Select(d => new SelectListItem
                        {
                            Value = d.DoctorId.ToString(),
                            Text = d.DoctorName
                        }).ToList();
            ViewBag.doctorlist = doctorList;

            // Fetch and set the nurse list
            var nurseList = _unitOfWork.NurseRepository.GetAll()
                                .Select(n => new SelectListItem
                                {
                                    Value = n.NurseId.ToString(),
                                    Text = n.NurseName
                                }).ToList();
            ViewBag.nurselist = nurseList;

            // Fetch and set the PhongBenh list
            var phongBenhList = _unitOfWork.PhongBenhRepository.GetAll(pb => pb.ProfessionId == patient.ProfesisonId)
                                .Where(pb =>
                                {
                                    var patients = _unitOfWork.PatientRepository.GetAll();
                                    foreach(var patient in patients)
                                    {
                                        patient.MedicalRecords = _unitOfWork.MedicalRecordRepository.GetAll(mr => mr.PatientId == patient.PatientId).ToList();
                                    }
                                    var validPatients = patients.Where(patient =>
                                    {
                                        var medicalRecords = _unitOfWork.MedicalRecordRepository.GetAll(mr => mr.PatientId == patient.PatientId && mr.PhongBenhId == pb.RoomId).ToList();
                                        patient.MedicalRecords = (ICollection<MedicalRecord>?)medicalRecords;
                                        return patient.TrangThaiBenhAn == Ultilities.Utilities.ETrangThaiBenhAn.dangchuatri &&
                                               medicalRecords.LastOrDefault() != null &&
                                               medicalRecords.LastOrDefault().TrangThaiBenhAn == Ultilities.Utilities.ETrangThaiBenhAn.dangchuatri &&
                                               medicalRecords.LastOrDefault().TrangThaiDieuTri == Ultilities.Utilities.ETrangThaiDieuTri.noitru;
                                    }).ToList();

                                    return validPatients.Count < pb.NumberOfBeds;
                                })
                                .Select(pb => new SelectListItem
                                {
                                    Value = pb.RoomId.ToString(),
                                    Text = pb.Name
                                }).ToList();

            ViewBag.phongbenhlist = phongBenhList;

            ViewBag.TinhTrangBenhNhan = Enum.GetValues(typeof(ETinhTrangBenhNhan))
                                           .Cast<ETinhTrangBenhNhan>()
                                           .Select(e => new SelectListItem
                                           {
                                               Value = e.ToString(),
                                               Text = e.ToString()
                                           }).ToList();

            // Fetch and set the PhongKham list
            var phongKhamList = _unitOfWork.PhongKhamRepository.GetAll(pk => pk.ProfessionId == patient.ProfesisonId)
                                .Select(pk => new SelectListItem
                                {
                                    Value = pk.RoomId.ToString(),
                                    Text = pk.Name
                                }).ToList();
            ViewBag.phongkhamlist = phongKhamList;
            ViewBag.Professions = _unitOfWork.ProfessionRepository.GetAll().Select(u => new SelectListItem
            {
                Text = u.ProfessionName,
                Value = u.ProfessionId.ToString()
            });
            return View();
        }
        [HttpPost("DoctorCreate/{PatientId}")]
        public IActionResult DoctorCreate(MedicalRecord medicalRecord, int PatientId)
        {
            if (ModelState.IsValid)
            {
                if((medicalRecord.DoctorId == null || medicalRecord.NurseId == null || medicalRecord.PhongBenhId == null) && (medicalRecord.PhongKhamId == null))
                {
                    var patient = _unitOfWork.PatientRepository.Get(pt => pt.PatientId == PatientId);
                    patient.Profession = _unitOfWork.ProfessionRepository.Get(pf => pf.ProfessionId == patient.ProfesisonId);
                    ViewBag.Patient = patient;
                    ViewBag.PatientId = PatientId;
                    ViewBag.statuslist = Enum.GetValues(typeof(ETrangThaiDieuTri))
                                               .Cast<ETrangThaiDieuTri>()
                                               .Select(e => new SelectListItem
                                               {
                                                   Value = e.ToString(),
                                                   Text = e.ToString()
                                               }).ToList();
                    var doctorList = _unitOfWork.DoctorRepository.GetAll(dr => dr.ProfessionId == patient.ProfesisonId)
                                .Select(d => new SelectListItem
                                {
                                    Value = d.DoctorId.ToString(),
                                    Text = d.DoctorName
                                }).ToList();
                    ViewBag.doctorlist = doctorList;

                    // Fetch and set the nurse list
                    var nurseList = _unitOfWork.NurseRepository.GetAll()
                                        .Select(n => new SelectListItem
                                        {
                                            Value = n.NurseId.ToString(),
                                            Text = n.NurseName
                                        }).ToList();
                    ViewBag.nurselist = nurseList;

                    // Fetch and set the PhongBenh list
                    var phongBenhList = _unitOfWork.PhongBenhRepository.GetAll(pb => pb.ProfessionId == patient.ProfesisonId)
                                        .Where(pb =>
                                        {
                                            var patients = _unitOfWork.PatientRepository.GetAll();
                                            foreach (var patient in patients)
                                            {
                                                patient.MedicalRecords = _unitOfWork.MedicalRecordRepository.GetAll(mr => mr.PatientId == patient.PatientId).ToList();
                                            }
                                            var validPatients = patients.Where(patient =>
                                            {
                                                var medicalRecords = _unitOfWork.MedicalRecordRepository.GetAll(mr => mr.PatientId == patient.PatientId && mr.PhongBenhId == pb.RoomId).ToList();
                                                patient.MedicalRecords = (ICollection<MedicalRecord>?)medicalRecords;
                                                return patient.TrangThaiBenhAn == Ultilities.Utilities.ETrangThaiBenhAn.dangchuatri &&
                                                       medicalRecords.LastOrDefault() != null &&
                                                       medicalRecords.LastOrDefault().TrangThaiBenhAn == Ultilities.Utilities.ETrangThaiBenhAn.dangchuatri &&
                                                       medicalRecords.LastOrDefault().TrangThaiDieuTri == Ultilities.Utilities.ETrangThaiDieuTri.noitru;
                                            }).ToList();

                                            return validPatients.Count < pb.NumberOfBeds;
                                        })
                                        .Select(pb => new SelectListItem
                                        {
                                            Value = pb.RoomId.ToString(),
                                            Text = pb.Name
                                        }).ToList();

                    ViewBag.phongbenhlist = phongBenhList;

                    ViewBag.TinhTrangBenhNhan = Enum.GetValues(typeof(ETinhTrangBenhNhan))
                                                   .Cast<ETinhTrangBenhNhan>()
                                                   .Select(e => new SelectListItem
                                                   {
                                                       Value = e.ToString(),
                                                       Text = e.ToString()
                                                   }).ToList();

                    // Fetch and set the PhongKham list
                    var phongKhamList = _unitOfWork.PhongKhamRepository.GetAll(pk => pk.ProfessionId == patient.ProfesisonId)
                                        .Select(pk => new SelectListItem
                                        {
                                            Value = pk.RoomId.ToString(),
                                            Text = pk.Name
                                        }).ToList();
                    ViewBag.phongkhamlist = phongKhamList;
                    ViewBag.Professions = _unitOfWork.ProfessionRepository.GetAll().Select(u => new SelectListItem
                    {
                        Text = u.ProfessionName,
                        Value = u.ProfessionId.ToString()
                    });

                    TempData["error"] = "Nhập thiếu thông tin";
                    return View();
                }
                var pnt = _unitOfWork.PatientRepository.Get(pt => pt.PatientId == PatientId);
                pnt.TrangThaiBenhAn = ETrangThaiBenhAn.dangchuatri;
                medicalRecord.TrangThaiBenhAn = ETrangThaiBenhAn.dangchuatri; 
                _unitOfWork.PatientRepository.Update(pnt);
                _unitOfWork.MedicalRecordRepository.Add(medicalRecord);
                _unitOfWork.Save();
                return RedirectToAction("DoctorPatientDetail", "Doctor", new {PatientId = medicalRecord.PatientId});
            }
            return RedirectToAction("DoctorCreate");
        }



        [HttpGet("DoctorDetail/{MedicalRecordId}")]
        public IActionResult DoctorDetail(int MedicalRecordId)
        {
            var medicalRecord = _unitOfWork.MedicalRecordRepository.Get(mr => mr.MedicalRecordId == MedicalRecordId);
            medicalRecord.Doctor = _unitOfWork.DoctorRepository.Get(dr => dr.DoctorId == medicalRecord.DoctorId);
            medicalRecord.Nurse = _unitOfWork.NurseRepository.Get(nr => nr.NurseId == medicalRecord.NurseId);
            medicalRecord.PhongBenh = _unitOfWork.PhongBenhRepository.Get(pb => pb.RoomId == medicalRecord.PhongBenhId);
            medicalRecord.PhongKham = _unitOfWork.PhongKhamRepository.Get(pk => pk.RoomId == medicalRecord.PhongKhamId);
            ViewBag.MedicalVisits = _unitOfWork.MedicalVisitRepository.GetAll(mv => mv.MedicalRecordId == MedicalRecordId);
            var refererUrl = Request.Headers["Referer"].ToString();
            ViewBag.ReferUrl = refererUrl;
            medicalRecord.Profession = _unitOfWork.ProfessionRepository.Get(pf => pf.ProfessionId == medicalRecord.ProfesisonId);
            return View("DoctorDetail", medicalRecord);
        }

        [HttpGet("AdminDetail/{MedicalRecordId}")]
        public IActionResult AdminDetail(int MedicalRecordId)
        {
            var medicalRecord = _unitOfWork.MedicalRecordRepository.Get(mr => mr.MedicalRecordId == MedicalRecordId);
            ViewBag.MedicalVisits = _unitOfWork.MedicalVisitRepository.GetAll(mv => mv.MedicalRecordId == MedicalRecordId);
            return View("AdminDetail", medicalRecord);
        }

        [HttpGet("Update/{MedicalRecordId}")]
        public IActionResult Update(int MedicalRecordId)
        {
            var medicalRecord = _unitOfWork.MedicalRecordRepository.Get(mr => mr.MedicalRecordId == MedicalRecordId);
            if (medicalRecord != null)
            {
                return View(medicalRecord);
            }
            return View(MedicalRecordId);
        }
        [HttpPost("Update/{MedicalRecordId}")]
        public IActionResult Update(MedicalRecord medicalRecord)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.MedicalRecordRepository.Update(medicalRecord);
                _unitOfWork.Save();
                if (User.IsInRole("Doctor"))
                {
                    return RedirectToAction("DoctorDetail", new { MedicalRecordId = medicalRecord.MedicalRecordId });
                }
                return RedirectToAction("Index");
            }
            return View(medicalRecord.MedicalRecordId);
        }

        [HttpPost("Delete/{MedicalRecordId}")]
        public IActionResult Delete(int MedicalRecordId)
        {
            var medicalRecord = _unitOfWork.MedicalRecordRepository.Get(mr => mr.MedicalRecordId == MedicalRecordId);
            if (medicalRecord != null && medicalRecord.TrangThaiBenhAn == ETrangThaiBenhAn.ketthucchuatri)
            {
                _unitOfWork.MedicalRecordRepository.Remove(medicalRecord);
                _unitOfWork.Save();
                TempData["success"] = "Xoá thành công"; 
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Bệnh án đang trong quá trình điều trị không thể xoá"; 
            }
            return RedirectToAction("Index");
        }

        [HttpGet("CreateMedicalVisit/{MedicalRecordId}")]
        public IActionResult CreateMedicalVisit(int MedicalRecordId)
        {
            ViewBag.MedicalRecordId = MedicalRecordId;
            ViewBag.TinhTrangBenhNhan = Enum.GetValues(typeof(ETinhTrangBenhNhan))
                                          .Cast<ETinhTrangBenhNhan>()       
                                          .Select(e => new SelectListItem
                                          {
                                              Value = e.ToString(),
                                              Text = e.ToString()
                                          }).ToList();
            ViewBag.ThuocList = _unitOfWork.MedicineRepository.GetAll(m => m.ExpiryDate > DateTime.Now && m.Quantity > 0)
                                    .Select(m => new SelectListItem
                                    {
                                        Value = m.MedicineId.ToString(),
                                        Text = "Tên: " + m.Name + " | Đơn vị: " + m.Unit + " | Số lượng trong kho: " + m.Quantity.ToString()
                                    }).ToList();
            
            return View();
        }
        [HttpPost("CreateMedicalVisit/{MedicalRecordId}")]
        public IActionResult CreateMedicalVisit(MedicalVisit medicalVisit, int MedicalRecordId)
        {
            var medicalRecord = _unitOfWork.MedicalRecordRepository.Get(mr => mr.MedicalRecordId == medicalVisit.MedicalRecordId);
            medicalRecord.TinhTrangBenhNhan = medicalVisit.TinhTrangBenhNhan;
            if (ModelState.IsValid)
            {
                _unitOfWork.MedicalVisitRepository.Add(medicalVisit);
                _unitOfWork.MedicalRecordRepository.Update(medicalRecord);
                _unitOfWork.Save();
                return RedirectToAction("DoctorDetail", new { MedicalRecordId = medicalVisit.MedicalRecordId });
            }
            else
            {
                ViewBag.MedicalRecordId = MedicalRecordId;
                ViewBag.TinhTrangBenhNhan = Enum.GetValues(typeof(ETinhTrangBenhNhan))
                                              .Cast<ETinhTrangBenhNhan>()
                                              .Select(e => new SelectListItem
                                              {
                                                  Value = e.ToString(),
                                                  Text = e.ToString()
                                              }).ToList();
                ViewBag.ThuocList = _unitOfWork.MedicineRepository.GetAll(m => m.ExpiryDate > DateTime.Now)
                                        .Select(m => new SelectListItem
                                        {
                                            Value = m.MedicineId.ToString(),
                                            Text = "Tên: " + m.Name + " | Đơn vị: " + m.Unit + " | Số lượng trong kho: " + m.Quantity.ToString()
                                        }).ToList();
            }
            return View();
        }

        public IActionResult MedicalVisitUpdate(int MedicalVisitId, int MedicalRecordId)
        {
            ViewBag.TinhTrangBenhNhan = Enum.GetValues(typeof(ETinhTrangBenhNhan))
                                          .Cast<ETinhTrangBenhNhan>()
                                          .Select(e => new SelectListItem
                                          {
                                              Value = e.ToString(),
                                              Text = e.ToString()
                                          }).ToList();
            var medicalvisit=_unitOfWork.MedicalVisitRepository.Get(mv=>mv.VisitId == MedicalVisitId);
            ViewBag.MedicalRecordId = MedicalRecordId;
            return View(medicalvisit);
        }
        [HttpPost]
        public IActionResult MedicalVisitUpdate(MedicalVisit medicalVisit, int MedicalRecordId)
        {
            _unitOfWork.MedicalVisitRepository.Update(medicalVisit);
            _unitOfWork.Save();
            return RedirectToAction("DoctorDetail", new { MedicalRecordId = MedicalRecordId });
        }

        [HttpPost("CloseMedicalRecord/{MedicalRecordId}")]
        public IActionResult CloseMedicalRecord(int MedicalRecordId)
        {
            var medicalRecord = _unitOfWork.MedicalRecordRepository.Get(mr => mr.MedicalRecordId == MedicalRecordId);
            if (medicalRecord != null)
            {
                medicalRecord.TrangThaiBenhAn = ETrangThaiBenhAn.ketthucchuatri;
                var patient = _unitOfWork.PatientRepository.Get(pt => pt.PatientId == medicalRecord.PatientId);
                patient.TrangThaiBenhAn = ETrangThaiBenhAn.ketthucchuatri;
                _unitOfWork.PatientRepository.Update(patient);
                _unitOfWork.MedicalRecordRepository.Update(medicalRecord);
                _unitOfWork.Save();
                return RedirectToAction("DoctorPatientDetail", "Doctor", new {PatientId=patient.PatientId});
            }
            return RedirectToAction("Index");
        }
    }
}
