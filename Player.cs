using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using System.Linq;

namespace CheckeredGameOfLife
{
    public abstract class Player
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
            // todo: set tile to null; have handling for this
            // the player ends up on Infancy only on their first turn
            Tile = Tile.Infancy;
            Color = color;
            Marker = Game.Grid.Add(this);
        }
        public void TakeTurn()
        {
            if (Game.Over || Game.CheckForGameEnd()) return;
            if (Dead)
            {

            }
            else if (!CanTakeNextTurn)
            {
                CanTakeNextTurn = true;
            }
            else
            {
                int roll = GetPlayerRoll();
                Game.DebugText.Text = roll + "";
                AvailableMoves.Clear();
                AvailableMoves.UnionWith(Move.MovesByRoll[roll].Where(x => x.OffsetPosIsInbounds(this)));
                foreach (Move m in AvailableMoves) Game.Grid.HighlightCoords(m.OffsetPos(this));
                GoTo(GetPlayerMove().OffsetPos(this));
            }            
            Game.Players.GetNextPlayer().TakeTurn();
        }
        public abstract int GetPlayerRoll();
        public abstract Move GetPlayerMove();
        public void GoTo(Tile t)
        {
            // brief delay to make movement more visible
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
