using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace PortfolioTracker.ViewModels
{
    public class PlatformKeyDataViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public Platform Platform { get; set; }
        [Required]
        [Display(Name = "API-Secret")]
        public string ApiSecret { get; set; } = default!;
        [MaxLength(64, ErrorMessage = "Passphrase is too long.")]
        public string? Passphrase { get; set; }
    }
}
