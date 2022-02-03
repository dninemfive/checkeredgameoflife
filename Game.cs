using System;
using System.Collections.Generic;
using System.Text;

namespace CheckeredGameOfLife
{
    public static class Game
    {
        public static Tile[,] Board { get; private set; }
        private readonly static Dictionary<string, Tile> tilesByName = new();
        public static Tile TileByName(string name)
        {
            if (tilesByName.ContainsKey(name)) return tilesByName[name];
            return null;
        }
    }
}
