using System;
using System.Collections.Generic;
using System.Text;

namespace CheckeredGameOfLife
{
    public class Tile
    {
        public static readonly Tile Jail;
        public string Name { get; private set; } = null;
        public Uri IconUri { get; private set; } = null;
        public (int x, int y) Pos { get; private set; }
        public Player Player { get; private set; } = null;
        public Tile(string name, string unresolvedUri) 
        {
            Name = name;
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
        public GainPoints(int points, string name, string unresolvedUri) : base(name, unresolvedUri)
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
        private readonly string _targetTileName;
        public GoTo(string name, string targetTileName, string unresolvedUri) : base(name, unresolvedUri)
        {
            _targetTileName = targetTileName;
        }
        public override void ReceivePlayer(Player p)
        {
            // no call of base because a player can never land here
            p.GoTo(TargetTile);
        }
        public override void PostInit()
        {
            base.PostInit();
            TargetTile = Game.TileByName(_targetTileName);
        }
    }
    public class SkipNextTurn : Tile
    {
        public SkipNextTurn(string name, string unresolvedUri) : base(name, unresolvedUri) { }
        public override void ReceivePlayer(Player p)
        {
            base.ReceivePlayer(p);
            p.SkipNextTurn();
        }
    }
    public class KillPlayer : Tile
    {
        public KillPlayer(string name, string unresolvedUri) : base(name, unresolvedUri) { }
        public override void ReceivePlayer(Player p)
        {
            base.ReceivePlayer(p);
            p.Dead = true;
        }
    }
    public class RandomlyMovePlayer : Tile
    {
        private HashSet<(int, string)> _targetTilesByRoll;
        public Dictionary<int, Tile> TargetTilesByRoll = new();
        public RandomlyMovePlayer(HashSet<(int, string)> targetTilesByRoll, string name, string unresolvedUri) : base(name, unresolvedUri)
        {
            _targetTilesByRoll = targetTilesByRoll;
        }
        public override void ReceivePlayer(Player p)
        {
            base.ReceivePlayer(p);
            int roll = Game.Roll();
            
        }
    }
}
