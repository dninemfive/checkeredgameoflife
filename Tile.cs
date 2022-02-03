using System;
using System.Collections.Generic;
using System.Text;

namespace CheckeredGameOfLife
{
    public class Tile
    {
        public static Tile Jail;
        public string Name { get; private set; } = null;
        public Uri IconUri { get; private set; } = null;
        public (int x, int y) Pos { get; private set; }
        public Player Player { get; private set; } = null;
        public Tile(string unresolvedUri) 
        {
            IconUri = new Uri(unresolvedUri);
        }
        public virtual void ReceivePlayer(Player p)
        {
            if (Player != null) Player.GoTo(Jail);
            Player = p;
        }
        public virtual void PostInit() { }
    }
    public class GainPoints : Tile
    {
        public int Points { get; private set; }
        public GainPoints(int points, string unresolvedUri) : base(unresolvedUri)
        {
            Points = points;
        }
        public override void ReceivePlayer(Player p)
        {
            base.ReceivePlayer(p);
            p.Points += Points;
        }
    }
    public class GoTo : Tile
    {
        public Tile TargetTile { get; private set; }
        private string _tileName;
        public GoTo(string tileName, string unresolvedUri) : base(unresolvedUri)
        {
            _tileName = tileName;
        }
        public override void ReceivePlayer(Player p)
        {
            // no call of base because a player can never land here
            p.GoTo(TargetTile);
        }
        public override void PostInit()
        {
            base.PostInit();
        }
    }
}
