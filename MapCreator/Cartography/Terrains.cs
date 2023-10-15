namespace Cartography
{
	public class Terrains : Colors<Terrain>
	{
		protected override string RootNodeName { get; } = "Terrains";
		protected override string EntryNodeName { get; } = "Terrain";
	}
}