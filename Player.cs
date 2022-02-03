using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;

namespace CheckeredGameOfLife
{
    public class Player
    {
        public string Name;
        public Tile Tile { get; private set; }
        public (int x, int y) Pos => Tile.Pos;
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
        public Ellipse Marker { get; private set; }
        public Color Color { get; private set; }
        public Player(Grid grid, Color color)
        {
            Tile = Tile.Infancy;
            Color = color;
            Marker = grid.Add(this);
        }
        public void SkipNextTurn() => AvailableActions.Clear();
        public void GoTo(Tile t)
        {
            LeaveCurrentTile();
            t.ReceivePlayer(this);
        }
        public void GoTo(int x, int y) => GoTo(Game.Board[x, y]);
        private void LeaveCurrentTile()
        {
            Tile.LosePlayer();            
        }
    }
}
