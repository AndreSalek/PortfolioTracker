using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PortfolioTracker.Common;
using PortfolioTracker.Common.Enums;
using PortfolioTracker.Data;
using PortfolioTracker.Models;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace PortfolioTracker.Controllers
{
    [Authorize]
	public class PortfolioController : Controller
	{
		private ILogger<PortfolioController> _logger;
        private Dictionary<Enum, string[]> _platformKeyMap { get; set; }
        private IHttpClientFactory _httpClientFactory;
        private UserManager<User> _userManager;
        public PortfolioController(IHttpClientFactory httpClientFactory,
                                Dictionary<Enum, string[]> platformKeyMap,
                                ILogger<PortfolioController> logger,
                                UserManager<User> userManager)
		{
			_httpClientFactory = httpClientFactory;
			_platformKeyMap = platformKeyMap;
            _userManager = userManager;
			_logger = logger;
		}

        #region GET METHODS
        public IActionResult Index()
        {
            ViewData["Title"] = "Portfolio";
            return View();
        }
        public async Task<IActionResult> ApiKeyManagement()
        {
            ViewData["Title"] = "Key Management";
            var user = await _userManager.GetUserAsync(User);



            return View(new List<PlatformKeyData>());
        }

        public IActionResult PlatformRequiredFields(string platform)
        {
            if (Enum.TryParse(platform, out Platform platformEnum))
            {
                return Json(_platformKeyMap[platformEnum]);
            }
            return NotFound(platform);
        }

		public IActionResult AddKey()
		{
            ViewData["Title"] = "Key Management - Add Key";
            return View(new PlatformKeyData());
		}
        #endregion


        #region POST METHODS
        [HttpPost]
        public IActionResult ApiKeyManagement(PlatformKeyData keyData)
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddKey(PlatformKeyData keyData)
        {
            return View(keyData);
        }
        [HttpPost]
        public IActionResult RemoveKey(int id)
        {
            return View(nameof(ApiKeyManagement));
        }
        #endregion

      
	}
	
}
