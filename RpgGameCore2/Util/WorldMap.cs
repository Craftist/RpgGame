using System;
using System.IO;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace RpgGameCore2.Util
{
    public static class WorldMap
    {
        public class Location
        {
            public char Char;
            public string ForeColor, BackColor;
        }

        /// <summary>
        /// Gets the outer map.
        /// Indexers are in this order: Map[X][Y].
        /// </summary>
        public static Location[][] Map;

        public static void LoadMap()
        {
            Image<Rgb24> mapImage = Image.Load<Rgb24>(File.ReadAllBytes(@$"{Utils.GetRealWorkingDirectory()}\Map.png"));
            Size mapSize = mapImage.Size();
            Map = new Location[mapSize.Width][mapSize.Height];
        }
    }
}
