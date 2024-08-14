using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PortfolioTracker.Common.Interfaces;
using PortfolioTracker.Data;
using System.Runtime.Versioning;

namespace PortfolioTracker.Common.Logging
{
    [UnsupportedOSPlatform("browser")]
    [ProviderAlias("Database")]
    public class DbLoggerProvider : ILoggerProvider
    {
        public readonly IOptions<DbLoggerOptions> Options;
        private readonly IServiceScopeFactory _scopeFactory;
        public DbLoggerProvider(IOptions<DbLoggerOptions> _options, IServiceScopeFactory scopeFactory)
        {
            Options = _options;
            _scopeFactory = scopeFactory;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new DbLogger(Options.Value, _scopeFactory);
        }

        public void Dispose()
        {

        }
    }
}
