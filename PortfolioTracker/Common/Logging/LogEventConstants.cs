namespace PortfolioTracker.Common.Logging
{
    public class LogEventConstants
    {
        internal static EventId Create = new(1000, "Created");
        internal static EventId Read = new(1001, "Read");
        internal static EventId Update = new(1002, "Updated");
        internal static EventId Delete = new(1003, "Deleted");

        internal static EventId ReadNotFound = new(1003, "ReadNotFound");
        internal static EventId UpdateNotFound = new(1003, "UpdateNotFound");
    }
}
