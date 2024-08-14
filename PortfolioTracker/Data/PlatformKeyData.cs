using PortfolioTracker.Common.Enums;

namespace PortfolioTracker.Data
{
    public class PlatformKeyData
    {
        public string Id { get; set; } = default!;
        public Platform Platform { get; set; }
        public string ApiSecret { get; set; } = default!;
        public string? Passphrase { get; set; }
        public string UserId { get; set; } = default!;
        public virtual User User { get; set; } = default!;
    }
}
