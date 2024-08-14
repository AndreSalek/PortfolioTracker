
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PortfolioTracker.Common.Interfaces;
using PortfolioTracker.Data;
using System.Threading;

namespace PortfolioTracker.Common.Logging
{
    public sealed class DbLogger : ILogger
    {
        public readonly DbLoggerOptions Options;
        public readonly IApplicationDbContext _dbContext;
        private readonly IServiceScopeFactory _scopeFactory;

        public DbLogger(DbLoggerOptions Options, IServiceScopeFactory scopeFactory)
        {
            Options = Options;
            _scopeFactory = scopeFactory;
        }
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default;

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            // Create ErrorLog instance
            string exceptionMessage, stackTrace, source;
            exceptionMessage = stackTrace = source = "No exception data provided.";

            if (exception is not null)
            {
                exceptionMessage = exception.Message;
                stackTrace = exception.StackTrace ?? "Missing stack trace.";
                source = exception.Source ?? "Missing exception source";
            }

            ErrorLog log = new ErrorLog(logLevel,
                                        eventId,
                                        exceptionMessage,
                                        stackTrace,
                                        source,
                                        DateTime.Now);
            // Write to it db
            using (var scope = _scopeFactory.CreateScope())
            {
                IApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();

                dbContext.ErrorLog.Add(log);
                dbContext.SaveChanges();

                // Scope and requested services get disposed here
            }
        }
    }
}
