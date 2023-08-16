namespace Cartography.userPlugin.fileTypeConverters
{
    public class MapReader
    {
        public ushort[] Tiles { get; }

        public MapReader(string filename, int width, int height)
        {
            var blocksX = width / 8;
            var blocksY = height / 8;

            Tiles = new ushort[width * height];

            var reader = new BinaryReader(new FileStream(filename, FileMode.Open));

            for (var i = 0; i < blocksX * blocksY; i++)
            {
                _ = reader.ReadInt32(); // get rid of header

                for (var j = 0; j < 64; j++)
                {
                    var tile = reader.ReadUInt16(); // read the tile
                    _ = reader.ReadByte(); // to hell with the height!

                    var imageX = (8 * (i / blocksY)) + (j % 8);
                    var imageY = (8 * (i % blocksY)) + (j / 8);

                    Tiles[imageX + (imageY * width)] = tile; // insert the tile at its right place
                }
            }

            reader.Close();
        }
    }

    public class MapDifReader
    {
    }

    public class StaticsReader
    {
    }

    public class StaticsDifReader
    {
    }

    public class RadarColReader
    {
        public ushort[] Colors { get; }

        public RadarColReader(string filename)
        {
            Colors = new ushort[0x8000];
            var reader = new BinaryReader(new FileStream(filename, FileMode.Open));

            for (var i = 0; i < 0x8000; i++)
            {
                Colors[i] = reader.ReadUInt16();
            }

            reader.Close();
        }
    }
}