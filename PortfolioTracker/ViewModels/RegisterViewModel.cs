using System.ComponentModel.DataAnnotations;

namespace PortfolioTracker.ViewModels
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage = "Username field is required")]
		[StringLength(128, ErrorMessage = "{0} length must be between {2} and {1} characters.", MinimumLength = 4)]
		[Display(Name = "Username")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Password field is required")]
		[StringLength(128, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 10)]
		[RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$", ErrorMessage = "Password must contain minimum of ten characters, at least one letter, one number and one special character")]
		public string Password { get; set; }

		[Required(ErrorMessage = "This field is required")]
		[Compare("Password", ErrorMessage = "Passwords do not match.")]
		[Display(Name = "Repeat password")]
		public string ConfirmPassword { get; set; }

		[Required(ErrorMessage = "Email field is required")]
		[EmailAddress(ErrorMessage = "Invalid email address.")]
		public string Email { get; set; }
	}
}
