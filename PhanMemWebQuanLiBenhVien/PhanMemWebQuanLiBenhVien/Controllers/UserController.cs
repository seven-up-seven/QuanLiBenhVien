using Microsoft.AspNetCore.Mvc;

namespace PhanMemWebQuanLiBenhVien.Controllers
{
	public class UserController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
