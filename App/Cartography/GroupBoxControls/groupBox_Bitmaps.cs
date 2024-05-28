using System.Xml;

namespace MapCreator
{
	public class MapInfo
	{
		#region Getters And Setters

		public string MapName { get; }

		public byte MapNumber { get; }

		public int XSize { get; }

		public int YSize { get; }

		#endregion

		public MapInfo(XmlElement iXml)
		{
			MapName = iXml.GetAttribute("Name");
			MapNumber = Utility.Parse<byte>(iXml.GetAttribute("Num"));
			XSize = Utility.Parse<int>(iXml.GetAttribute("XSize"));
			YSize = Utility.Parse<int>(iXml.GetAttribute("YSize"));
		}

		public override string ToString()
		{
			return String.Format("{0}", MapName);
		}
	}
}