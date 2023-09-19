using Photoshop;

using System.Xml;

namespace Cartography
{
	public interface IColorTableEntry : IColorEntry, IXmlEntry
	{
		sbyte Z { get; set; }
	}
}