using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckeredGameOfLife
{
    public abstract class TurnAction
    {
        public static readonly TurnAction_Move MoveUpOne = new TurnAction_Move(0, 1);
        public abstract void Take();
    }
    public class TurnAction_Move
    {
        public int xOffset, yOffset;
        public TurnAction_Move(int x, int y)
        {
            xOffset = x;
            yOffset = y;
        }
        public (int x, int y) OffsetPos(Player p) => (p.Pos.x + xOffset, p.Pos.y + yOffset);
    }
}
