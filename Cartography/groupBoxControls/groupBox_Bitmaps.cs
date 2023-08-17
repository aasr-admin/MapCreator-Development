using System.Xml;

namespace Cartography.groupBoxControls
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
			MapNumber = XmlConvert.ToByte(iXml.GetAttribute("Num"));
			XSize = XmlConvert.ToInt32(iXml.GetAttribute("XSize"));
			YSize = XmlConvert.ToInt32(iXml.GetAttribute("YSize"));
		}

		public override string ToString()
		{
			return String.Format("{0}", MapName);
		}
	}
}