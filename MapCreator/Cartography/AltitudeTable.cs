namespace Cartography
{
	public class AltitudeTable : ColorTable<Altitude>
	{
		protected override string RootNodeName { get; } = "Altitudes";
		protected override string EntryNodeName { get; } = "Altitude";
	}
}