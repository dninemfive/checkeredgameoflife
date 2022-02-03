using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using System.Windows.Controls;

namespace CheckeredGameOfLife
{
    public class Game
    {
        public static readonly Random Random = new();
        public static int Roll() => Random.Next(1, Constants.DieSize);
        public static Board Board { get; private set; }
        public static Grid Grid { get; private set; }
        public static TextBlock DebugText { get; private set; }
        public Game(Grid g, TextBlock debugText)
        {
            Grid = g;
            // Selects all Tile fields on the Tile class
            foreach(Tile t in typeof(Tile).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.FieldType == typeof(Tile) || x.FieldType.IsSubclassOf(typeof(Tile)))
                .Select(x => x.GetValue(null))) 
            {
                Tile.Tiles.Add(t);
                Tile.TilesByPos[t.Pos] = t;
            }
            Board = new();
            DebugText = debugText;
        }
    }
}
