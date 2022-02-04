using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckeredGameOfLife
{
    public class Board
    {
        private Tile[,] _board { get; set; }
        public Board()
        {
            int n = Constants.GridSize;
            _board = new Tile[n, n];
            for(int i = 0; i < n; i++) for(int j = 0; j < n; j++)
                {
                    if(i % 2 == j % 2)
                    {
                        _board[i, j] = Tile.TilesByPos[(i, j)];
                    } 
                    else
                    {
                        _board[i, j] = new Tile($"({i}, {j})", (i, j), "Red");
                        Tile.TilesByPos[(i, j)] = _board[i, j];
                    }
                }
        }
        public Tile this[int x, int y]
        {
            get
            {
                if (!x.IsInbounds()) throw new ArgumentOutOfRangeException(nameof(x));
                if (!y.IsInbounds()) throw new ArgumentOutOfRangeException(nameof(y));
                return _board[x, y];
            }
        }
        public Tile this[(int x, int y) pos] => this[pos.x, pos.y];
    }
}
