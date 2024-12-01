using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using PhanMemWebQuanLiBenhVien.DataAccess;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using PhanMemWebQuanLiBenhVien.Models;

namespace PhanMemWebQuanLiBenhVien.Controllers
{
	public class CustomedUserController : Controller
	{
		private IUnitOfWork _unitofwork;
		private UserManager<IdentityUser> _usermanager;
		private ApplicationDbContext _db;
		public CustomedUserController(IUnitOfWork unitofwork, UserManager<IdentityUser> userManager, ApplicationDbContext db)
		{
			_unitofwork = unitofwork;
			_usermanager = userManager;
			_db= db;
		}
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult DoctorIndex()
		{
			var doctorlist = _unitofwork.DoctorRepository.GetAll();
			return View(doctorlist);
		}
		public IActionResult AssignDoctorAccount(int DoctorId)
		{
			var doctor=_unitofwork.DoctorRepository.Get(u=>u.DoctorId==DoctorId);
			return View(doctor);
		}
		[HttpPost]
		[ActionName("AssignDoctorAccount")]
		public async Task<IActionResult> AssignDoctorAccountPost(int DoctorId, string username, string password)
		{
			var true_user = new CustomedUser();
			true_user.UserId= DoctorId;
			true_user.UserName= username;
			true_user.UserRole = Ultilities.Utilities.ERole.doctor;
			_usermanager.CreateAsync(true_user, password).GetAwaiter().GetResult();
			_usermanager.AddToRoleAsync(true_user, "Doctor").GetAwaiter().GetResult();
			var doctor=_unitofwork.DoctorRepository.Get(u=>u.DoctorId == DoctorId);
			doctor.HasAccount = true;
			doctor.Username = username;
			_unitofwork.DoctorRepository.Update(doctor);
			_unitofwork.Save();
			return RedirectToAction("DoctorIndex");
		}

		public IActionResult NurseIndex()
		{
			var nurselist=_unitofwork.NurseRepository.GetAll();
			return View(nurselist);
		}
		
        public IActionResult AssignNurseAccount(int NurseId)
        {
            var nurse = _unitofwork.NurseRepository.Get(u => u.NurseId == NurseId);
            return View(nurse);
        }
        [HttpPost]
        [ActionName("AssignNurseAccount")]
        public async Task<IActionResult> AssignNurseAccountPost(int NurseId, string username, string password)
        {
            var true_user = new CustomedUser();
            true_user.UserId = NurseId;
            true_user.UserName = username;
			true_user.UserRole = Ultilities.Utilities.ERole.nurse; 
            _usermanager.CreateAsync(true_user, password).GetAwaiter().GetResult();
            _usermanager.AddToRoleAsync(true_user, "Nurse").GetAwaiter().GetResult();
            var nurse = _unitofwork.NurseRepository.Get(u => u.NurseId == NurseId);
            nurse.HasAccount = true;
            nurse.Username = username;
            _unitofwork.NurseRepository.Update(nurse);
            _unitofwork.Save();
            return RedirectToAction("NurseIndex");
        }

		public async Task<IActionResult> DeleteDoctorAccount(int DoctorId)
		{
			var doctor=_unitofwork.DoctorRepository.Get(u=>u.DoctorId == DoctorId);
			if (doctor != null)
			{
                doctor.HasAccount = false;
                _unitofwork.DoctorRepository.Update(doctor);
                _unitofwork.Save();
            }
			var user=_db.customedUsers.FirstOrDefault(u=>(u.UserId == DoctorId && u.UserRole==Ultilities.Utilities.ERole.doctor));
			if (user != null)
			{
				await _usermanager.DeleteAsync(user);
			}
			return RedirectToAction("AssignDoctorAccount", new {DoctorId=DoctorId});
		}
        public async Task<IActionResult> DeleteNurseAccount(int NurseId)
        {
            var nurse = _unitofwork.NurseRepository.Get(u => u.NurseId == NurseId);
            if (nurse != null)
            {
                nurse.HasAccount = false;
                _unitofwork.NurseRepository.Update(nurse);
                _unitofwork.Save();
            }
            var user = _db.customedUsers.FirstOrDefault(u => u.UserId == NurseId && u.UserRole == Ultilities.Utilities.ERole.nurse);
            if (user != null)
            {
                await _usermanager.DeleteAsync(user);
            }
            return RedirectToAction("AssignNurseAccount", new { NurseId = NurseId });
        }
    }
}
