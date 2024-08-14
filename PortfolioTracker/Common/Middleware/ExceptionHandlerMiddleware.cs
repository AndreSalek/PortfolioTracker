using Microsoft.AspNetCore.Diagnostics;

namespace PortfolioTracker.Common.Exceptions
{
    public class ExceptionMiddleware : IExceptionHandler
    {
        private readonly ILogger _logger;
        public ExceptionMiddleware(ILogger logger)
        {
            _logger = logger;
        }

        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
