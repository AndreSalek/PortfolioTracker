namespace PortfolioTracker.Data
{
    public class ErrorLog
    {
        public int Id { get; set; }
        public LogLevel LogLevel { get; set; }
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string ExceptionMessage { get; set; }
        public string StackTrace { get; set; }
        public string Source { get; set; }
        public DateTime DateCreated { get; set; }

        public ErrorLog(LogLevel logLevel, EventId eventId, string exceptionMessage, string stackTrace, string source, DateTime dateCreated)
        {
            LogLevel = logLevel;
            EventId = eventId.Id;
            EventName = eventId.Name ?? "";
            ExceptionMessage = exceptionMessage;
            StackTrace = stackTrace;
            Source = source;
            DateCreated = dateCreated;
        }

        public ErrorLog(LogLevel logLevel, int eventId, string eventName, string exceptionMessage, string stackTrace, string source, DateTime dateCreated)
        {
            LogLevel = logLevel;
            this.EventId = eventId;
            EventName = eventName;
            ExceptionMessage = exceptionMessage;
            StackTrace = stackTrace;
            Source = source;
            DateCreated = dateCreated;
        }
    }
}
