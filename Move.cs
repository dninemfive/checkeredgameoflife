using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckeredGameOfLife
{
    public class Move
    {
        public static Dictionary<int, List<Move>> MovesByRoll = new()
        {
            { 1, new() { Up, Down } },
            { 2, new() { Left, Right } },
            { 3, new() { UpRight, UpLeft, DownRight, DownLeft } },
            { 4, new() { Up, Up * 2, Down, Down * 2 } },
            { 5, new() { Left, Left * 2, Right, Right * 2 } },
            { 6, new() { UpRight, UpRight * 2, UpLeft, UpLeft * 2, DownRight, DownRight * 2, DownLeft, DownLeft * 2 } },
        };
        public int xOffset, yOffset;
        public Move(int x, int y)
        {
            xOffset = x;
            yOffset = y;
        }
        public (int x, int y) OffsetPos(Player p) => (p.Pos.x + xOffset, p.Pos.y + yOffset);
        public bool OffsetPosIsInbounds(Player p) => OffsetPos(p).IsInbounds();
        public static Move operator +(Move a, Move b) => new Move(a.xOffset + b.xOffset, a.yOffset + b.yOffset);
        public static Move operator *(Move m, int dist) => new Move(m.xOffset * dist, m.yOffset * dist);
        public static Move operator -(Move m) => m * -1;
        public static readonly Move Up = new Move(0, 1);
        public static readonly Move Right = new Move(1, 0);
        public static readonly Move Down = -Up;
        public static readonly Move Left = -Right;
        public static readonly Move UpRight = Up + Right;
        public static readonly Move UpLeft = Up + Left;
        public static readonly Move DownRight = Down + Right;
        public static readonly Move DownLeft = Down + Left;
    }
}
