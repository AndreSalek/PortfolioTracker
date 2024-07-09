using Microsoft.AspNetCore.Mvc;
using PortfolioTracker.Models;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace PortfolioTracker.Controllers
{
	public class PortfolioController : Controller
	{
		private IConfiguration _config;
		public PortfolioController(IConfiguration config)
		{
			_config = config;
		}
		public IActionResult Index()
		{
			return View();
		}
	}
	
}
