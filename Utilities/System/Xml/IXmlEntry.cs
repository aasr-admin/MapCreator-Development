namespace System.Xml
{
	public interface IXmlEntry
	{
		public void Save(XmlElement node);
		public void Load(XmlElement node);
	}
}