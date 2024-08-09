using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PortfolioTracker.Data;
using PortfolioTracker.ViewModels;

namespace PortfolioTracker.Controllers
{
    public class AccountController : Controller
    {
        //Manages IdentityUser authentication
        private readonly SignInManager<User> _signInManager;
        /*Manages communication with data access layer of Microsoft Identity tables
         https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-8.0&tabs=visual-studio
         */
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
             UserManager<User> userManager,
             SignInManager<User> signInManager,
             ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }
        #region GET_Methods
        public IActionResult Login()
        {
            if (_signInManager.IsSignedIn(User)) return RedirectToAction("Index", "Client");
            return View(new LoginViewModel());
        }
        public IActionResult Register()
        {
            if (_signInManager.IsSignedIn(User)) return RedirectToAction("Index", "Client");
            return View(new RegisterViewModel());
        }
        public IActionResult Lockout() => View();
        #endregion


        // POST METHODS
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = default)
        {
            returnUrl ??= Url.Content("/Portfolio/Index");

            // Login attempt failed
            if (ModelState == null || !ModelState.IsValid) return View(model);

            var identity = await _userManager.FindByNameAsync(model.UserName);
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: true);

            if (identity == null)
            {
                ModelState.AddModelError(string.Empty, "User does not exist.");
                return View(model);
            }
            if (result.Succeeded)
            {
                await _userManager.ResetAccessFailedCountAsync(identity);
                _logger.LogInformation("User logged in.");
                return LocalRedirect(returnUrl);
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out.");
                return LocalRedirect("/Account/Lockout");
            }
            else
            {
                if (identity != null) await _userManager.AccessFailedAsync(identity);
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, string? returnUrl = default)
        {
            returnUrl ??= Url.Content("/Account/Login");
            if (ModelState.IsValid)
            {
                var user = CreateUser();
                //string hashedPassword = _userManager.PasswordHasher.HashPassword(user, model.Password);
                //Trace.WriteLine("Password: " + hashedPassword);
                await _userManager.SetUserNameAsync(user, model.UserName);
                await _userManager.SetEmailAsync(user, model.Email);
                await _userManager.AddPasswordAsync(user, model.Password);
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Registration attempt failed.");
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string? returnUrl = default)
        {
            await _signInManager.SignOutAsync();
            returnUrl ??= Url.Content("/Account/Login");
            return LocalRedirect(returnUrl);
        }


        private User CreateUser()
        {
            try
            {
                return Activator.CreateInstance<User>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(Data.User)}'. " +
                    $"Ensure that '{nameof(Data.User)}' is not an abstract class and has a parameterless constructor, or alternatively ");
            }
        }

    }
}

