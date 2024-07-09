using Microsoft.AspNetCore.Mvc;

namespace PortfolioTracker.Controllers
{
	public class PortfolioController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
