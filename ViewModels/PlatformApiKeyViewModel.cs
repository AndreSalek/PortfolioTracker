using PortfolioTracker.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace PortfolioTracker.ViewModels
{
    public class PlatformApiKeyViewModel
    {
        public Platform Platform { get; set; }
        [Required]
        [Display(Name = "API-Key")]
        public string ApiKey { get; set; }
        [Display(Name = "API-Secret")]
        public string? ApiSecret { get; set; }
        public string? Passphrase { get; set; }
    }
}
