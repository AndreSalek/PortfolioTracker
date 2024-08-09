using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PortfolioTracker.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PortfolioTracker.ViewModels
{
    public class PlatformKeyDataViewModel
    {
        public Guid Id { get; set; }
        [TypeConverter(typeof(StringToEnumConverter<Platform>))]
        [Required]
        public string Platform { get; set; }
        [Required]
        [Display(Name = "API-Secret")]
        public string ApiSecret { get; set; } = default!;
        [MaxLength(64, ErrorMessage = "Passphrase is too long.")]
        public string? Passphrase { get; set; }
    }
}
