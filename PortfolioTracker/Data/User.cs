using Microsoft.AspNetCore.Identity;

namespace PortfolioTracker.Data
{
    public class User : IdentityUser
    {
        public virtual ICollection<PlatformKeyData> ApiKeyList { get; set; } = default!;
    }
}
