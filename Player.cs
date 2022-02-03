using System;
using System.Collections.Generic;
using System.Text;

namespace CheckeredGameOfLife
{
    public class Player
    {
        public string name;
        private bool _dead = false;
        public bool Dead
        {
            get => _dead;
            set
            {
                _dead = true;
            }
        }
        public int Points;
        public delegate void TurnAction();
        public HashSet<TurnAction> AvailableActions;
        public void SkipNextTurn() => AvailableActions.Clear();
        public void GoTo(Tile t) { }
    }
}
