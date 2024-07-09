namespace PortfolioTracker.Models
{
	public class AccountApiRequestInfo
	{
		public Guid UserId { get; set; }
		public long LastNonce { get; set; }
	}
}
