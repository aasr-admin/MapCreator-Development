namespace Cartography
{
	public class TerrainTable : ColorTable<Terrain>
	{
		protected override string RootNodeName { get; } = "Terrains";
		protected override string EntryNodeName { get; } = "Terrain";
	}
}