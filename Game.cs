using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;

namespace CheckeredGameOfLife
{
    public static class Game
    {
        public static readonly Random Random = new();
        public static int Roll() => Random.Next(1, 7);
        public static Board Board { get; private set; }
        static Game()
        {
            // Selects all Tile fields on the Tile class
            foreach(Tile t in typeof(Tile).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.FieldType == typeof(Tile) || x.FieldType.IsSubclassOf(typeof(Tile)))
                .Select(x => x.GetValue(null))) 
            {
                Tile.Tiles.Add(t);
                Tile.TilesByPos[t.Pos] = t;
            }
            Board = new();
        }
    }
}
