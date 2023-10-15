namespace MapCreator
{
	public class LoggingUpdatedEventArgs : LoggingEventArgs
	{
		public LogEntry Entry { get; init; }

		public LoggingUpdatedEventArgs(Logging logger, LogEntry entry)
			: base(logger)
		{
			Entry = entry;
		}
	}
}