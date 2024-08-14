using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioTracker.Common.Enums;
using PortfolioTracker.Common.Interfaces;
using PortfolioTracker.Data;
using PortfolioTracker.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PortfolioTracker.Controllers
{
    [Authorize]
    public class PortfolioController : Controller
    {
        private ILogger<PortfolioController> _logger;
        private Dictionary<Enum, string[]> _platformKeyMap { get; set; }
        private IHttpClientFactory _httpClientFactory;
        private UserManager<User> _userManager;
        private IApplicationDbContext _dbContext;
        private IMapper _mapper;
        public PortfolioController(IHttpClientFactory httpClientFactory,
                                Dictionary<Enum, string[]> platformKeyMap,
                                UserManager<User> userManager,
                                IApplicationDbContext dbContext,
                                IMapper mapper,
                                ILogger<PortfolioController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _platformKeyMap = platformKeyMap;
            _userManager = userManager;
            _dbContext = dbContext;
            _mapper = mapper;
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
            ICollection<PlatformKeyData> keyList = user.ApiKeyList;
            ICollection<PlatformKeyDataViewModel> viewData = _mapper.Map<PlatformKeyDataViewModel[]>(keyList);

            return View(viewData);
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
            return View(new PlatformKeyDataViewModel());
        }
        #endregion


        #region POST METHODS
        [HttpPost]
        public async Task<IActionResult> AddKey(PlatformKeyDataViewModel viewmodel)
        {
            if (!ModelState.IsValid) return View(viewmodel);
            var data = _mapper.Map<PlatformKeyData>(viewmodel);
            data.Id = Guid.NewGuid().ToString();
            data.UserId = _userManager.GetUserId(User) ?? throw new KeyNotFoundException("Missing user ID claim.");
            await _dbContext.ApiKeyData.AddAsync(data);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(ApiKeyManagement));
        }
        [HttpPost]
        public async Task<IActionResult> RemoveKey(string id)
        {
            if (id == null) return NotFound("Invalid key ID");
            string userId = _userManager.GetUserId(User) ?? throw new KeyNotFoundException("Missing user ID claim.");
            var keyData = await _dbContext.ApiKeyData.SingleOrDefaultAsync(data => (data.UserId == userId) && (data.Id == id));

            if (keyData == null) return NotFound(id);
            _dbContext.ApiKeyData.Remove(keyData);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(ApiKeyManagement));
        }
        #endregion


    }

}
