using Microsoft.AspNetCore.Mvc;
using PortfolioTracker.Common;
using PortfolioTracker.Common.Enums;
using PortfolioTracker.Models;
using PortfolioTracker.ViewModels;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace PortfolioTracker.Controllers
{
	public class PortfolioController : Controller
	{
		private IConfiguration _config;
		private ILogger<PortfolioController> _logger;
        private Dictionary<Enum, string[]> _platformKeyMap { get; set; }
        private IHttpClientFactory _httpClientFactory;
        public PortfolioController(IConfiguration config, IHttpClientFactory httpClientFactory, Dictionary<Enum, string[]> platformKeyMap, ILogger<PortfolioController> logger)
		{
			_config = config;
			_httpClientFactory = httpClientFactory;
			_platformKeyMap = platformKeyMap;
			_logger = logger;
		}

		[HttpGet]
		public IActionResult ApiKeyManagement()
		{
            ViewData["Title"] = "Key Management";
            return View(new PlatformApiKeyViewModel());
		}
		[HttpPost]
		public IActionResult ApiKeyManagement(PlatformApiKeyViewModel apiData)
		{
			
			return View();
		}
		[HttpGet]
		public IActionResult PlatformRequiredFields(string platform)
		{
			if (Enum.TryParse(platform, out Platform platformEnum))
			{
                return Json(_platformKeyMap[platformEnum]);
            }
			return NotFound(platform);
		}

		public IActionResult Index()
		{
			return View();
		}
	}
	
}
