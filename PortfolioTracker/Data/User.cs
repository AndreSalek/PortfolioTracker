using Microsoft.AspNetCore.Identity;
using PortfolioTracker.Models;

namespace PortfolioTracker.Data
{
    public class User : IdentityUser
    {
        public virtual ICollection<PlatformKeyData> ApiKeyList { get; set; } = default!;
    }
}
