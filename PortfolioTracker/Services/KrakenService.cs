namespace PortfolioTracker.Services
{
    public class KrakenService
    {
        private HttpClient _httpClient;
        public KrakenService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
