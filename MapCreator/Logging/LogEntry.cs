namespace MapCreator
{
	public record class LogEntry : IComparable<LogEntry>
	{
		public Guid UID { get; } = Guid.NewGuid();

		public DateTimeOffset Date { get; } = DateTimeOffset.Now;

		public LogType Type { get; init; }
		public string Message { get; init; }

		private string _Formatted;

		public LogEntry(LogType type, string message)
		{
			Type = type;
			Message = message;
		}

		public override string ToString()
		{
			if (_Formatted != null)
			{
				return _Formatted;
			}

			var now = DateTimeOffset.Now;

			if (Date.Year == now.Year && Date.DayOfYear == now.DayOfYear)
			{
				return _Formatted = $"[{Date.DateTime.ToShortTimeString()}] {Message}";
			}

			return _Formatted = $"[{Date.DateTime.ToShortDateString()} {Date.DateTime.ToShortTimeString()}] {Message}";
		}

		public int CompareTo(LogEntry other)
		{
			if (other is not null)
			{
				return Date.CompareTo(other.Date);
			}

			return -1;
		}

		public static bool operator <(LogEntry left, LogEntry right)
		{
			return left is null ? right is not null : left.CompareTo(right) < 0;
		}

		public static bool operator <=(LogEntry left, LogEntry right)
		{
			return left is null || left.CompareTo(right) <= 0;
		}

		public static bool operator >(LogEntry left, LogEntry right)
		{
			return left is not null && left.CompareTo(right) > 0;
		}

		public static bool operator >=(LogEntry left, LogEntry right)
		{
			return left is null ? right is null : left.CompareTo(right) >= 0;
		}
	}
}