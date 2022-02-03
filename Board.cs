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
            _board = new Tile[8, 8];
            for(int i = 0; i < 8; i++) for(int j = 0; j < 8; j++)
                {
                    if(i % 2 == j % 2)
                    {
                        _board[i, j] = Tile.TilesByPos[(i, j)];
                    } 
                    else
                    {
                        _board[i, j] = new Tile($"Red at ({i}, {j})", (i, j));
                        Tile.TilesByPos[(i, j)] = _board[i, j];
                    }
                }
        }
        public Tile this[int x, int y]
        {
            get
            {
                if (x < 0 || x >= _board.GetLength(0)) throw new ArgumentOutOfRangeException(nameof(x));
                if (y < 0 || y >= _board.GetLength(1)) throw new ArgumentOutOfRangeException(nameof(y));
                return _board[x, y];
            }
        }
        public Tile this[(int x, int y) pos] => this[pos.x, pos.y];
    }
}
