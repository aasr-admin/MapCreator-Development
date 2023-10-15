namespace Cartography
{
	public class Altitudes : Colors<Altitude>
	{
		protected override string RootNodeName { get; } = "Altitudes";
		protected override string EntryNodeName { get; } = "Altitude";
	}
}