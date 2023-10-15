namespace MapCreator
{
	public class LoggingEventArgs : EventArgs
	{
		public Logging Logger { get; init; }

		public LoggingEventArgs(Logging logger)
		{
			Logger = logger;
		}
	}
}