using System;
using System.Collections.Generic;
using System.Text;

namespace CheckeredGameOfLife
{
    public class Tile
    {
        #region TileDefs
        public static readonly GainPoints Wealth = new("Wealth", 10);
        public static readonly Tile Matrimony = new("Matrimony");
        public static readonly GoTo Gambling = new("Gambling", Ruin);
        public static readonly GainPoints HappyOldAge = new("Happy Old Age", 5);
        public static readonly GoTo Perseverence = new("Perseverance", Success);
        public static readonly Tile Truth = new("Truth");
        public static readonly GoTo Politics = new("Politics", Congress);
        public static readonly GoTo Intemperance = new("Intemperance", Intemperance);
        public static readonly GoTo Crime = new("Crime", Prison);
        public static readonly GainPoints Happiness = new("Happiness", 5);
        public static readonly GoTo Idleness = new("Idleness", Disgrace);
        public static readonly GainPoints Success = new("Success", 5);
        public static readonly GainPoints Congress = new("Congress", 5);
        public static readonly GainPoints Honor = new("Honor");
        public static readonly GoTo Cupid = new("Cupid", Matrimony);
        public static readonly RandomlyMovePlayer Speculation = new(
            "Speculation",
            new HashSet<(int[], string)>()
            {
                (new int[] { 3, 6 }, Wealth)
            },
            Ruin
        );
        public static readonly GoTo Honesty = new("Honesty", Happiness);
        public static readonly GoTo Industry = new("Industry", Wealth);
        public static readonly GoTo Bravery = new("Bravery", Honor);
        public static readonly Tile Ruin = new("Ruin");
        public static readonly Tile Poverty = new("Poverty");
        public static readonly GoTo Ambition = new("Ambition", Fame);
        public static readonly GainPoints College = new("College");
        public static readonly KillPlayer Suicide = new("Suicide");
        public static readonly GoTo School = new("School", College);
        public static readonly GoTo Influence = new("Influence", FatOffice);
        public static readonly Tile Fame = new("Fame");
        public static readonly GainPoints FatOffice = new("Fat Office");
        public static readonly Tile Infance = new("Infancy");
        public static readonly Tile Disgrace = new("Disgrace");
        public static readonly Tile Jail = new("Jail");
        public static readonly SkipNextTurn Prison = new("Prison");
        #endregion TileDefs
        public string Name { get; private set; } = null;
        public Uri IconUri { get; private set; } = null;
        public (int x, int y) Pos { get; private set; }
        public Player Player { get; private set; } = null;
        public Tile(string name) 
        {
            Name = name;
            IconUri = new Uri(name);
        }
        public virtual void ReceivePlayer(Player p)
        {
            if (Player != null) Player.GoTo(Jail);
            Player = p;
        }
    }
    public class GainPoints : Tile
    {
        public int Points { get; private set; }
        public GainPoints(string name, int points = 5) : base(name)
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
        public GoTo(string name, Tile targetTile) : base(name)
        {
            TargetTile = targetTile;
        }
        public override void ReceivePlayer(Player p)
        {
            // no call of base because a player can never stay here
            p.GoTo(TargetTile);
        }
    }
    public class SkipNextTurn : Tile
    {
        public SkipNextTurn(string name) : base(name) { }
        public override void ReceivePlayer(Player p)
        {
            base.ReceivePlayer(p);
            p.SkipNextTurn();
        }
    }
    public class KillPlayer : Tile
    {
        public KillPlayer(string name) : base(name) { }
        public override void ReceivePlayer(Player p)
        {
            base.ReceivePlayer(p);
            p.Dead = true;
        }
    }
    public class RandomlyMovePlayer : Tile
    {
        public DefaultDict<int, Tile> TargetTilesByRoll;
        public RandomlyMovePlayer(string name, HashSet<(int[], Tile)> targetTilesByRoll, Tile defaultTile) : base(name)
        {
            TargetTilesByRoll = new(defaultTile);
            foreach ((int[] ns, Tile t) in targetTilesByRoll)
                foreach (int n in ns) TargetTilesByRoll[n] = t;
        }
        public override void ReceivePlayer(Player p)
        {
            base.ReceivePlayer(p);
            p.GoTo(TargetTilesByRoll[Game.Roll()]);
        }
    }
}
