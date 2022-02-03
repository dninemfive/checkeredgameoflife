using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using System.Linq;

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
        public bool CanTakeNextTurn = true;
        public readonly HashSet<Move> AvailableMoves = new();
        public Ellipse Marker { get; private set; }
        public Color Color { get; private set; }
        public Player(Color color)
        {
            Tile = Tile.Infancy;
            Color = color;
            Marker = Game.Grid.Add(this);
        }
        public void TakeTurn()
        {
            // allow player to control roll
            int roll = Game.Roll();
            Game.DebugText.Text = roll + "";
            AvailableMoves.Clear();
            AvailableMoves.UnionWith(Move.MovesByRoll[roll].Where(x => x.OffsetPosIsInbounds(this)));
            foreach (Move m in AvailableMoves) Game.Grid.HighlightCoords(m.OffsetPos(this));
            // wait for player to select move...
            // if landed on speculation, allow player to roll again
            // if landed on tile which moves you, briefly sit there to show how you moved before moving you to the correct tile
        }
        public void GoTo(Tile t)
        {
            LeaveCurrentTile();
            t.ReceivePlayer(this);
        }
        public void GoTo(int x, int y) => GoTo(Game.Board[x, y]);
        public void GoTo((int x, int y) pos) => GoTo(pos.x, pos.y);
        private void LeaveCurrentTile()
        {
            Tile.LosePlayer();            
        }
    }
}
