using System;
using System.Collections.Generic;
using System.Text;

namespace CheckeredGameOfLife
{
    public static class Game
    {
        public static Tile[,] Board { get; private set; }
        public readonly static DefaultDict<string, Tile> TilesByName = new(null)
        {
            { "Jail", new Tile("Jail", "") },
            { }
        };
        public static readonly Random Random = new();
        public static int Roll() => Random.Next(1, 7);
    }
}
