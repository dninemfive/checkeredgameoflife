using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckeredGameOfLife
{
    class Board
    {
        private Tile[,] _board { get; set; }
        public Board()
        {
            _board = new Tile[8, 8];
            for(int i = 0; i < 8; i++) for(int j = 0; j < 8; j++)
                {
                    if(i % 2 == j % 2)
                    {
                        
                    } 
                    else
                    {
                        _board[i, j] = new Tile($"Red at ({i}, {j})", (i, j));
                    }
                }
        }
    }
}
