using System;
using System.Collections.Generic;
using System.Text;

namespace CheckeredGameOfLife
{
    public class Player
    {
        public string name;
        public bool dead;
        public int Points;
        public delegate void TurnAction();
        public HashSet<TurnAction> AvailableActions;
        public void GoTo(Tile t) { }
    }
}
