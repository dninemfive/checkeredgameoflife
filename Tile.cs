using System;
using System.Collections.Generic;
using System.Text;

namespace CheckeredGameOfLife
{
    public class Tile
    {
        #region TileDefs
        public static readonly Tile_GainPoints Wealth = new("Wealth", 10, (1, 7));
        public static readonly Tile Matrimony = new("Matrimony", (3, 7));
        public static readonly Tile_GoTo Gambling = new("Gambling", Ruin, (5, 7));
        public static readonly Tile_GainPoints HappyOldAge = new("Happy Old Age", 5, (7, 7));
        public static readonly Tile_GoTo Perseverence = new("Perseverance", Success, (0, 6));
        public static readonly Tile Truth = new("Truth", (2, 6));
        public static readonly Tile_GoTo Politics = new("Politics", Congress, (4, 6));
        public static readonly Tile_GoTo Intemperance = new("Intemperance", Intemperance, (6, 6));
        public static readonly Tile_GoTo Crime = new("Crime", Prison, (1, 5));
        public static readonly Tile_GainPoints Happiness = new("Happiness", 5, (3, 5));
        public static readonly Tile_GoTo Idleness = new("Idleness", Disgrace, (5, 5));
        public static readonly Tile_GainPoints Success = new("Success", 5, (7, 5));
        public static readonly Tile_GainPoints Congress = new("Congress", 5, (0, 4));
        public static readonly Tile_GainPoints Honor = new("Honor", 5, (2, 4));
        public static readonly Tile_GoTo Cupid = new("Cupid", Matrimony, (4, 4));
        public static readonly Tile_GoToByRoll Speculation = new(
            "Speculation",
            new()
            {
                (new int[] { 6, 3 }, Wealth)
            },
            Ruin, (6, 4)
        );
        public static readonly Tile_GoTo Honesty = new("Honesty", Happiness, (1, 3));
        public static readonly Tile_GoTo Industry = new("Industry", Wealth, (3, 3));
        public static readonly Tile_GoTo Bravery = new("Bravery", Honor, (5, 3));
        public static readonly Tile Ruin = new("Ruin", (7, 3));
        public static readonly Tile Poverty = new("Poverty", (0, 2));
        public static readonly Tile_GoTo Ambition = new("Ambition", Fame, (2, 2));
        public static readonly Tile_GainPoints College = new("College", 5, (4, 2));
        public static readonly Tile_Death Suicide = new("Suicide", (6, 2));
        public static readonly Tile_GoTo School = new("School", College, (1, 1));
        public static readonly Tile_GoTo Influence = new("Influence", FatOffice, (3, 1));
        public static readonly Tile Fame = new("Fame", (5, 1));
        public static readonly Tile_GainPoints FatOffice = new("Fat Office", 5, (7, 1));
        public static readonly Tile Infancy = new("Infancy", (0, 0));
        public static readonly Tile Disgrace = new("Disgrace", (2, 0));
        public static readonly Tile Jail = new("Jail", (4, 0));
        public static readonly Tile_SkipNextTurn Prison = new("Prison", (6, 0));

        public static readonly HashSet<Tile> Tiles = new();
        public static readonly Dictionary<(int x, int y), Tile> TilesByPos = new();
        #endregion TileDefs
        public string Name { get; private set; } = null;
        public Uri IconUri { get; private set; } = null;
        public (int x, int y) Pos { get; private set; }
        public Player Player { get; private set; } = null;
        public Tile(string name, (int x, int y) pos) 
        {
            Name = name;
            IconUri = new Uri("Red.png", UriKind.Relative);
            Pos = pos;
        }
        public virtual void ReceivePlayer(Player p)
        {
            if (Player != null) Player.GoTo(Jail);
            Player = p;
        }
        public void LosePlayer()
        {
            Player = null;
        }
    }
    public class Tile_GainPoints : Tile
    {
        public int Points { get; private set; }
        public Tile_GainPoints(string name, int points, (int x, int y) pos) : base(name, pos)
        {
            Points = points;
        }
        public override void ReceivePlayer(Player p)
        {
            base.ReceivePlayer(p);
            p.Points += Points;
        }
    }
    public class Tile_GoTo : Tile
    {
        public Tile TargetTile { get; private set; }
        public Tile_GoTo(string name, Tile targetTile, (int x, int y) pos) : base(name, pos)
        {
            TargetTile = targetTile;
        }
        public override void ReceivePlayer(Player p)
        {
            // no call of base because a player can never stay here
            p.GoTo(TargetTile);
        }
    }
    public class Tile_SkipNextTurn : Tile
    {
        public Tile_SkipNextTurn(string name, (int x, int y) pos) : base(name, pos) { }
        public override void ReceivePlayer(Player p)
        {
            base.ReceivePlayer(p);
            p.SkipNextTurn();
        }
    }
    public class Tile_Death : Tile
    {
        public Tile_Death(string name, (int x, int y) pos) : base(name, pos) { }
        public override void ReceivePlayer(Player p)
        {
            base.ReceivePlayer(p);
            p.Dead = true;
        }
    }
    public class Tile_GoToByRoll : Tile
    {
        public DefaultDict<int, Tile> TargetTilesByRoll;
        public Tile_GoToByRoll(string name, HashSet<(int[], Tile)> targetTilesByRoll, Tile defaultTile, (int x, int y) pos) : base(name, pos)
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
