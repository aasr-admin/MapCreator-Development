using System.Collections;

namespace MapCreator
{
	public class Logging : IEnumerable<LogEntry>
	{
		public static Logging Global { get; } = new();

		public static event EventHandler<LoggingUpdatedEventArgs> OnUpdated, OnRemoved;
		public static event EventHandler<LoggingEventArgs> OnClear, OnCleared;

		private readonly SortedSet<LogEntry> _Logs = new();

		public int Count => _Logs.Count;

		public IEnumerable<LogEntry> this[LogType type] => Enumerate(type);

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _Logs.GetEnumerator();
		}

		public IEnumerator<LogEntry> GetEnumerator()
		{
			return _Logs.GetEnumerator();
		}

		public IEnumerable<LogEntry> Enumerate(LogType type)
		{
			foreach (var log in _Logs)
			{
				if (log.Type == type)
				{
					yield return log;
				}
			}
		}

		public void Clear()
		{
			OnClear?.Invoke(this, new LoggingEventArgs(this));

			_Logs.Clear();

			OnCleared?.Invoke(this, new LoggingEventArgs(this));
		}

		public bool Remove(LogEntry entry)
		{
			if (_Logs.Remove(entry))
			{
				OnRemoved?.Invoke(this, new LoggingUpdatedEventArgs(this, entry));

				return true;
			}

			return false;
		}

		public void Log(LogType type, string message)
		{
			var entry = new LogEntry(type, message);

			if (_Logs.Add(entry))
			{
				OnUpdated?.Invoke(this, new LoggingUpdatedEventArgs(this, entry));
			}
		}

		public void LogInfo(string message)
		{
			Log(LogType.Info, message);
		}

		public void LogWarn(string message)
		{
			Log(LogType.Warn, message);
		}

		public  void LogError(string message)
		{
			Log(LogType.Error, message);
		}

		public void LogTrace(string message)
		{
			Log(LogType.Trace, message);
		}

		public void WriteFile(string filePath)
		{
			File.WriteAllLines(filePath, _Logs.Select(o => o.ToString()));
		}

		public void AppendFile(string filePath)
		{
			File.AppendAllLines(filePath, _Logs.Select(o => o.ToString()));
		}

		public Task WriteFileAsync(string filePath)
		{
			return File.WriteAllLinesAsync(filePath, _Logs.Select(o => o.ToString()));
		}

		public Task AppendFileAsync(string filePath)
		{
			return File.AppendAllLinesAsync(filePath, _Logs.Select(o => o.ToString()));
		}
	}
}