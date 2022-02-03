using System;
using System.Collections.Generic;
using System.Text;

namespace CheckeredGameOfLife
{
    public static class Game
    {
        public static Tile[,] Board { get; private set; }        
        public static readonly Random Random = new();
        public static int Roll() => Random.Next(1, 7);
    }
}
