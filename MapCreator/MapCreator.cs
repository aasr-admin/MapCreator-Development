using MapCreator.Interface;

namespace MapCreator
{
	internal static partial class MapCreator
	{
		public static MapCreatorUI UI => MapCreatorUI.Instance;

		[STAThread]
		private static void Main()
		{
			ApplicationConfiguration.Initialize();
			
			Application.Run(UI);
		}
	}
}