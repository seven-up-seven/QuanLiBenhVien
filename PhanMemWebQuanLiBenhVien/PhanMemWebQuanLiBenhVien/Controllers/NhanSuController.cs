using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;
using PhanMemWebQuanLiBenhVien.Models.Models;
using System.Numerics;
using static PhanMemWebQuanLiBenhVien.Ultilities.Utilities;

namespace PhanMemWebQuanLiBenhVien.Controllers
{
    public class NhanSuController : Controller
    {
        private IUnitOfWork _uniUnitOfWork;
        private string wwwroot;
        private IWebHostEnvironment _webHostEnvironment;
        public NhanSuController(IUnitOfWork unitOfWork, IWebHostEnvironment webhostenvironment)
        {
            _uniUnitOfWork = unitOfWork;
            _webHostEnvironment = webhostenvironment;
        }
        public IActionResult Create()
        {
            ViewBag.Genders = new SelectList(new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Nam",
                    Value = ((int)EGender.male).ToString()
                },
                new SelectListItem
                {
                    Text = "Nữ",
                    Value = ((int)EGender.female).ToString()
                }
            }, "Value", "Text");
            ViewBag.Roles = new SelectList(new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Quản lí nhân sự",
                    Value = "QuanLiNhanSu"
                },
                new SelectListItem
                {
                    Text = "Quản lí vật tư",
                    Value = "QuanLiVatTu"
                },
                new SelectListItem
                {
                    Text = "Quản lí bệnh nhân",
                    Value = "QuanLiBenhNhan"
                }
            }, "Value", "Text");
            return View();
        }
        [HttpPost]
        public IActionResult Create(NhanSu nhansu, IFormFile? ImgUrl)
        {
            if (ModelState.IsValid)
            {
                if (ImgUrl!=null)
                {
                    wwwroot = _webHostEnvironment.WebRootPath;
                    string filename = Path.GetFileNameWithoutExtension(ImgUrl.FileName) + Path.GetExtension(ImgUrl.FileName);
                    string filepath = Path.Combine(wwwroot, @"images\");
                    using (var filestream = new FileStream(Path.Combine(filepath, filename), FileMode.Create))
                    {
                        ImgUrl.CopyTo(filestream);
                    }
                    nhansu.ImgUrl = @"\images\" + filename;
                }
                else nhansu.ImgUrl = "";
                _uniUnitOfWork.NhanSuRepository.Add(nhansu);
                _uniUnitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Genders = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = "Nam",
                        Value = ((int)EGender.male).ToString()
                    },
                    new SelectListItem
                    {
                        Text = "Nữ",
                        Value = ((int)EGender.female).ToString()
                    }
                }, "Value", "Text");
                ViewBag.Roles = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = "Quản lí nhân sự",
                        Value = "QuanLiNhanSu"
                    },
                    new SelectListItem
                    {
                        Text = "Quản lí vật tư",
                        Value = "QuanLiVatTu"
                    },
                    new SelectListItem
                    {
                        Text = "Quản lí bệnh nhân",
                        Value = "QuanLiBenhNhan"
                    }
                }, "Value", "Text");
                return View();
            }
        }

        public IActionResult Index()
        {
            var listnhansu=_uniUnitOfWork.NhanSuRepository.GetAll();
            ViewBag.VaiTro= new SelectList(new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Quản lí nhân sự",
                    Value = "QuanLiNhanSu"
                },
                new SelectListItem
                {
                    Text = "Quản lí vật tư",
                    Value = "QuanLiVatTu"
                },
                new SelectListItem
                {
                    Text="Quản lí bệnh nhân",
                    Value="QuanLiBenhNhan" 
                },
                new SelectListItem
                {
                    Text="Chưa có",
                    Value="NoRole"
                }
            }, "Value", "Text");
            return View(listnhansu);
        }
        [HttpPost]
        public IActionResult Index(string SearchName, string SearchID, string SearchVaiTro)
        {
            var listnhansu = _uniUnitOfWork.NhanSuRepository.GetAll();
            if (!string.IsNullOrEmpty(SearchName)) listnhansu = listnhansu.Where(u => u.NhanSuName.ToLower().Contains(SearchName.ToLower()));
            if (!string.IsNullOrEmpty(SearchID)) listnhansu = listnhansu.Where(u => u.NhanSuId.ToString().Contains(SearchID));
            if (!string.IsNullOrEmpty(SearchVaiTro))
            {
                if (SearchVaiTro == "QuanLiVatTu") listnhansu = listnhansu.Where(u => u.Role == "QuanLiVatTu");
                else if (SearchVaiTro == "QuanLiNhanSu") listnhansu = listnhansu.Where(u => u.Role == "QuanLiNhanSu");
                else if (SearchVaiTro == "QuanLiBenhNhan") listnhansu = listnhansu.Where(u => u.Role == "QuanLiBenhNhan");
                else if (SearchVaiTro =="NoRole") listnhansu=listnhansu.Where(u=>string.IsNullOrEmpty(u.Role));
            }
            ViewBag.VaiTro = new SelectList(new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Quản lí nhân sự",
                    Value = "QuanLiNhanSu"
                },
                new SelectListItem
                {
                    Text = "Quản lí vật tư",
                    Value = "QuanLiVatTu"
                },
                new SelectListItem
                {
                    Text="Quản lí bệnh nhân",
                    Value="QuanLiBenhNhan"
                },
                new SelectListItem
                {
                    Text="Chưa có",
                    Value="NoRole"
                }
            }, "Value", "Text");
            return View(listnhansu);
        }

        public IActionResult Update(int NhanSuId)
        {
            var nhansu=_uniUnitOfWork.NhanSuRepository.Get(u=>u.NhanSuId== NhanSuId);
            ViewBag.Genders = new SelectList(new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Nam",
                    Value = ((int)EGender.male).ToString()
                },
                new SelectListItem
                {
                    Text = "Nữ",
                    Value = ((int)EGender.female).ToString()
                }
            }, "Value", "Text");
            ViewBag.Roles = new SelectList(new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Quản lí nhân sự",
                    Value = "QuanLiNhanSu"
                },
                new SelectListItem
                {
                    Text = "Quản lí vật tư",
                    Value = "QuanLiVatTu"
                },
                new SelectListItem
                {
                    Text = "Quản lí bệnh nhân",
                    Value = "QuanLiBenhNhan"
                }
            }, "Value", "Text");
            return View(nhansu);
        }
        [HttpPost]
        public IActionResult Update(NhanSu nhansu, IFormFile? ImgUrl)
        {
            if (ModelState.IsValid)
            {
                if (ImgUrl != null)
                {
                    wwwroot = _webHostEnvironment.WebRootPath;
                    string filename = Path.GetFileNameWithoutExtension(ImgUrl.FileName) + Path.GetExtension(ImgUrl.FileName);
                    string filepath = Path.Combine(wwwroot, @"images\");
                    using (var filestream = new FileStream(Path.Combine(filepath, filename), FileMode.Create))
                    {
                        ImgUrl.CopyTo(filestream);
                    }
                    nhansu.ImgUrl = @"\images\" + filename;
                }
                else nhansu.ImgUrl = "";
                _uniUnitOfWork.NhanSuRepository.Update(nhansu);
                _uniUnitOfWork.Save();
                if (User.IsInRole("QuanLiNhanSu") || User.IsInRole("QuanLiBenhNhan") || User.IsInRole("QuanLiVatTu"))
                {
                    TempData["success"] = "Cập nhật thông tin thành công!";
                    return RedirectToPage("/Account/Manage/Index", new { Area = "Identity" });
                }
                else
                {
                    TempData["success"] = "Cập nhật thông tin thành công!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ViewBag.Genders = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = "Nam",
                        Value = ((int)EGender.male).ToString()
                    },
                    new SelectListItem
                    {
                        Text = "Nữ",
                        Value = ((int)EGender.female).ToString()
                    }
                }, "Value", "Text");
                ViewBag.Roles = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = "Quản lí nhân sự",
                        Value = "QuanLiNhanSu"
                    },
                    new SelectListItem
                    {
                        Text = "Quản lí vật tư",
                        Value = "QuanLiVatTu"
                    },
                    new SelectListItem
                    {
                        Text = "Quản lí bệnh nhân",
                        Value = "QuanLiBenhNhan"
                    }
                }, "Value", "Text");
                if (User.IsInRole("QuanLiNhanSu") || User.IsInRole("QuanLiBenhNhan") || User.IsInRole("QuanLiVatTu"))
                {
                    TempData["error"] = "Cập nhật không thành công!";
                    return RedirectToPage("/Account/Manage/Index", new { Area = "Identity" });
                }
                else
                {
                    TempData["error"] = "Cập nhật không thành công!";
                    return View(nhansu);
                }
            }
        }
        public IActionResult Delete(int NhanSuId)
        {
            var nhansu = _uniUnitOfWork.NhanSuRepository.Get(u => u.NhanSuId == NhanSuId);
            if (nhansu.HasAccount==true)
            {
                TempData["error"] = "Nhân sự đang có tài khoản, không thể xóa";
                return RedirectToAction("Index");
            }
            else
            {
                _uniUnitOfWork.NhanSuRepository.Remove(nhansu);
                _uniUnitOfWork.Save();
                TempData["success"] = "Xóa nhân sự thành công!";
                return RedirectToAction("Index");
            }
        }
    }
}
