namespace MapCreator
{
	public class ProgressUpdateEventArgs : EventArgs
	{
		public Project Project { get; }

		public string Title { get; }
		public string Summary { get; }

		public long Value { get; }
		public long Limit { get; }

		public bool IsComplete => Value >= Limit;

		public ProgressUpdateEventArgs(Project project, string title, string summary, long value, long limit)
		{
			Project = project;
			Title = title;
			Summary = summary;
			Value = value;
			Limit = limit;
		}
	}
}